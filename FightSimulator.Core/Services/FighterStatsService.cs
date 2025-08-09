using System.Text.Json;
using System.Text.Json.Serialization;
using FightSimulator.Core.Boosts;

namespace FightSimulator.Core.Services;

public class FighterStatsService
{
    protected static Dictionary<string, List<List<Talent>>> _talentTreeCombinationsCache = new Dictionary<string, List<List<Talent>>>();
    protected static Dictionary<string, List<List<Talent>>> _allCombinationsCache = new Dictionary<string, List<List<Talent>>>();
    protected string _cacheFilesDirectory = @"\\nas-pears\documents\AgeOfApes\FighterOutputs\Cache";
    
    public List<FighterConfiguration> GetConfigurationsForFighter(Fighter fighter, Fighter? deputyFighter, int selectedDeputyTalent, FightSimulationOptions fightOptions)
    {
        // TODO: Can expand this idea to what's the best combination of research, relics, equipment etc. if they're all standardised into one kind of cost unit
        // TODO: Needs implementing to show best configs of different types by doing battle sims, like best map config, best seige config, best talent leap config, best group fight config
        var allTalentCombinations = GetAllPossibleCombinations(fighter, new List<int> { 100 });
        var allConfigurations = new List<FighterConfiguration>();
        
        foreach (var combination in allTalentCombinations)
        {

            if (deputyFighter != null)
            {
                var deputyTalent = new Talent
                {
                    Boosts = new List<Boost>()
                };

                var passiveFighterSkills =
                    deputyFighter
                        .FighterSkills
                        .Where(x => x.FighterSkillType == FigherSkillType.Passive)
                        .SelectMany(x => x.Boosts)
                        .ToList();
                
                deputyTalent.Boosts.AddRange(passiveFighterSkills);
                deputyTalent.Boosts.AddRange(deputyFighter.TalentSkills[selectedDeputyTalent].Boosts);
                deputyTalent.Boosts.ForEach(x => x.Source = "Deputy");
                
                combination.Add(deputyTalent);
            }

            var config = new FighterConfiguration
            {
                Fighter = fighter,
                DeputyFighter = deputyFighter,
                DeputySelectedTalent = selectedDeputyTalent,
                SelectedTalents = combination,
                // TODO: Improve this so that fight options and deputy fighters can be applied in later steps, then this can be cached
                ArmyBoosts = BuildArmyBoosts(fighter, deputyFighter, combination, fightOptions)
            };
            
            allConfigurations.Add(config);
        }

        var distinctConfigurations = GetDistinctConfigurations(allConfigurations);

        return distinctConfigurations;
    }

    private List<FighterConfiguration> GetDistinctConfigurations(List<FighterConfiguration> allConfigurations)
    {
        var distinctConfigurations = new List<FighterConfiguration>();
        var configurationsByTalentBreakdown = allConfigurations.GroupBy(x => x.TalentBreakdown);

        
        foreach (var group in configurationsByTalentBreakdown)
        {
            var distinctWithinGroup = new List<FighterConfiguration>();

            foreach (var config in group.ToList())
            {
                var alreadyExists = false;
                foreach (var distinctExisting in distinctWithinGroup)
                {
                    var differences = config.ArmyBoosts.BoostsByType.Except(distinctExisting.ArmyBoosts.BoostsByType, BoostGrouping.BoostGroupingComparer);
                    if (!differences.Any())
                    {
                        alreadyExists = true;
                        break;
                    }
                }
                
                if(!alreadyExists)
                    distinctWithinGroup.Add(config);
            }
            
            distinctConfigurations.AddRange(distinctWithinGroup);
        }
        
        return distinctConfigurations;
    }

    public List<List<Talent>> GetAllPossibleCombinations(Fighter fighter, List<int> desiredTalentTotals)
    {
        var cacheFileName = $"{_cacheFilesDirectory}/{fighter.Name}Combinations.json";

        if (_allCombinationsCache.ContainsKey(fighter.Name))
        {
            return _allCombinationsCache[fighter.Name];
        }
        
        if (File.Exists(cacheFileName))
        {
            var cachedJson = File.ReadAllText(cacheFileName);
            var cachedCombinations = JsonSerializer.Deserialize<List<List<Talent>>>(cachedJson);
            
            if (_allCombinationsCache.Count < 10)
                _allCombinationsCache[fighter.Name] = cachedCombinations;
            
            return cachedCombinations;
        }
        
        var rootTalents = fighter
            .TalentSkills
            .Select(x => x.TalentTree)
            .ToList();

        var combinations = new List<List<Talent>>();

        foreach (var rootTalent in rootTalents)
        {
            var treeCombinations = GetTreeCombinationsCached(rootTalent);
            combinations.AddRange(treeCombinations);
        }

        var allPermutations = GetAllPermutations(combinations, desiredTalentTotals);
        
        var json = JsonSerializer.Serialize(allPermutations, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles});
        File.WriteAllText(cacheFileName, json);
        
        if (_allCombinationsCache.Count < 10)
            _allCombinationsCache[fighter.Name] = allPermutations;

        return allPermutations;
    }

    public List<List<Talent>> GetTreeCombinationsCached(Talent rootTalent)
    {
        var cacheFileName = $"{_cacheFilesDirectory}/{rootTalent.TalentTreeName}.json";
        if (_talentTreeCombinationsCache.ContainsKey(rootTalent.TalentTreeName))
        {
            return _talentTreeCombinationsCache[rootTalent.TalentTreeName];
        }
        
        if (File.Exists(cacheFileName))
        {
            var cachedJson = File.ReadAllText(cacheFileName);
            var cachedCombinations = JsonSerializer.Deserialize<List<List<Talent>>>(cachedJson);
            _talentTreeCombinationsCache[rootTalent.TalentTreeName] = cachedCombinations;
            return cachedCombinations;
        }
        
        var combinations = GetTreeCombinations(rootTalent);
        combinations
            .ForEach(x => x
            .ForEach(t =>
                t.Boosts.ForEach(b => b.Source = rootTalent.TalentTreeName)
            ));
        
        _talentTreeCombinationsCache[rootTalent.TalentTreeName] = combinations;
        
        // Remove these large links before serialising, they aren't needed anymore.  Should probably have two different models for building and returning.
        combinations.ForEach(x => x.ForEach(t =>
        {
            t.RootTalent = null;
            t.NextTalents = null;
            t.LastRequiredTalent = null;
        }));
        
        var json = JsonSerializer.Serialize(combinations, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles});
        File.WriteAllText(cacheFileName, json);
        return combinations;
    }
    
    public List<List<Talent>> GetTreeCombinations(Talent rootTalent) =>
        GetTreeCombinations(new List<Talent> { rootTalent });
    
    protected List<List<Talent>> GetTreeCombinations(List<Talent> talents)
    {
        var lastTalent = talents.Last();

        var nextCombinations = new List<List<Talent>>();
        
        // Do the traversal in a very controlled linear way so we don't end up with duplicate results or effort
        // Follow the required tree only
        
        var nextRequiredTalent = lastTalent.NextTalents.FirstOrDefault(x => !x.Optional);
        if (nextRequiredTalent == null && lastTalent.LastRequiredTalent != null)
        {
            // Navigate from an optional path back to the main path
            nextRequiredTalent = lastTalent.LastRequiredTalent.NextTalents.FirstOrDefault(x => !x.Optional);
        }
        
        if (nextRequiredTalent != null)
        {
            var copiedList = CopyTalentsList(talents);
            copiedList.Add(nextRequiredTalent);
            var combinationsWithoutOptionalTalent = GetTreeCombinations(copiedList);
            nextCombinations.AddRange(combinationsWithoutOptionalTalent);
        }
        
        // Add optional talents in this path
        // Choose the first so it prefers to navigate left at this step
        var optionalTalent = lastTalent.NextTalents.FirstOrDefault(x => x.Optional);
        if (optionalTalent != null)
        {
            var copiedList = CopyTalentsList(talents);
            copiedList.Add(optionalTalent);
            var combinationsWithOptionalTalent = GetTreeCombinations(copiedList);
            nextCombinations.AddRange(combinationsWithOptionalTalent);
        }
        
        
        // Traverse the other side of the tree to get all the combinations
        // Choose the last so that it prefers traversing to the right at this step
        var otherSideOfTree = lastTalent.RootTalent.NextTalents.LastOrDefault(x => !talents.Contains(x));
        if (otherSideOfTree != null)
        {
            var copiedList = CopyTalentsList(talents);
            copiedList.Add(otherSideOfTree);
            var combinationsWithOtherTreeHalf = GetTreeCombinations(copiedList);
            nextCombinations.AddRange(combinationsWithOtherTreeHalf);
        }

        // Order them by depth so it's easier to filter out duplicates later
        var orderedTalents = talents.OrderBy(x => x.TalentId).ToList();
        nextCombinations.Add(orderedTalents);

        // Some combinations can be captured twice by forward + reverse traversals, so filter them out
        var distinctCombinations = new List<List<Talent>>();
        foreach (var nextCombination in nextCombinations)
        {
            var orderedNextCombination = nextCombination.OrderBy(x => x.TalentId).ToList();
            if (!distinctCombinations.Any(x => x.SequenceEqual(orderedNextCombination)))
            {
                distinctCombinations.Add(orderedNextCombination);
            }
        }
            
        return distinctCombinations;
    }

    private List<List<Talent>> GetAllPermutations(List<List<Talent>> talentLists, List<int> desiredSizes)
    {
        var allPermutations = new List<List<Talent>>();
        var distinctTalentTreeCount = talentLists.Select(x => x.First().TalentTreeName).Distinct().Count();
        
        // Try to minimise wasted work by only looking for permutations that use all talent points
        var talentListDictionary = talentLists.GroupBy(x => x.Sum(t => t.TalentPointCost)).ToList();
        
        foreach (var list in talentListDictionary)
        {
            var size = list.Key;
            var values = list.ToList();
            
            foreach (var value in values)
            {
                var talentTreeName = value.First().TalentTreeName;
                
                foreach (var desiredSize in desiredSizes)
                {
                    // TODO: This needs to support combining multiple trees
                    var sizeToMatchWith = desiredSize - size;

                    if (sizeToMatchWith == 0)
                    {
                        // It is already the desired size on its own, so add it
                        allPermutations.Add(value);
                    }
                    
                    var matchingGroup = talentListDictionary
                        .SingleOrDefault(x => x.Key == sizeToMatchWith);

                    if (matchingGroup == null) continue;

                    var matchingLists = matchingGroup
                        .ToList()
                        .Where(x => x.First().TalentTreeName != talentTreeName || distinctTalentTreeCount == 1)
                        // Don't combine overlapping lists
                        .Where(x => x.Except(value).Any())
                        .ToList();
                    
                    foreach (var matches in matchingLists)
                    {
                        var copiedList = CopyTalentsList(value);
                        copiedList.AddRange(matches.Where(x => !copiedList.Contains(x)));
                        allPermutations.Add(copiedList);
                    }

                }
            
            }
        }

        return allPermutations;
    }

    private List<Talent> CopyTalentsList(List<Talent> talents)
    {
        var newList = new List<Talent>();
        newList.AddRange(talents);
        return newList;
    }

    public ArmyBoosts BuildArmyBoosts(Fighter fighter, Fighter deputyFighter, List<Talent> selectedTalents, FightSimulationOptions fightOptions)
    {
        AddFighterSkillBoosts(fighter, selectedTalents, fightOptions);
        AddFighterTalentSkillBoosts(fighter, selectedTalents);
        AddOtherBoosts(selectedTalents);

        // TODO: Implement increased rage talents/boosts
        
        var boosts = selectedTalents
            .SelectMany(x => x.Boosts)
            .ToList();
        
        boosts.ForEach(x => x.ApplicabilityGroup = GetApplicabilityGroup(x));
        
        var applicableBoosts = boosts.Where(x => IsApplicableBoost(x, fighter, deputyFighter, fightOptions)).ToList();
        var boostsByType = GroupBoostsByType(applicableBoosts);
        var unitBoosts = GroupBoostsByTroopRestriction(fightOptions, boostsByType);

        // TODO: Use ApplicabilityGroup below here, return a function from GetStatBoosts that takes in fightOptions to finalise the stats
        var armyBoosts = new ArmyBoosts
        {
            UnitBoosts = new List<UnitBoosts>
            {
                GetUnitBoosts(unitBoosts, TroopType.Hitter), 
                GetUnitBoosts(unitBoosts, TroopType.Pilot),
                GetUnitBoosts(unitBoosts, TroopType.Shooter),
                GetUnitBoosts(unitBoosts, TroopType.WallBreaker)
            },
            DamageDealtByNormalAttacks = GetStatBoosts(boostsByType, x => x.BoostType is BoostType.IncreasedNormalAttackDamage),
            DamageDealtBySkillsPercentIncrease = GetStatBoosts(boostsByType, x => x.BoostType is BoostType.IncreasedSkillDamage),
            DamageDealtByCounterAttacks = GetStatBoosts(boostsByType, x => x.BoostType is BoostType.IncreasedCounterAttackDamage),
            IncreasedMaxTroopsPercent = boostsByType.Where(x => x.BoostType == BoostType.IncreasedMaxTroops).Sum(x => x.TotalMaxBoostAmount),
            ApplicableBoosts = applicableBoosts,
            PassiveSkills = boostsByType.FirstOrDefault(x => x.BoostType == BoostType.SkillDamageFactor)?.Boosts ?? new List<Boost>(),
            BoostsByType = boostsByType
        };

        return armyBoosts;
    }

    private static List<BoostGrouping> GroupBoostsByTroopRestriction(FightSimulationOptions fightOptions, List<BoostGrouping> boostGroupings)
    {
        var unitBoosts = boostGroupings.Where(x =>
            x.BoostType 
                is BoostType.IncreasedAttack 
                or BoostType.IncreasedDefence 
                or BoostType.IncreasedHealth
                or BoostType.IncreasedDamage 
                or BoostType.IncreasedDamageToCounteredUnit)
            .SelectMany(x => x.Boosts);
        
        var groupedBoosts = unitBoosts
            .Where(x => 
                !(
                    x.TroopRestriction == TroopType.ThreeUnitTypes
                    && (fightOptions.UseCannons || fightOptions.UseShooterUnitSkill)
                )
            )
            .GroupBy(x => new BoostGrouping
            {
                BoostType = x.BoostType,
                TroopRestriction = x.TroopRestriction
            })
            .Select(x => new BoostGrouping
            {
                BoostType = x.Key.BoostType,
                TroopRestriction = x.Key.TroopRestriction,
                Boosts = x.ToList()
            }).ToList();

        CalculateTotalBoosts(groupedBoosts);

        return groupedBoosts;
    }

    private static List<BoostGrouping> GroupBoostsByType(IEnumerable<Boost> applicableBoosts)
    {
        var groupedBoosts = applicableBoosts
            .GroupBy(x => new BoostGrouping
            {
                BoostType = x.BoostType,
                ApplicabilityGroup = x.ApplicabilityGroup
            })
            .Select(x => new BoostGrouping
            {
                BoostType = x.Key.BoostType,
                ApplicabilityGroup = x.Key.ApplicabilityGroup,
                Boosts = x.ToList()
            })
            .ToList();
        
        CalculateTotalBoosts(groupedBoosts);

        return groupedBoosts;
    }

    private static void CalculateTotalBoosts(List<BoostGrouping> groupedBoosts)
    {
        groupedBoosts.ForEach(x => x.TotalMaxBoostAmount = x.Boosts.Sum(b => b.MaxBoostAmount));
    }

    private void AddFighterSkillBoosts(Fighter fighter, List<Talent> selectedTalents, FightSimulationOptions fightOptions)
    {
        foreach (var fighterSkill in fighter.FighterSkills.Where(x => x.FighterSkillType == FigherSkillType.Passive))
        {
            var applicableFighterBoosts = fighterSkill
                .Boosts.Where(x => IsApplicableBoost(x, fightOptions)).ToList();

            applicableFighterBoosts.ForEach(x => x.Source = "FighterSkill");

            var talent = new Talent
            {
                Boosts = applicableFighterBoosts
            };

            selectedTalents.Add(talent);
        }
    }

    private static void AddFighterTalentSkillBoosts(Fighter fighter, List<Talent> selectedTalents)
    {
        foreach (var talentSkill in fighter.TalentSkills)
        {
            var pointsInSelection = selectedTalents
                .Where(x => x.TalentTreeName == talentSkill.TalentTree.TalentTreeName)
                .Sum(x => x.TalentPointCost);

            if (pointsInSelection >= 50)
            {
                talentSkill.Boosts.ForEach(x => x.Source = "TalentSkill");
                var talent = new Talent
                {
                    Boosts = talentSkill.Boosts
                };

                selectedTalents.Add(talent);
            }
        }
    }

    private static void AddOtherBoosts(List<Talent> selectedTalents)
    {
        // TODO: Improve this so it only chooses the best display buffs based on the army rather than all display values
        var relicsRepository = new RelicBoostsRepository();
        var researchBoostsRepository = new ResearchBoostsRepository();
        
        var otherBoosts = new List<Boost>();
        otherBoosts.AddRange(relicsRepository.GetBoosts());
        otherBoosts.AddRange(researchBoostsRepository.GetBoosts());

        selectedTalents.Add(new Talent
        {
            Boosts = otherBoosts
        });
    }

    private bool IsApplicableBoost(Boost boost, Fighter fighter, Fighter deputyFighter, FightSimulationOptions fightOptions)
    {
        if (boost.ApplicabilityGroup == null)
            return true;
        
        var applicable = fightOptions.ApplicabilityGroups.Contains(boost.ApplicabilityGroup.Value);

        if (boost.DisabledInCannonMode && fightOptions.UseCannons)
            applicable = false;

        if (boost.BoostRestrictionType == BoostRestrictionType.WithOscar)
            applicable = fighter.Name == Oscar.GetFighter().Name || deputyFighter.Name == Oscar.GetFighter().Name;
        
        return applicable;
    }

    public ApplicabilityGroup GetApplicabilityGroup(Boost boost)
    {
        var group = boost.BoostRestrictionType switch
        {
            BoostRestrictionType.SeigeMode => ApplicabilityGroup.Siege,
            BoostRestrictionType.GatheringResources => ApplicabilityGroup.Gathering,
            BoostRestrictionType.AttackingCitiesOnly => ApplicabilityGroup.Siege,
            BoostRestrictionType.MapBattle => ApplicabilityGroup.MapBattle,
            BoostRestrictionType.LeadingRally => ApplicabilityGroup.Rally,
            BoostRestrictionType.HealthBelowHalf => ApplicabilityGroup.None,
            BoostRestrictionType.TwoSecondsAfterActiveSkillRelease => ApplicabilityGroup.None,
            BoostRestrictionType.HealthAbove70 => ApplicabilityGroup.None, 
            BoostRestrictionType.HealthBelow70 => ApplicabilityGroup.None, 
            BoostRestrictionType.HealthAbove90 => ApplicabilityGroup.None, 
            BoostRestrictionType.HealthAbove80 => ApplicabilityGroup.None, 
            BoostRestrictionType.HealthBelow80 => ApplicabilityGroup.None, 
            BoostRestrictionType.FirstFiveSecondsOfBattle => ApplicabilityGroup.None,
            BoostRestrictionType.FirstTenSecondsOfBattle => ApplicabilityGroup.None,
            BoostRestrictionType.FiveSecondsAfterActiveSkillRelease => ApplicabilityGroup.None,
            BoostRestrictionType.ThreeSecondsAfterHitByActiveSkill => ApplicabilityGroup.None,
            BoostRestrictionType.TenSecondsAfterLeavingCity => ApplicabilityGroup.None,
            BoostRestrictionType.ThreeSecondsAfterHealing => ApplicabilityGroup.None,
            BoostRestrictionType.ThreeSecondsAfterSuccessfulChance => ApplicabilityGroup.None,
            BoostRestrictionType.AttackingGatherers => ApplicabilityGroup.Gathering,
            BoostRestrictionType.Garrison => ApplicabilityGroup.Garrison,
            BoostRestrictionType.DefendingAgainstMultipleTroops => ApplicabilityGroup.None, 
            BoostRestrictionType.AttackingNeutralUnits => ApplicabilityGroup.AttackingNeutralUnits,
            BoostRestrictionType.MultipliedByAdjacentAllies => ApplicabilityGroup.None, 
            BoostRestrictionType.TwoSecondsAfterTakingSkillDamage => ApplicabilityGroup.None, 
            BoostRestrictionType.AfterTakingSkillDamage => ApplicabilityGroup.None, 
            BoostRestrictionType.TroopNumberGreaterThanEnemy => ApplicabilityGroup.None, 
            BoostRestrictionType.AfterNormalAttack => ApplicabilityGroup.None, 
            BoostRestrictionType.AfterActiveSkillRelease => ApplicabilityGroup.None, 
            BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease => ApplicabilityGroup.None, 
            BoostRestrictionType.NotDisguised => ApplicabilityGroup.None, 
            BoostRestrictionType.AttackedByMultipleEnemies => ApplicabilityGroup.None, 
            BoostRestrictionType.AgainstRallys => ApplicabilityGroup.Garrison, 
            BoostRestrictionType.WithOscar => ApplicabilityGroup.None,
            null => ApplicabilityGroup.None,
            _ => throw new NotImplementedException()
        };

        return group;
    }

    public double GetStatBoosts(IEnumerable<BoostGrouping> boostGroupings, Func<BoostGrouping, bool> filter)
    {
        return boostGroupings
            .FirstOrDefault(filter)
            ?.TotalMaxBoostAmount ?? 0;
    }

    public UnitBoosts GetUnitBoosts(List<BoostGrouping> boostGroupings, TroopType troopType)
    {
        var unitBoosts = boostGroupings
            .Where(x => x.TroopRestriction == troopType || x.TroopRestriction == null);
        
        return new UnitBoosts
        {
            AttackBoostPercent = GetStatBoosts(unitBoosts, x => x.BoostType == BoostType.IncreasedAttack),
            DefenceBoostPercent = GetStatBoosts(unitBoosts, x => x.BoostType == BoostType.IncreasedDefence),
            HealthBoostPercent = GetStatBoosts(unitBoosts, x => x.BoostType == BoostType.IncreasedHealth),
            Damage = GetStatBoosts(unitBoosts, x => x.BoostType == BoostType.IncreasedDamage),
            Counter = GetStatBoosts(unitBoosts, x => x.BoostType == BoostType.IncreasedDamageToCounteredUnit),
            TroopType = troopType
        };
    }
}