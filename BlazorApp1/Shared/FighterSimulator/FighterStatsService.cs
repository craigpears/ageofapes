using System.Diagnostics;

namespace BlazorApp1.Shared.FighterSimulator;

public class FighterStatsService
{
    public List<FighterConfiguration> GetConfigurationsForFighter(Fighter fighter, FightSimulationOptions fightOptions)
    {
        // TODO: Can expand this idea to what's the best combination of research, relics, equipment etc. if they're all standardised into one kind of cost unit
        // TODO: Needs implementing to show best configs of different types by doing battle sims, like best map config, best seige config, best talent leap config, best group fight config
        Debug.Write($"Getting configurations for {fighter.Name}");
        var allTalentCombinations = GetAllPossibleCombinations(fighter);
        var allConfigurations = new List<FighterConfiguration>();
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

    private List<List<Talent>> GetAllPossibleCombinations(Fighter fighter)
    {
        // TODO: Probably should be writing test for something as complex as this...
        var combinations = fighter
            .TalentSkills
            .Select(x => new List<Talent> { x.RootTalent })
            .ToList();

        var nextCombinations = new List<List<Talent>>();

        foreach (var combination in combinations)
        {
            nextCombinations.AddRange(GetNextCombinations(combination));
        }
        
        // TODO: Can likely get some duplicates from this if there are multiple journeys to the same result, so this needs deduplicating
        combinations.AddRange(nextCombinations);
        
        return combinations;
    }

    private List<List<Talent>> GetNextCombinations(List<Talent> talents)
    {
        Debug.WriteLine($"Getting next combinations for {talents.Count()} talents");
        var lastTalent = talents.OrderBy(x => x.TalentDepth).Last();
        if (talents.Count() == 100)
            return new List<List<Talent>>();
        
        if (talents.Count() == 1)
        {
            var trees = lastTalent.NextTalents;
            var root = talents.Single();
            var leftTree = trees.First();
            var rightTree = trees.Skip(1).First();

            var leftTreeCombinations = GetNextCombinations(new List<Talent> { root, leftTree });
            var rightTreeCombinations = GetNextCombinations(new List<Talent> { root, rightTree });

            var returnList = new List<List<Talent>>();
            returnList.AddRange(leftTreeCombinations);
            returnList.AddRange(rightTreeCombinations);
            
            // TODO: At this point will just return all permutations of one side of the tree, won't combine sides yet
            return returnList;
        }

        var nextCombinations = new List<List<Talent>>();
        
        // Do the traversal in a very controlled linear way so we don't end up with duplicate results or effort
        // Follow the required tree only
        var nextRequiredTalent = lastTalent.NextTalents.FirstOrDefault(x => !x.Optional);
        if (nextRequiredTalent != null)
        {
            Debug.WriteLine("Adding required talent");
            var copiedList = CopyTalentsList(talents);
            copiedList.Add(nextRequiredTalent);
            var combinationsWithoutOptionalTalent = GetNextCombinations(copiedList);
            nextCombinations.AddRange(combinationsWithoutOptionalTalent);
        }
        
        // Add optional talents in this path
        var optionalTalent = lastTalent.NextTalents.FirstOrDefault(x => x.Optional);
        if (optionalTalent != null)
        {
            Debug.WriteLine("Adding optional talent");
            var copiedList = CopyTalentsList(talents);
            copiedList.Add(optionalTalent);
            var combinationsWithOptionalTalent = GetNextCombinations(copiedList);
            nextCombinations.AddRange(combinationsWithOptionalTalent);
        }
        
        nextCombinations.Add(talents);
        
        return nextCombinations;
    }

    private List<Talent> CopyTalentsList(List<Talent> talents)
    {
        var newList = new List<Talent>();
        newList.AddRange(talents);
        return newList;
    }

    public ArmyBoosts BuildArmyBoosts(Fighter fighter, List<Talent> selectedTalents, FightSimulationOptions fightOptions)
    {
        // TODO: Need to filter out based on context like seige/map battle etc
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
            DamageDealtBySkills = GetStatBoosts(selectedTalents, x => x.BoostType == BoostType.IncreasedSkillDamage),
            DamageDealtByCounterAttacks = GetStatBoosts(selectedTalents, x => x.BoostType == BoostType.IncreasedCounterAttackDamage),
        };
        
        // TODO: Add in talent and fighter skills in a simplified way somehow

        return boosts;
    }
    
    public static IEnumerable<T> Traverse<T>(IEnumerable<T> items, 
        Func<T, IEnumerable<T>> childSelector)
    {
        var stack = new Stack<T>(items);
        while(stack.Any())
        {
            var next = stack.Pop();
            yield return next;
            foreach(var child in childSelector(next))
                stack.Push(child);
        }
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
            Attack = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedAttack),
            Defence = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedDefence),
            Health = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedHealth),
            Damage = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedDamage),
            Counter = GetStatBoosts(filteredTalents, x => x.BoostType == BoostType.IncreasedDamageToCounteredUnit),
            TroopType = troopType
        };
    }
}