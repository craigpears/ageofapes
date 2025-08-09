using FightSimulator.Core.FighterSimulator.Services;

namespace FightSimulator.Core.FighterSimulator.Scenarios;

public class WhaleSeige : FightScenario
{
    public WhaleSeige() : base("WhaleCityAttackResults", 
            new FightSimulationOptions(ApplicabilityGroup.Garrison)
        ) {}
    
    public override Func<Army, Army, Army> EnemyArmyFunc(FighterConfiguration configuration) =>
            (Army currentArmy, Army enemyArmy) =>
            {
                var freshArmy = new Army
                {
                    ArmyBoosts = new ArmyBoosts { UnitBoosts = new List<UnitBoosts>
                    {
                        new UnitBoosts { AttackBoostPercent = 250, DefenceBoostPercent = 180, TroopType = TroopType.Pilot }, 
                        new UnitBoosts { AttackBoostPercent = 250, DefenceBoostPercent = 180, TroopType = TroopType.Hitter }, 
                        new UnitBoosts { AttackBoostPercent = 250, DefenceBoostPercent = 180, TroopType = TroopType.Shooter }
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

    public override Func<Army, Army, Army> YourArmyFunc(FighterConfiguration configuration) =>
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

                var maxSwapsPerRound = enemyIsAttacking ? 1 : maxReinforcingArmies;
                var swaps = 0;
                while (spaceAvailable >= reinforcementArmySize && army.ReinforcingTroops.Count < maxReinforcingArmies && swaps < maxSwapsPerRound)
                {
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
                    swaps++;
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

                    var hospitalTotal = maxHospitalLimit - healableLosses;
                    army.HospitalMax = Math.Max(hospitalTotal, army.HospitalMax);
                }

                

                return army;
            };
        
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
        
        private class HealingCalculation
        {
            public int TroopsHealed { get; set; }
            public int HealingResourceCost { get; set; }
        }
}