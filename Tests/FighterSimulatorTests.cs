using System.Diagnostics;
using System.Text;
using System.Xml.Schema;
using BlazorApp1.Shared.FighterSimulator;
using BlazorApp1.Shared.FighterSimulator.Extensions;
using Newtonsoft.Json;
using ScoutingParser;
using Syncfusion.Pdf;

namespace Tests;

public class FighterSimulatorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldDoStuff()
    {
        var outputBaseFolder = @"\\nas-pears\documents\AgeOfApes\FighterOutputs";
        
        var fightSimulationService = new FightSimulationService();
        var statsService = new FighterStatsService();

        Func<Army, Army, Army> DefendingArmyFunc(FighterConfiguration configuration) =>
            (Army currentArmy, Army enemyArmy) => new Army
            {
                // TODO: Defending army should have a fighter so reduced damage from skills gives a benefit
                ArmyBoosts = new ArmyBoosts { UnitBoosts = new List<UnitBoosts> { new UnitBoosts { AttackBoostPercent = 360, DefenceBoostPercent = 520, TroopType = TroopType.Pilot }, new UnitBoosts { AttackBoostPercent = 360, DefenceBoostPercent = 520, TroopType = TroopType.Hitter }, new UnitBoosts { AttackBoostPercent = 360, DefenceBoostPercent = 520, TroopType = TroopType.Shooter } } }, Troops = new List<Troop> { new() { TroopType = TroopType.Hitter, Count = 250000, GearLevel = 5, TroopLevel = 5 }, new() { TroopType = TroopType.Pilot, Count = 250000, GearLevel = 5, TroopLevel = 5 }, new() { TroopType = TroopType.Shooter, Count = 250000, GearLevel = 5, TroopLevel = 5 }, }
            };

        Func<Army, Army, Army> AttackingArmyFunc(FighterConfiguration configuration) =>
            (Army currentArmy, Army enemyArmy) => new Army
            {
                ArmyBoosts = configuration.ArmyBoosts,
                FighterConfiguration = configuration,
                Troops = configuration.PreferredTroopType == TroopType.ThreeUnitTypes
                    ? new List<Troop> { new() { Count = (int)(140000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Pilot, GearLevel = 5, TroopLevel = 5 }, new() { Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Hitter, GearLevel = 5, TroopLevel = 5 }, new() { Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Shooter, GearLevel = 5, TroopLevel = 5 } }
                    : new List<Troop> { new Troop() { Count = (int)(150000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = configuration.PreferredTroopType, GearLevel = 5, TroopLevel = 5 } }
            };

        Func<Army, Army, Army> WhaleArmyAttackFunc(FighterConfiguration configuration) =>
            (Army currentArmy, Army enemyArmy) =>
            {
                var freshArmy = new Army
                {
                    ArmyBoosts = new ArmyBoosts { UnitBoosts = new List<UnitBoosts>
                    {
                        new UnitBoosts { AttackBoostPercent = 60, DefenceBoostPercent = 55, TroopType = TroopType.Pilot }, 
                        new UnitBoosts { AttackBoostPercent = 60, DefenceBoostPercent = 55, TroopType = TroopType.Hitter }, 
                        new UnitBoosts { AttackBoostPercent = 60, DefenceBoostPercent = 55, TroopType = TroopType.Shooter }
                    } },
                    Troops = new List<Troop>
                    {
                        new() { TroopType = TroopType.Hitter, Count = 2500000, GearLevel = 5, TroopLevel = 5 },
                        new() { TroopType = TroopType.Pilot, Count = 2500000, GearLevel = 5, TroopLevel = 5 },
                        new() { TroopType = TroopType.Pilot, Count = 2500000, GearLevel = 5, TroopLevel = 5 },
                    },
                    TroopReserveRemaining = 10000000
                };

                // How many rounds after withdrawing troops before replacements come
                var refreshRounds = 1;

                // A troop will retreat when it has less than this number of troops left
                var refreshTriggerTroopCount = 600000;
                var attackerMinimumTroops = 1000000;

                var army = currentArmy ?? freshArmy;
                var troopsRemaining = army.TotalTroopsCount;
                if (troopsRemaining < refreshTriggerTroopCount)
                {
                    // TODO: Calculate injuries and losses
                    foreach (var troop in army.Troops)
                    {
                        var healingResult = CalculateHealing(troop.LossesSinceLastRefresh);
                        army.TroopReserveRemaining += healingResult.TroopsHealed;
                        army.HealingResourceCost += healingResult.HealingResourceCost;
                        troop.LossesSinceLastRefresh = 0;
                        troop.RefreshRoundsLeft ??= refreshRounds;
                        troop.RefreshRoundsLeft--;
                    }
                    
                }

                if (army.Troops.All(x => x.RefreshRoundsLeft <= 0))
                {
                    army.Troops.ForEach(x => x.RefreshRoundsLeft = null);

                    if (currentArmy.TroopReserveRemaining > attackerMinimumTroops)
                    {
                        freshArmy.TroopReserveRemaining = army.TroopReserveRemaining + army.TotalTroopsCount - freshArmy.TotalTroopsCount;
                        freshArmy.HealingResourceCost = army.HealingResourceCost;
                        army = freshArmy;
                    }
                }

                return army;
            };

        Func<Army, Army, Army> DefendingAgainstWhaleArmyFunc(FighterConfiguration configuration) =>
            (Army currentArmy, Army enemyArmy) =>
            {
                var freshArmy = new Army
                {
                    ArmyBoosts = configuration.ArmyBoosts,
                    FighterConfiguration = configuration,
                    Troops = new List<Troop>
                    {
                        new()
                        {
                            Count = 750000,
                            TroopType = TroopType.Pilot,
                            GearLevel = 5,
                            TroopLevel = 5,
                            PlayerNumber = 0 // Main garrison troops
                        },
                        new()
                        {
                            Count = 500000,
                            TroopType = TroopType.Hitter,
                            GearLevel = 5,
                            TroopLevel = 5,
                            PlayerNumber = 0 // Main garrison troops
                        },
                        new()
                        {
                            Count = 200000,
                            TroopType = TroopType.Shooter,
                            GearLevel = 5,
                            TroopLevel = 5,
                            PlayerNumber = 0 // Main garrison troops
                        }
                    }
                };

                var army = currentArmy ?? freshArmy;

                var currentReinforcementsCount = army.Troops.Where(x => x.PlayerNumber > 0).Sum(x => x.Count);
                var maxReinforcements = 1.2 * 1000000;
                var reinforcementArmySize = 250000;
                var reinforcementSwapLevel = 100000;
                var maxReinforcingArmies = 10;
                var spaceAvailable = maxReinforcements - currentReinforcementsCount;

                var enemyIsAttacking = enemyArmy?.TotalTroopsCount > 0;

                var reinforcingTroops = army.ReinforcingTroops;
                var troopsToReturn = enemyIsAttacking ? reinforcingTroops : reinforcingTroops.Where(x => x.Count < reinforcementSwapLevel);

                foreach (var returningTroop in troopsToReturn)
                {
                    // Simulate reinforcements swapping and healing
                    var healingResult = CalculateHealing(reinforcementArmySize - returningTroop.Count);
                    army.AlliesHealingResourceCost += healingResult.HealingResourceCost;
                }

                while (spaceAvailable >= reinforcementArmySize && army.ReinforcingTroops.Count < maxReinforcingArmies)
                {
                    // TODO: Add a sane limit on how fast people can swap
                    // Simulate Reinforcements arriving
                    army.Troops.Add(new Troop()
                    {
                        Count = reinforcementArmySize,
                        TroopType = TroopType.Pilot,
                        GearLevel = 5,
                        TroopLevel = 5,
                        PlayerNumber = 1 // Reinforcement troops
                    });

                    spaceAvailable -= reinforcementArmySize;
                }

                var maxHospitalLimit = 750000;
                
                if (!enemyIsAttacking)
                {
                    var healableLosses = maxHospitalLimit;
                        
                    foreach (var troop in army.GarrisonTroops)
                    {
                        
                        var healingResult = CalculateHealing(troop.LossesSinceLastRefresh);
                        army.HealingResourceCost += healingResult.HealingResourceCost;

                        troop.Count += Math.Min(troop.LossesSinceLastRefresh, healableLosses);
                        healableLosses -= troop.LossesSinceLastRefresh;
                        troop.LossesSinceLastRefresh = 0;
                    }
                }

                // TODO: Simulate hospital capacity

                return army;
            };
        
        Debug.WriteLine($"Simulating whale city attack");
        var whaleFightResults = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                Garrison = true,
                RecalculateArmies = true
            }, 
            statsService,
            DefendingAgainstWhaleArmyFunc,
            WhaleArmyAttackFunc);
        
        ResultsToCsv(whaleFightResults, $"{outputBaseFolder}//WhaleCityAttackResults");
        
        Debug.WriteLine($"Simulating cannon city attacks");
        var cannonCityAttackResults = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                Seige = true,
                UseCannons = true,
                UseShooterUnitSkill = false,
                MapBattle = false
            }, 
            statsService,
            (FighterConfiguration configuration) => (Army currentArmy, Army enemyArmy) => new Army
            {
                ArmyBoosts = configuration.ArmyBoosts, 
                FighterConfiguration = configuration,
                Troops = new List<Troop>
                {
                    new() { Count = (int)(100000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.WallBreaker, GearLevel = 5, TroopLevel = 5 }
                }
            },
            DefendingArmyFunc);
        
        ResultsToCsv(cannonCityAttackResults, $"{outputBaseFolder}//CannonResults");

        Debug.WriteLine($"Simulating shooter unit skill attacks");
        var shooterUnitSkillAttackResults = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                UseShooterUnitSkill = true,
                MapBattle = true
            }, 
            statsService,
            (FighterConfiguration configuration) => (Army currentArmy, Army enemyArmy) => new Army
            {
                ArmyBoosts = configuration.ArmyBoosts, 
                FighterConfiguration = configuration,
                Troops = new List<Troop>
                {
                    new() { Count = (int)(100000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Shooter, GearLevel = 5, TroopLevel = 5 }
                }
            },
            DefendingArmyFunc);
        
        ResultsToCsv(shooterUnitSkillAttackResults, $"{outputBaseFolder}//ShooterUnitSkillResults");
        
        Debug.WriteLine($"Simulating direct city attacks");
        var directCityAttackResults = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                Seige = true
            }, 
            statsService,
            AttackingArmyFunc,
            DefendingArmyFunc);
        
        ResultsToCsv(directCityAttackResults, $"{outputBaseFolder}//DirectCityAttackResults");
        
        Debug.WriteLine($"Simulating direct map attacks");
        var directMapAttacks = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                MapBattle = true,
                Gathering = true // Show the gatherers at their best as if they are defending while gathering resources
            }, 
            statsService,
            AttackingArmyFunc,
            DefendingArmyFunc);
        
        ResultsToCsv(directMapAttacks, $"{outputBaseFolder}//DirectMapAttackResults");
        
        Debug.WriteLine($"Simulating neutral unit attacks");
        var neutralUnitAttacks = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                MapBattle = true,
                AttackingNeutralUnits = true
            }, 
            statsService,
            AttackingArmyFunc,
            DefendingArmyFunc);
        
        ResultsToCsv(neutralUnitAttacks, $"{outputBaseFolder}//NeutralUnitAttackResults");
        
        Debug.WriteLine($"Simulating defending");
        var defensiveFightResults = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                Garrison = true
            }, 
            statsService,
            AttackingArmyFunc,
            DefendingArmyFunc);
        
        ResultsToCsv(defensiveFightResults, $"{outputBaseFolder}//DefensiveFightResults");
        
        
        
        // TODO: For 1v1s show the results in a matrix
        // TODO: Simulate the differences in gear level
        // TODO: Simulate how much research contributes vs gears etc relative to their coin cost
        // TODO: Output summaries of each talent trees
        // TODO: Show coin costs cumulatively 
        // TODO: Add deputy fighter support
    }

    private static HealingCalculation CalculateHealing(int losses)
    {
        var t5HealingCost = 350;
        var moderateRate = 0.078;
        
        var injuries = losses * moderateRate;
        var healingCost = injuries * t5HealingCost;
        var result = new HealingCalculation
        {
            HealingResourceCost = (int)healingCost,
            TroopsHealed = (int)injuries
        };
        
        return result;
    }

    private static void ResultsToCsv(List<AttackResult> results, string outputFolder)
    {
        Debug.WriteLine($"Writing results");
        var bestResults = results
            .GroupBy(x => new
            {
                x.YourArmy.FighterConfiguration.Fighter.Name
            }) // Only take the best of each breakdown, there are usually lots of variants with stats that don't matter
            .Select(x => x.ToList().GetBestResult())
            .ToList();

        var resultsCsv = ResultsToCsv(bestResults);

        File.WriteAllText($"{outputFolder}//bestResults.txt", resultsCsv);
        foreach (var bestResult in bestResults)
        {
            var fighterConfig = bestResult.YourArmy.FighterConfiguration;
            var fighterSelectedTalents = fighterConfig.SelectedTalents;

            var talentsJson = JsonConvert.SerializeObject(fighterSelectedTalents);
            File.WriteAllText($"{outputFolder}//{fighterConfig.Fighter.Name}.bestConfig.txt", talentsJson);
        }

        var resultsByFighter = results.GroupBy(x => x.YourArmy.FighterConfiguration.Fighter.Name);
        foreach (var fighterResults in resultsByFighter)
        {
            var bestFighterResults = fighterResults
                .GroupBy(x => new
                {
                    x.YourArmy.FighterConfiguration.TalentBreakdown
                }) // Only take the best of each breakdown, there are usually lots of variants with stats that don't matter
                .Select(x => x.ToList().GetBestResult())
                .ToList();
            
            var fighterResultsCsv = ResultsToCsv(bestFighterResults);
            File.WriteAllText($"{outputFolder}//{fighterResults.Key}.results.txt", fighterResultsCsv);
        }
    }

    private static string ResultsToCsv(List<AttackResult> bestResults)
    {
        var headers =
            "Fighter\tRoundsTaken\tMaxDamage\tMaxSkillDamage\tDefenderLosses\tAttackerLosses\tTalentBreakdown\tCannons\tSeige\tGathering\tMap Battle\tUse Shooter Unit Skill\t";
        
        // TODO: Break down by type like research, equipment, talent skill etc.
        var boostTypes = bestResults
            .SelectMany(x => x.YourArmy.FighterConfiguration.SelectedTalents)
            .SelectMany(x => x.Boosts)
            .OrderBy(x => x.Source)
            .Select(x => $"{x.Source}-{x.BoostType}")
            .Distinct();

        foreach (var boostType in boostTypes)
        {
            headers += $"{boostType.ToString()}\t";
        }

        var sb = new StringBuilder();

        sb.AppendLine(headers);

        foreach (var result in bestResults)
        {
            sb.Append($"{result.YourArmy.FighterConfiguration.Fighter.Name}\t");
            sb.Append($"{result.NumberOfRounds}\t");
            sb.Append($"{result.HighestYourDamage}\t");
            sb.Append($"{result.HighestYourSkillDamage}\t");
            sb.Append($"{result.TotalEnemyLostTroops}\t");
            sb.Append($"{result.TotalYourLostTroops}\t");
            sb.Append($"{result.AttackLogs.First().EnemyLostTroops}\t");
            sb.Append($"{result.YourArmy.FighterConfiguration.TalentBreakdown}\t");

            sb.Append($"{result.FightOptions.UseCannons}\t");
            sb.Append($"{result.FightOptions.Seige}\t");
            sb.Append($"{result.FightOptions.Gathering}\t");
            sb.Append($"{result.FightOptions.MapBattle}\t");
            sb.Append($"{result.FightOptions.UseShooterUnitSkill}\t");

            var resultBoosts = result
                .YourArmy
                .ArmyBoosts
                .ApplicableBoosts
                .GroupBy(x => $"{x.Source}-{x.BoostType}");

            foreach (var boostType in boostTypes)
            {
                var matchingBoost = resultBoosts.SingleOrDefault(x => x.Key == boostType);
                if (matchingBoost != null)
                {
                    sb.Append($"{matchingBoost.ToList().Sum(b => b.BoostAmounts.Max())}");
                }

                sb.Append("\t");
            }

            sb.Append("\r\n");
        }

        var resultsCsv = sb.ToString();
        return resultsCsv;
    }
}