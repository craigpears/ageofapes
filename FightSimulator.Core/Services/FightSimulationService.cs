using FightSimulator.Core.FighterSimulator.Extensions;
using FightSimulator.Core.FighterSimulator.Fighters;
using FightSimulator.Core.FighterSimulator.Scenarios;

namespace FightSimulator.Core.FighterSimulator.Services;

public class FightSimulationService
{
    public static int RageOnNormalAttack = 90;
    public static int RageOnCounterAttack = 45;
    public Random rand = new Random();
    
    // TODO: Improve this into a set of parameters to support fighters that do better in 3v3 fights, modified mutants, defending, 1v1 map fights etc.
    public void SimulateFight(
            FightScenario scenario, 
            FighterStatsService statsService
        )
    {
        
        var repo = new FightersRepository();
        var fighters = repo.GetFighters(scenario.RunOptions);
        var i = 0;
        
        foreach (var fighter in fighters.OrderBy(x => scenario.GetLastRanDate(x.Name)))
        {
            i++;
            Console.WriteLine($"{scenario.outputFolder} {DateTime.UtcNow.ToShortTimeString()} - Running for {fighter.Name} ({i}/{fighters.Count})");
            var results = new List<AttackResult>();
            var otherFighters = fighters.Where(x => x != fighter).ToList();
            var countToRun = otherFighters.Count * 3;
            var j = 0;
            foreach (var deputyFighter in otherFighters)
            {
                // Test with each possible choice of talent on the deputy
                for (var selectedTalent = 0; selectedTalent < 3; selectedTalent++)
                {
                    j++;
                    var comboResults = new List<AttackResult>();
                    Console.WriteLine($"{scenario.outputFolder} {DateTime.UtcNow.ToShortTimeString()} - Running for {fighter.Name} and {deputyFighter?.Name}-{selectedTalent} ({j}/{countToRun})");
                    var configurations = statsService.GetConfigurationsForFighter(fighter, deputyFighter, selectedTalent, scenario.FightSimulationOptions);

                    foreach (var configuration in configurations)
                    {
                        var attackResult = SimulateCityAttack(scenario.YourArmyFunc(configuration),
                            scenario.EnemyArmyFunc(configuration), scenario.FightSimulationOptions);
                        attackResult.FightOptions = scenario.FightSimulationOptions;
                        comboResults.Add(attackResult);
                    }
                    
                    var bestResult = comboResults.GetBestResult();
                    results.Add(bestResult);
                }
            }
            
            Console.WriteLine($"{scenario.outputFolder} {DateTime.UtcNow.ToShortTimeString()} - Saving results for {fighter.Name}");
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
        
        while (yourArmy.MainPlayerIsAlive && enemyArmy.MainPlayerIsAlive && (enemyArmy.AnyPlayerIsAlive || !options.RunToAllPlayersDead))
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
            // TODO: Implement continuous damage skill
            var log = new AttackLog();

            log.EnemyRoundLogData.AttackDefenceRatio = enemyArmy.Troops.Average(x => x.CalculatedAttack) /
                                                       yourArmy.Troops.Average(x => x.CalculatedDefence);

            
            
            var enemyCounterDamageBoost = enemyArmy.Troops.Average(x => x.CalculatedCounterDamageBoost).ToMultiplier();
            
            
            log.YourRoundLogData = CalculateRoundLogData(yourArmy, enemyArmy);
            log.EnemyRoundLogData  = CalculateRoundLogData(yourArmy, enemyArmy);


            
            // TODO: Not worrying about wounded yet
            log.EnemyRoundLogData.LostTroops = ProcessArmyLosses(enemyArmy, log.YourNormalDamage);
            
            // TODO: Implement different logic for counter attacks
            yourArmy.RageLevel += RageOnNormalAttack;
            
            
            var yourSkillsResult = ProcessSkills(yourArmy, enemyArmy, log.YourRoundLogData.BaseDamage, options);
            log.YourRoundLogData.SkillDamage = yourSkillsResult.TotalDamage;
            log.EnemyRoundLogData.LostTroops += yourSkillsResult.TroopsKilled;
            
            // TODO: If scenarios get more complex will need to simulate cannons and shooter skills having half attack rates
            if (!cannonAttack && !options.UseShooterUnitSkill)
            {
                log.YourRoundLogData.LostTroops = ProcessArmyLosses(yourArmy, log.EnemyRoundLogData.NormalDamage);
                
                enemyArmy.RageLevel += RageOnNormalAttack;
                var defenderSkillsResult = ProcessSkills(yourArmy, yourArmy, log.EnemyRoundLogData.BaseDamage, options);
            }
            // TODO: Implement temporary boosts, like x% chance and those active after an active skill release
            // TODO: Implement ThreeSecondsAfterSuccessfulChance restriction type 
            
            // TODO: Implement healing

            log.YourRoundLogData.TroopsRemaining = yourArmy.TotalTroopsCount;
            log.EnemyRoundLogData.TroopsRemaining = enemyArmy.TotalTroopsCount;
            
            attackResult.AttackLogs.Add(log);
            
            yourArmy = RefreshArmy(yourArmyFunc, yourArmy, enemyArmy, options);
            enemyArmy = RefreshArmy(enemyArmyFunc, enemyArmy, yourArmy, options); 
        }

        return attackResult;
    }

    private RoundLogData CalculateRoundLogData(Army yourArmy, Army enemyArmy)
    {
        // TODO: This formula seems different than real examples but is at least proportionate, might need to multiply by power instead of count?
        // TODO: Need to work out how counter damage works when there are multiple troop types
        var roundLogData = new RoundLogData();

        roundLogData.AttackDefenceRatio = yourArmy.Troops.Average(x => x.CalculatedAttack) /
                                          enemyArmy.Troops.Average(x => x.CalculatedDefence);
        var yourCounterDamageBoost = yourArmy.Troops.Average(x => x.CalculatedCounterDamageBoost).ToMultiplier();
        roundLogData.BaseDamage =
            roundLogData.AttackDefenceRatio * yourArmy.TotalTroopsCount * yourCounterDamageBoost;
        roundLogData.NormalDamage = roundLogData.BaseDamage *
                                    yourArmy.ArmyBoosts.DamageDealtByNormalAttacks.ToMultiplier();
        roundLogData.TotalHealth = yourArmy.Troops.Average(x => x.CalculatedHealth * x.Count);

        return roundLogData;
    }

    public Army RefreshArmy(Func<Army, Army, Army> armyFunc, Army currentArmy, Army enemyArmy, FightSimulationOptions options)
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
            
            troop.CalculateStats(newArmy.ArmyBoosts, useCounterBoosts);
        }
        
        return newArmy;
    }

    public ProcessSkillsResult ProcessSkills(Army attackingArmy, Army defendingArmy, double baseDamage, FightSimulationOptions options)
    {
        if (options.UseShooterUnitSkill)
            return new ProcessSkillsResult();
        
        var result = new ProcessSkillsResult();

        if (!options.UseCannons || attackingArmy.FighterConfiguration.Fighter.ActiveSkill.CannonDamageFactor > 0)
        {
            ProcessActiveSkill(attackingArmy, defendingArmy, baseDamage, options, result);
        }
        
        ProcessPassiveSkills(attackingArmy, defendingArmy, baseDamage, result);

        return result;
    }

    private void ProcessActiveSkill(Army attackingArmy, Army defendingArmy, double baseDamage, FightSimulationOptions options,
        ProcessSkillsResult result)
    {
        var activeSkill = attackingArmy.FighterConfiguration.Fighter.ActiveSkill;

        if (attackingArmy.RageLevel >= activeSkill.RageRequired)
        {
            attackingArmy.RageLevel -= activeSkill.RageRequired;
            var skillDamageFactor = options.UseCannons
                ? activeSkill.CannonDamageFactor ?? activeSkill.DamageFactor
                : activeSkill.DamageFactor;

            // TODO: Implement takes less skill damage talents
            // TODO: Implement enemy has less skill damage
            // TODO: Work out what, if any, the difference is between the above two things
            // TODO: Need to confirm this is right - do unit stats and attack really not factor into skill damage at all?
            // TODO: Implement passive skill chance
            InflictSkillDamage(attackingArmy, defendingArmy, baseDamage, skillDamageFactor, activeSkill.MaxTargets, result);

            // TODO: Implement RestoreRageAfterActiveSkill boost type
            // TODO: Implement ChanceToAttackTwice
            // TODO: Implement skills being able to hit multiple players
            // TODO: Implement additional damage chance and factor
            // TODO: Implement increased master fighter skill damage and IncreasedDeputyFighterSkillDamage
        }
    }

    private void ProcessPassiveSkills(Army attackingArmy, Army defendingArmy, double baseDamage, ProcessSkillsResult result)
    {
        var passiveSkills = attackingArmy.ArmyBoosts.PassiveSkills;

        foreach (var passiveSkill in passiveSkills)
        {
            var randomNumber = rand.Next(0, 100);
            var damageFactor = (int)passiveSkill.MaxBoostAmount;
            if (randomNumber <= passiveSkill.Chance)
            {
                InflictSkillDamage(attackingArmy, defendingArmy, baseDamage, damageFactor, 1, result);
            }
        }
    }

    private void InflictSkillDamage(Army attackingArmy, Army defendingArmy, double baseDamage, int skillDamageFactor, int maxTargets,
        ProcessSkillsResult result)
    {
        var targets = Math.Min(defendingArmy.NumberOfPlayers, Math.Max(maxTargets, 1));
        var skillDamageMultiplier = skillDamageFactor / 200.0;
        skillDamageMultiplier += attackingArmy.ArmyBoosts.DamageDealtBySkillsPercentIncrease.ToMultiplier();
        var skillTotalDamage = baseDamage * skillDamageMultiplier * targets;

        result.TotalDamage += (int)skillTotalDamage;
        result.TroopsKilled += ProcessArmyLosses(defendingArmy, skillTotalDamage);
    }

    private int ProcessArmyLosses(Army army, double damage)
    {
        // From testing in game damage looks to be evenly spread out for each unit, so higher count units take more of the damage
        int totalLosses = 0;
        var remainingTroops = army.Troops.Where(x => x.Count > 0).ToList();
        foreach (var troop in remainingTroops)
        {
            var percentageOfArmySize = troop.Count / (double)army.TotalTroopsCount;
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