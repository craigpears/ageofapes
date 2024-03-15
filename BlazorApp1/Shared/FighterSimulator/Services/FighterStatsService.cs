using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorApp1.Shared.FighterSimulator.Boosts;

namespace BlazorApp1.Shared.FighterSimulator;

public class FighterStatsService
{
    protected static Dictionary<string, List<List<Talent>>> _talentTreeCombinationsCache = new Dictionary<string, List<List<Talent>>>();
    protected string _cacheFilesDirectory = @"\\nas-pears\documents\AgeOfApes\FighterOutputs\Cache";
    
    public List<FighterConfiguration> GetConfigurationsForFighter(Fighter fighter, Fighter? deputyFighter, int selectedDeputyTalent, FightSimulationOptions fightOptions)
    {
        // TODO: Can expand this idea to what's the best combination of research, relics, equipment etc. if they're all standardised into one kind of cost unit
        // TODO: Needs implementing to show best configs of different types by doing battle sims, like best map config, best seige config, best talent leap config, best group fight config
        Debug.WriteLine($"Getting configurations for {fighter.Name} and {deputyFighter?.Name}-{selectedDeputyTalent}");
        var allTalentCombinations = GetAllPossibleCombinations(fighter, new List<int> { 100 });
        var allConfigurations = new List<FighterConfiguration>();
        
        Debug.WriteLine($"Calculating army boosts");

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
                combination.Add(deputyTalent);
            }

            
            
            var config = new FighterConfiguration
            {
                Fighter = fighter,
                DeputyFighter = deputyFighter,
                DeputySelectedTalent = selectedDeputyTalent,
                SelectedTalents = combination,
                ArmyBoosts = BuildArmyBoosts(fighter, combination, fightOptions)
            };
            
            allConfigurations.Add(config);
        }

        return allConfigurations;
    }

    public List<List<Talent>> GetAllPossibleCombinations(Fighter fighter, List<int> desiredTalentTotals)
    {
        var cacheFileName = $"{_cacheFilesDirectory}/{fighter.Name}Combinations.json";
        
        if (File.Exists(cacheFileName))
        {
            var cachedJson = File.ReadAllText(cacheFileName);
            var cachedCombinations = JsonSerializer.Deserialize<List<List<Talent>>>(cachedJson);
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
        
        var json = JsonSerializer.Serialize(combinations, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles});
        File.WriteAllText(cacheFileName, json);

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
        
        //Debug.WriteLine($"No cached tree combinations for {rootTalent.TalentTreeName}, building...");
        var combinations = GetTreeCombinations(rootTalent);
        _talentTreeCombinationsCache[rootTalent.TalentTreeName] = combinations;
        
        // Remove these large links before serialising
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

    public ArmyBoosts BuildArmyBoosts(Fighter fighter, List<Talent> selectedTalents, FightSimulationOptions fightOptions)
    {
        selectedTalents
            .SelectMany(x => x.Boosts)
            .ToList()
            .ForEach(x => x.Source = "Talent");

        foreach (var fighterSkill in fighter.FighterSkills.Where(x => x.FighterSkillType == FigherSkillType.Passive))
        {
            var applicableBoosts = fighterSkill
                .Boosts.Where(x => IsApplicableBoost(x, fightOptions)).ToList();
            
            applicableBoosts.ForEach(x => x.Source = "FighterSkill");
            
            var talent = new Talent
            {
                Boosts = applicableBoosts
            };
                
            selectedTalents.Add(talent);
        }
        
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

        // TODO: Implement increased rage talents/boosts
        var boosts = new ArmyBoosts
        {
            UnitBoosts = new List<UnitBoosts>
            {
                GetUnitBoosts(selectedTalents, TroopType.Hitter, fightOptions), 
                GetUnitBoosts(selectedTalents, TroopType.Pilot, fightOptions),
                GetUnitBoosts(selectedTalents, TroopType.Shooter, fightOptions),
                GetUnitBoosts(selectedTalents, TroopType.WallBreaker, fightOptions)
            },
            DamageDealtByNormalAttacks = GetStatBoosts(selectedTalents, fightOptions, x => x.BoostType == BoostType.IncreasedNormalAttackDamage || x.BoostType == BoostType.IncreasedDamage),
            DamageDealtBySkillsPercentIncrease = GetStatBoosts(selectedTalents, fightOptions, x => x.BoostType == BoostType.IncreasedSkillDamage || x.BoostType == BoostType.IncreasedDamage),
            DamageDealtByCounterAttacks = GetStatBoosts(selectedTalents, fightOptions, x => x.BoostType == BoostType.IncreasedCounterAttackDamage || x.BoostType == BoostType.IncreasedDamage),
            IncreasedMaxTroopsPercent = selectedTalents.SelectMany(x => x.Boosts.Where(x => x.BoostType == BoostType.IncreasedMaxTroops)).Sum(x => x.MaxBoostAmount),
            ApplicableBoosts = selectedTalents.SelectMany(x => x.Boosts).Where(x => IsApplicableBoost(x, fightOptions)).ToList()
        };

        return boosts;
    }

    private bool IsApplicableBoost(Boost boost, FightSimulationOptions fightOptions)
    {
        var applicable = boost.BoostRestrictionType switch
        {
            BoostRestrictionType.SeigeMode => fightOptions.Seige,
            BoostRestrictionType.GatheringResources => fightOptions.Gathering,
            BoostRestrictionType.AttackingCitiesOnly => fightOptions.Seige,
            BoostRestrictionType.MapBattle => fightOptions.MapBattle,
            BoostRestrictionType.LeadingRally => false, // TODO: This should be a fight option
            BoostRestrictionType.HealthBelowHalf => false, // TODO: need to improve this later
            BoostRestrictionType.TwoSecondsAfterActiveSkillRelease => false,
            BoostRestrictionType.HealthAbove70 => true, // TODO: Needs improving later
            BoostRestrictionType.HealthAbove90 => true, // TODO: need to improve this later
            BoostRestrictionType.HealthAbove80 => true, // TODO: need to improve this later
            BoostRestrictionType.HealthBelow80 => false, // TODO: need to improve this later
            BoostRestrictionType.FirstFiveSecondsOfBattle => false,
            BoostRestrictionType.FirstTenSecondsOfBattle => false,
            BoostRestrictionType.FiveSecondsAfterActiveSkillRelease => false,
            BoostRestrictionType.ThreeSecondsAfterHitByActiveSkill => false,
            BoostRestrictionType.TenSecondsAfterLeavingCity => false,
            BoostRestrictionType.ThreeSecondsAfterHealing => false,
            BoostRestrictionType.ThreeSecondsAfterSuccessfulChance => false,
            BoostRestrictionType.AttackingGatherers => fightOptions.Gathering,
            BoostRestrictionType.Garrison => fightOptions.Garrison,
            BoostRestrictionType.DefendingAgainstMultipleTroops => false, // TODO: This needs implementing
            BoostRestrictionType.AttackingNeutralUnits => fightOptions.AttackingNeutralUnits,
            BoostRestrictionType.MultipliedByAdjacentAllies => true, // TODO: This needs implementing
            BoostRestrictionType.TwoSecondsAfterTakingSkillDamage => false, // TODO: This needs implementing
            BoostRestrictionType.AfterTakingSkillDamage => false, // TODO: This needs implementing
            BoostRestrictionType.TroopNumberGreaterThanEnemy => true, // TODO: This needs implementing
            BoostRestrictionType.AfterNormalAttack => false, // TODO: This needs implementing
            BoostRestrictionType.AfterActiveSkillRelease => false, // TODO: This needs implementing
            BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease => false, // TODO: This needs implementing
            null => true,
            _ => throw new NotImplementedException()
        };

        if (boost.DisabledInCannonMode)
            applicable = false;
        
        return applicable;
    }


    public double GetStatBoosts(List<Talent> talents, FightSimulationOptions fightSimulationOptions, Func<Boost, bool> filter)
    {
        return talents
            .SelectMany(x => x.Boosts)
            .Where(x => IsApplicableBoost(x, fightSimulationOptions))
            .Where(filter)
            .Select(x => x.BoostAmounts.Max())
            .Sum();
    }

    public UnitBoosts GetUnitBoosts(List<Talent> talents, TroopType troopType, FightSimulationOptions fightSimulationOptions)
    {
        var filteredTalents = talents
            .Where(x => x.Boosts.Any(b => b.TroopRestriction == troopType 
                                          || (b.TroopRestriction == TroopType.ThreeUnitTypes && !fightSimulationOptions.UseCannons) 
                                          || b.TroopRestriction == null))
            .ToList();
        
        return new UnitBoosts
        {
            AttackBoostPercent = GetStatBoosts(filteredTalents, fightSimulationOptions, x => x.BoostType == BoostType.IncreasedAttack),
            DefenceBoostPercent = GetStatBoosts(filteredTalents, fightSimulationOptions, x => x.BoostType == BoostType.IncreasedDefence),
            HealthBoostPercent = GetStatBoosts(filteredTalents, fightSimulationOptions, x => x.BoostType == BoostType.IncreasedHealth),
            Damage = GetStatBoosts(filteredTalents, fightSimulationOptions, x => x.BoostType == BoostType.IncreasedDamage),
            Counter = GetStatBoosts(filteredTalents, fightSimulationOptions, x => x.BoostType == BoostType.IncreasedDamageToCounteredUnit),
            TroopType = troopType
        };
    }
}