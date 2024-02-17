namespace BlazorApp1.Shared.FighterSimulator;

public class FightSimulationService
{
    public static int RageOnNormalAttack = 90;
    public static int RageOnCounterAttack = 45;
    
    
    public void SimulateFight(Army armyOne, Army armyTwo) // TODO: Change this into a set of parameters to support fighters that do better in 3v3 fights etc.
    {
        // TODO: Can start with attacking a city, simply dealing damage as the first simulation.  Would effectively be like Attack on Dr. Hogg
        // TODO: Simulate options with/without unit skills active
    }

    public AttackResult SimulateCityAttack(Army attackingArmy, Army defendingArmy, FightSimulationOptions options)
    {
        // TODO: Simulate sentry tower attack, health and protection, city walls etc.
        // TODO: Support simulating skills
        
        // TODO: Need to improve this code so it doesn't modify the original models if doing anything more complicated
        attackingArmy.ArmyBoosts.AddGearBoosts(attackingArmy.Troops);
        defendingArmy.ArmyBoosts.AddGearBoosts(defendingArmy.Troops);
        
        attackingArmy.Troops.ForEach(x => x.CalculateStats(attackingArmy.ArmyBoosts));
        defendingArmy.Troops.ForEach(x => x.CalculateStats(defendingArmy.ArmyBoosts));

        var cannonAttack = attackingArmy.Troops.All(x => x.TroopType == TroopType.WallBreaker) && options.UseCannons;

        var attackResult = new AttackResult
        {
            AttackingArmy = attackingArmy,
        };
        
        while (attackingArmy.Troops.Any(x => x.Count > 0) && defendingArmy.Troops.Any(x => x.Count > 0))
        {
            // TODO: Not worrying about counter unit type damage yet
            // TODO: No idea how things work if there are a mix of troops - evidence seems to show it's spread evenly by unit count since units rarely get wiped out
            // TODO: Not got troop level damage yet, should divide by defence at the troop level?
            // TODO: Not thinking about normal vs counter attacks yet
            // TODO: Should have various damage factor multipliers in here as well
            var log = new AttackLog
            {
                AttackerDamageFactor = attackingArmy.Troops.Average(x => x.CalculatedAttack) / defendingArmy.Troops.Average(x => x.CalculatedDefence),
                DefenderDamageFactor = defendingArmy.Troops.Average(x => x.CalculatedAttack) / attackingArmy.Troops.Average(x => x.CalculatedDefence)
            };

            // TODO: This needs implementing
            var attackerDamageMultiplier = 1.0;
            var defenderDamageMultiplier = 1.0;

            log.AttackerDamage = log.AttackerDamageFactor * attackingArmy.Troops.Sum(x => x.Count) * attackerDamageMultiplier;
            log.DefenderDamage = log.DefenderDamageFactor * defendingArmy.Troops.Sum(x => x.Count) * defenderDamageMultiplier;

            log.AttackerTotalHealth = attackingArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            log.DefenderTotalHealth = defendingArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            
            // TODO: Not worrying about wounded yet
            log.DefenderLostTroops = ProcessArmyLosses(defendingArmy, log.AttackerDamage);
            
            
            
            // TODO: Implement different logic for counter attacks
            attackingArmy.RageLevel += RageOnNormalAttack;

            var attackerSkillsResult = ProcessSkills(attackingArmy, defendingArmy, options);
            log.AttackerSkillDamage = attackerSkillsResult.TotalDamage;
            
            if (!cannonAttack)
            {
                log.AttackerLostTroops = ProcessArmyLosses(attackingArmy, log.DefenderDamage);
            }

            defendingArmy.RageLevel += RageOnNormalAttack;
            var defenderSkillsResult = ProcessSkills(attackingArmy, attackingArmy, options);

            // TODO: Implement temporary boosts, like x% chance and those active after an active skill release
            
            // TODO: Implement healing
            
            attackResult.AttackLogs.Add(log);
        }

        return attackResult;
    }

    private static ProcessSkillsResult ProcessSkills(Army attackingArmy, Army defendingArmy, FightSimulationOptions options)
    {
        var result = new ProcessSkillsResult();
        
        var activeSkill =
            attackingArmy.FighterConfiguration.Fighter.FighterSkills.Single(x =>
                x.FighterSkillType == FigherSkillType.Active);
        if (attackingArmy.RageLevel >= activeSkill.RageRequired)
        {
            attackingArmy.RageLevel -= activeSkill.RageRequired;
            attackingArmy.RageLevel -= activeSkill.RageRequired;
            var skillDamageFactor = options.UseCannons
                ? activeSkill.CannonDamageFactor ?? activeSkill.DamageFactor
                : activeSkill.DamageFactor;

            // TODO: Implement takes less skill damage talents
            // TODO: Implement enemy has less skill damage
            // TODO: Work out what, if any, the difference is between the above two things
            var skillTotalDamage = (skillDamageFactor / defendingArmy.Troops.Average(x => x.CalculatedDefence))
                                   * (1 + (attackingArmy.ArmyBoosts.DamageDealtBySkillsPercentIncrease / 100))
                                   * attackingArmy.Troops.Sum(x => x.Count);

            result.TotalDamage = (int)skillTotalDamage;
            result.TroopsKilled += ProcessArmyLosses(defendingArmy, skillTotalDamage);
            
            // TODO: Implement RestoreRageAFterActiveSkill boost type
        }

        return result;
    }

    private static int ProcessArmyLosses(Army army, double damage)
    {
        // From testing in game damage looks to be evenly spread out for each unit, so higher count units take more of the damage
        int totalLosses = 0;
        var totalArmySize = (double)army.Troops.Sum(x => x.Count);
        var remainingTroops = army.Troops.Where(x => x.Count > 0).ToList();
        foreach (var troop in remainingTroops)
        {
            var percentageOfArmySize = troop.Count / totalArmySize;
            var damageTaken = damage * percentageOfArmySize;

            var losses = (int)(damageTaken / troop.CalculatedHealth);
            losses = Math.Min(losses, troop.Count);
            totalLosses += losses;
            troop.Count -= losses;
        }

        return totalLosses;
    }
}

internal class ProcessSkillsResult
{
    public int TotalDamage { get; set; }
    public int TroopsKilled { get; set; }
}

public class FightSimulationOptions
{
    public bool MapBattle { get; set; }
    public bool Seige { get; set; }
    public bool UseCannons { get; set; }
}