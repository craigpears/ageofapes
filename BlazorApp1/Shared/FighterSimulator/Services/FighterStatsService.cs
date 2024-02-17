using System.Diagnostics;

namespace BlazorApp1.Shared.FighterSimulator;

public class FighterStatsService
{
    public List<FighterConfiguration> GetConfigurationsForFighter(Fighter fighter, FightSimulationOptions fightOptions)
    {
        // TODO: Can expand this idea to what's the best combination of research, relics, equipment etc. if they're all standardised into one kind of cost unit
        // TODO: Needs implementing to show best configs of different types by doing battle sims, like best map config, best seige config, best talent leap config, best group fight config
        Debug.WriteLine($"Getting configurations for {fighter.Name}");
        var allTalentCombinations = GetAllPossibleCombinations(fighter, new List<int> { 100 });
        var allConfigurations = new List<FighterConfiguration>();
        
        Debug.WriteLine($"Calculating army boosts");

        foreach (var combination in allTalentCombinations)
        {
            var config = new FighterConfiguration
            {
                Fighter = fighter,
                SelectedTalents = combination,
                ArmyBoosts = BuildArmyBoosts(fighter, combination, fightOptions)
            };
            
            allConfigurations.Add(config);
        }

        return allConfigurations;
    }

    public List<List<Talent>> GetAllPossibleCombinations(Fighter fighter, List<int> desiredTalentTotals)
    {
        var combinations = fighter
            .TalentSkills
            .Select(x => new List<Talent> { x.TalentTree })
            .ToList();

        var nextCombinations = new List<List<Talent>>();

        foreach (var combination in combinations)
        {
            nextCombinations.AddRange(GetTreeCombinations(combination));
        }
        
        combinations.AddRange(nextCombinations);

        var allPermutations = GetAllPermutations(combinations, desiredTalentTotals);
        Debug.WriteLine($"{allPermutations.Count()} permutations found");

        return allPermutations;
    }

    public List<List<Talent>> GetTreeCombinations(Talent rootTalent) =>
        GetTreeCombinations(new List<Talent> { rootTalent });
    
    protected List<List<Talent>> GetTreeCombinations(List<Talent> talents)
    {
        Debug.WriteLine($"Getting next combinations for {talents.Count()} talents");
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
            Debug.WriteLine("Adding required talent");
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
            Debug.WriteLine("Adding optional talent");
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
            Debug.WriteLine("Traversing second side of tree");
            var copiedList = CopyTalentsList(talents);
            copiedList.Add(otherSideOfTree);
            var combinationsWithOtherTreeHalf = GetTreeCombinations(copiedList);
            nextCombinations.AddRange(combinationsWithOtherTreeHalf);
        }

        // Order them by depth so it's easier to filter out duplicates later
        var orderedTalents = talents.OrderBy(x => x.TalentId).ToList();
        nextCombinations.Add(orderedTalents);
        Debug.WriteLine("Finished traversing tree");

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
        var distinctTalentTreeCount = talentLists.Select(x => x.First().RootTalent.TalentTreeName).Distinct().Count();
        
        // Try to minimise wasted work by only looking for permutations that use all talent points
        var talentListDictionary = talentLists.GroupBy(x => x.Sum(t => t.TalentPointCost)).ToList();
        var i = 0;

        foreach (var list in talentListDictionary)
        {
            var size = list.Key;
            var values = list.ToList();
            
            Debug.WriteLine($"Getting all permutations with {values.Count()} lists of size {size} ({++i}/{talentListDictionary.Count()})");

            foreach (var value in values)
            {
                var talentTreeName = value.First().RootTalent.TalentTreeName;
                
                
                    
                foreach (var desiredSize in desiredSizes.Where(x => x >= size))
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
                        .Where(x => x.First().RootTalent.TalentTreeName != talentTreeName || distinctTalentTreeCount == 1)
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
        // TODO: need to make sure all the boost restrictions are implemented properly
        // TODO: variable ones like above x health should be modelled like active skills 
        foreach (var fighterSkill in fighter.FighterSkills.Where(x => x.FighterSkillType == FigherSkillType.Passive))
        {
            var applicableBoosts = fighterSkill
                .Boosts
                .Where(x => x.BoostRestrictionType != BoostRestrictionType.SeigeMode || fightOptions.Seige)
                .Where(x => !x.DisabledInCannonMode || !fightOptions.UseCannons)
                .ToList();
        }
        
        foreach (var talentSkill in fighter.TalentSkills)
        {
            var pointsInSelection = selectedTalents
                .Where(x => x.RootTalent == talentSkill.TalentTree)
                .Sum(x => x.TalentPointCost);

            if (pointsInSelection >= 50)
            {
                var talent = new Talent
                {
                    Boosts = talentSkill.Boosts,
                    RootTalent = talentSkill.TalentTree
                };
                
                selectedTalents.Add(talent);
            }
        }
        
        // TODO: Need to filter out based on context like seige/map battle etc
        // TODO: Implement increased rage talents/boosts
        var boosts = new ArmyBoosts
        {
            UnitBoosts = new List<UnitBoosts>
            {
                GetUnitBoosts(selectedTalents, TroopType.Hitter), 
                GetUnitBoosts(selectedTalents, TroopType.Pilot),
                GetUnitBoosts(selectedTalents, TroopType.Shooter),
                GetUnitBoosts(selectedTalents, TroopType.WallBreaker)
            },
            DamageDealtByNormalAttacks = GetStatBoosts(selectedTalents, x => x.BoostType == BoostType.IncreasedNormalAttackDamage),
            DamageDealtBySkillsPercentIncrease = GetStatBoosts(selectedTalents, x => x.BoostType == BoostType.IncreasedSkillDamage),
            DamageDealtByCounterAttacks = GetStatBoosts(selectedTalents, x => x.BoostType == BoostType.IncreasedCounterAttackDamage),
            IncreasedMaxTroopsPercent = selectedTalents.SelectMany(x => x.Boosts.Where(x => x.BoostType == BoostType.IncreasedMaxTroops)).Sum(x => x.MaxBoostAmount),
        };
        
        // TODO: Add in talent and fighter skills in a simplified way somehow

        return boosts;
    }
    
    
    

    public double GetStatBoosts(List<Talent> talents, Func<Boost, bool> filter)
    {
        return talents
            .SelectMany(x => x.Boosts)
            .Where(filter)
            .Select(x => x.BoostAmounts.Max())
            .Sum();
    }

    public UnitBoosts GetUnitBoosts(List<Talent> talents, TroopType troopType)
    {
        var filteredTalents = talents
            .Where(x => x.Boosts.Any(b => b.TroopRestriction == troopType || b.TroopRestriction == null))
            .ToList();
        
        return new UnitBoosts
        {
            AttackBoostPercent = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedAttack),
            DefenceBoostPercent = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedDefence),
            HealthBoostPercent = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedHealth),
            Damage = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedDamage),
            Counter = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedDamageToCounteredUnit),
            TroopType = troopType
        };
    }
}