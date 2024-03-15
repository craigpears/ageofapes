using System.Diagnostics;
using BlazorApp1.Shared.FighterSimulator.Extensions;
using BlazorApp1.Shared.FighterSimulator.Scenarios;

namespace BlazorApp1.Shared.FighterSimulator;

public class FightSimulationService
{
    public static int RageOnNormalAttack = 90;
    public static int RageOnCounterAttack = 45;
    
    // TODO: Improve this into a set of parameters to support fighters that do better in 3v3 fights, modified mutants, defending, 1v1 map fights etc.
    public void SimulateFight(
            FightScenario scenario, 
            FighterStatsService statsService
        )
    {
        
        var repo = new FightersRepository();
        
        var fighters = repo.GetFighters();
        
        
        foreach (var fighter in fighters)
        {
            var results = new List<AttackResult>();
            var otherFighters = fighters.Where(x => x != fighter).ToList();
            foreach (var deputyFighter in otherFighters)
            {
                // Test with each possible choice of talent on the deputy
                for (var selectedTalent = 0; selectedTalent < 3; selectedTalent++)
                {
                    var comboResults = new List<AttackResult>();
                    var configurations = statsService.GetConfigurationsForFighter(fighter, deputyFighter, selectedTalent, scenario.FightSimulationOptions);
                    Debug.WriteLine($"Simulating attacks");
                    
                    foreach (var configuration in configurations)
                    {            
                        var attackResult = SimulateCityAttack(scenario.YourArmyFunc(configuration), scenario.EnemyArmyFunc(configuration), scenario.FightSimulationOptions);
                        attackResult.FightOptions = scenario.FightSimulationOptions;
                        comboResults.Add(attackResult);
                    }
                    
                    var bestResult = comboResults.GetBestResult();
                    results.Add(bestResult);
                }
            }
            
            Debug.WriteLine($"Saving results for {fighter}");
            scenario.SaveResults(results);
            
        }

        scenario.FlushResults();

    }

    public AttackResult SimulateCityAttack(Func<Army, Army, Army> yourArmyFunc, Func<Army, Army, Army> enemyArmyFunc, FightSimulationOptions options)
    {
        var yourArmy = RefreshArmy(yourArmyFunc, null, null, options);
        var enemyArmy = RefreshArmy(enemyArmyFunc, null, null, options);

        var cannonAttack = yourArmy.Troops.All(x => x.TroopType == TroopType.WallBreaker) && options.UseCannons;

        var attackResult = new AttackResult
        {
            YourArmy = yourArmy,
            EnemyArmy = enemyArmy
        };
        
        while (   yourArmy.GarrisonTroops.Any(x => x.Count > 0 || x.RefreshRoundsLeft != null) 
               && enemyArmy.GarrisonTroops.Any(x => x.Count > 0 || x.RefreshRoundsLeft != null))
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
            // TODO: Implement silence target boost
            // TODO: Implement IncreasedDamageFromCounteringUnit
            // TODO: Implement CrowdControlShield boost type (immune from intimidation, silence, speed reduction)
            // TODO: Implement TwoSecondsAfterTakingSkillDamage, AfterTakingSkillDamage
            var log = new AttackLog
            {
                YourDamageFactor = yourArmy.Troops.Average(x => x.CalculatedAttack) / enemyArmy.Troops.Average(x => x.CalculatedDefence),
                EnemyDamageFactor = enemyArmy.Troops.Average(x => x.CalculatedAttack) / yourArmy.Troops.Average(x => x.CalculatedDefence)
            };

            var yourTroopExtraDamage = yourArmy.Troops.Average(x => x.CalculatedDamageBoost);
            var enemyTroopExtraDamage = enemyArmy.Troops.Average(x => x.CalculatedDamageBoost);

            var yourDamageFactor = 1.0 + ((yourArmy.ArmyBoosts.DamageDealtByNormalAttacks + yourTroopExtraDamage) / 100);
            var enemyDamageFactor = 1.0 + ((enemyArmy.ArmyBoosts.DamageDealtByNormalAttacks + enemyTroopExtraDamage) / 100);

            log.YourDamage = log.YourDamageFactor * yourArmy.Troops.Sum(x => x.Count) * yourDamageFactor;
            log.EnemyDamage = log.EnemyDamageFactor * enemyArmy.Troops.Sum(x => x.Count) * enemyDamageFactor;

            log.YourTotalHealth = yourArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            log.EnemyTotalHealth = enemyArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            
            // TODO: Not worrying about wounded yet
            log.EnemyLostTroops = ProcessArmyLosses(enemyArmy, log.YourDamage);
            
            if (!options.UseShooterUnitSkill && (!options.UseCannons || yourArmy.FighterConfiguration.Fighter.Name == "Derrick"))
            {
                // TODO: Implement different logic for counter attacks
                yourArmy.RageLevel += RageOnNormalAttack;
            }
            
            var yourSkillsResult = ProcessSkills(yourArmy, enemyArmy, options);
            log.YourSkillDamage = yourSkillsResult.TotalDamage;
            log.EnemyLostTroops += yourSkillsResult.TroopsKilled;
            
            // TODO: If scenarios get more complex will need to simulate cannons and shooter skills having half attack rates
            if (!cannonAttack && !options.UseShooterUnitSkill)
            {
                log.YourLostTroops = ProcessArmyLosses(yourArmy, log.EnemyDamage);
                
                enemyArmy.RageLevel += RageOnNormalAttack;
                var defenderSkillsResult = ProcessSkills(yourArmy, yourArmy, options);
            }
            // TODO: Implement temporary boosts, like x% chance and those active after an active skill release
            // TODO: Implement ThreeSecondsAfterSuccessfulChance restriction type 
            
            // TODO: Implement healing
            
            attackResult.AttackLogs.Add(log);
            
            yourArmy = RefreshArmy(yourArmyFunc, yourArmy, enemyArmy, options);
            enemyArmy = RefreshArmy(enemyArmyFunc, enemyArmy, yourArmy, options); 
        }

        return attackResult;
    }

    private Army RefreshArmy(Func<Army, Army, Army> armyFunc, Army currentArmy, Army enemyArmy, FightSimulationOptions options)
    {
        // TODO: Do things in this refresh loop like updating army buffs based on percentage troops alive, random chance events, x second events etc.
        // TODO: Pass in the opposing army so you can simulate healing, refreshing reinforcements etc
        var shouldGenerateArmy = currentArmy == null || options.RecalculateArmies;
        var newArmy = shouldGenerateArmy ? armyFunc(currentArmy, enemyArmy) : currentArmy;

        if (newArmy != currentArmy)
        {
            newArmy.ArmyBoosts.AddGearBoosts(newArmy.Troops);
        }

        foreach (var troop in newArmy.Troops)
        {
            TroopType? counteredTroopType = troop.TroopType switch
            {
                TroopType.Hitter => TroopType.Pilot,
                TroopType.Pilot => TroopType.Shooter,
                TroopType.Shooter => TroopType.Hitter,
                _ => null
            };
            
            // TODO: This only works if there is only one troop type, needs improving
            var useCounterBoosts = enemyArmy != null && enemyArmy.Troops.Any(x => x.TroopType == counteredTroopType);
            
            var applicableBoosts = new List<Boost>();

            troop.CalculateStats(newArmy.ArmyBoosts, useCounterBoosts);
        }
        
        return newArmy;
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
            InflictSkillDamage(attackingArmy, defendingArmy, skillDamageFactor, activeSkill.MaxTargets, result);

            // TODO: Implement RestoreRageAfterActiveSkill boost type
            // TODO: Implement ChanceToAttackTwice
            // TODO: Implement skills being able to hit multiple players
            // TODO: Implement additional damage chance and factor
            // TODO: Implement increased master fighter skill damage and IncreasedDeputyFighterSkillDamage
        }

        var passiveSkills = attackingArmy.ArmyBoosts.ApplicableBoosts
            .Where(x => x.BoostType == BoostType.SkillDamageFactor)
            .ToList();

        foreach (var passiveSkill in passiveSkills)
        {
            var rand = new Random();
            var randomNumber = rand.Next(0, 100);
            var damageFactor = (int)passiveSkill.MaxBoostAmount;
            if (randomNumber >= passiveSkill.Chance)
            {
                InflictSkillDamage(attackingArmy, defendingArmy, damageFactor, 1, result);
            }
        }

        return result;
    }

    private static void InflictSkillDamage(Army attackingArmy, Army defendingArmy, int skillDamageFactor, int maxTargets,
        ProcessSkillsResult result)
    {
        var targets = Math.Min(defendingArmy.NumberOfPlayers, Math.Max(maxTargets, 1));
        var skillTotalDamage = (skillDamageFactor / defendingArmy.Troops.Average(x => x.CalculatedDefence))
                               * (1 + (attackingArmy.ArmyBoosts.DamageDealtBySkillsPercentIncrease / 100))
                               * attackingArmy.Troops.Sum(x => x.Count)
                               * targets;

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
            troop.LossesSinceLastRefresh += losses;
        }

        return totalLosses;
    }
}

internal class ProcessSkillsResult
{
    public int TotalDamage { get; set; }
    public int TroopsKilled { get; set; }
}