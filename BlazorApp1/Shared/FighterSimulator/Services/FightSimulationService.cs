using System.Diagnostics;

namespace BlazorApp1.Shared.FighterSimulator;

public class FightSimulationService
{
    public static int RageOnNormalAttack = 90;
    public static int RageOnCounterAttack = 45;
    
    // TODO: Improve this into a set of parameters to support fighters that do better in 3v3 fights, modified mutants, defending, 1v1 map fights etc.
    public List<AttackResult> SimulateFight(
            FightSimulationOptions fightOptions, 
            FighterStatsService statsService,
            Func<FighterConfiguration, Army> attackingArmyFunc,
            Func<FighterConfiguration, Army> defendingArmyFunc
        )
    {
        
        var repo = new FightersRepository();
        
        var fighters = repo.GetFighters();
        var results = new List<AttackResult>();
        foreach (var fighter in fighters)
        {
            var configurations = statsService.GetConfigurationsForFighter(fighter, fightOptions);
            var i = 0;
            foreach (var configuration in configurations)
            {
                if (i++ % 10000 == 0)
                {
                    Debug.WriteLine($"Simulating attack {i}/{configurations.Count()}");
                }
                
                var attackResult = SimulateCityAttack(attackingArmyFunc(configuration), defendingArmyFunc(configuration), fightOptions);
                attackResult.FightOptions = fightOptions;
                results.Add(attackResult);
            }
            
        }

        return results;
    }

    public AttackResult SimulateCityAttack(Army attackingArmy, Army defendingArmy, FightSimulationOptions options)
    {
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
            // TODO: Need to implement attack vs counter unit type damage when this gets improved
            // TODO: Need to implement ReduceEnemyDefence boost
            // TODO: Need to implement DamageTakenReduced
            // TODO: Need to implement DefendingAgainstMultipleTroops
            // TODO: Implement IncreasedNormalAttackDamage
            var log = new AttackLog
            {
                AttackerDamageFactor = attackingArmy.Troops.Average(x => x.CalculatedAttack) / defendingArmy.Troops.Average(x => x.CalculatedDefence),
                DefenderDamageFactor = defendingArmy.Troops.Average(x => x.CalculatedAttack) / attackingArmy.Troops.Average(x => x.CalculatedDefence)
            };

            var attackerDamageMultiplier = 1.0 + (attackingArmy.ArmyBoosts.DamageDealtByNormalAttacks / 100);
            var defenderDamageMultiplier = 1.0 + (defendingArmy.ArmyBoosts.DamageDealtByNormalAttacks / 100);

            log.AttackerDamage = log.AttackerDamageFactor * attackingArmy.Troops.Sum(x => x.Count) * attackerDamageMultiplier;
            log.DefenderDamage = log.DefenderDamageFactor * defendingArmy.Troops.Sum(x => x.Count) * defenderDamageMultiplier;

            log.AttackerTotalHealth = attackingArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            log.DefenderTotalHealth = defendingArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            
            // TODO: Not worrying about wounded yet
            log.DefenderLostTroops = ProcessArmyLosses(defendingArmy, log.AttackerDamage);
            
            
            
            // TODO: Implement different logic for counter attacks
            attackingArmy.RageLevel += RageOnNormalAttack;

            if (!options.UseShooterUnitSkill && (!options.UseCannons || attackingArmy.FighterConfiguration.Fighter.Name == "Derrick"))
            {
                var attackerSkillsResult = ProcessSkills(attackingArmy, defendingArmy, options);
                log.AttackerSkillDamage = attackerSkillsResult.TotalDamage;
            }
            
            // TODO: If scenarios get more complex will need to simulate cannons and shooter skills having half attack rates
            if (!cannonAttack && !options.UseShooterUnitSkill)
            {
                log.AttackerLostTroops = ProcessArmyLosses(attackingArmy, log.DefenderDamage);
                
                defendingArmy.RageLevel += RageOnNormalAttack;
                var defenderSkillsResult = ProcessSkills(attackingArmy, attackingArmy, options);
            }
            // TODO: Implement temporary boosts, like x% chance and those active after an active skill release
            // TODO: Implement ThreeSecondsAfterSuccessfulChance restriction type 
            
            // TODO: Implement healing
            
            attackResult.AttackLogs.Add(log);
        }

        return attackResult;
    }

    private static ProcessSkillsResult ProcessSkills(Army attackingArmy, Army defendingArmy, FightSimulationOptions options)
    {

        if (options.UseShooterUnitSkill)
            return new ProcessSkillsResult();
        
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
            // TODO: Need to confirm this is right - do unit stats and attack really not factor into skill damage at all?
            // TODO: Implement passive skill chance
            InflictSkillDamage(attackingArmy, defendingArmy, skillDamageFactor, result);

            // TODO: Implement RestoreRageAfterActiveSkill boost type
            // TODO: Implement ChanceToAttackTwice
            // TODO: Implement skills being able to hit multiple players
            // TODO: Implement additional damage chance and factor
            // TODO: Implement increased master fighter skill damage and IncreasedDeputyFighterSkillDamage
        }

        var passiveSkills = attackingArmy.FighterConfiguration.Fighter.FighterSkills
            .SelectMany(x => x.Boosts)
            .Where(x => x.BoostType == BoostType.SkillDamageFactor)
            .ToList();

        foreach (var passiveSkill in passiveSkills)
        {
            var rand = new Random();
            var randomNumber = rand.Next(0, 100);
            var damageFactor = (int)passiveSkill.MaxBoostAmount;
            if (randomNumber >= passiveSkill.Chance)
            {
                InflictSkillDamage(attackingArmy, defendingArmy, damageFactor, result);
            }
        }

        return result;
    }

    private static void InflictSkillDamage(Army attackingArmy, Army defendingArmy, int skillDamageFactor,
        ProcessSkillsResult result)
    {
        var skillTotalDamage = (skillDamageFactor / defendingArmy.Troops.Average(x => x.CalculatedDefence))
                               * (1 + (attackingArmy.ArmyBoosts.DamageDealtBySkillsPercentIncrease / 100))
                               * attackingArmy.Troops.Sum(x => x.Count);

        result.TotalDamage += (int)skillTotalDamage;
        result.TroopsKilled += ProcessArmyLosses(defendingArmy, skillTotalDamage);
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