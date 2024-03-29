﻿namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class SimpleHittersMapAttack : FightScenario
{
    public SimpleHittersMapAttack() : base("HittersMapAttackResults", new FightSimulationOptions()
    {
        MapBattle = true
    }) {}
    
    public virtual Func<Army, Army, Army> EnemyArmyFunc(FighterConfiguration configuration) =>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = configuration.ArmyBoosts,
            FighterConfiguration = configuration,
            Troops = configuration.PreferredTroopType == TroopType.ThreeUnitTypes
                ? new List<Troop> { new() { Count = (int)(140000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Pilot, GearLevel = 5, TroopLevel = 5 }, new() { Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Hitter, GearLevel = 5, TroopLevel = 5 }, new() { Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Shooter, GearLevel = 5, TroopLevel = 5 } }
                : new List<Troop> { new Troop() { Count = (int)(150000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = configuration.PreferredTroopType, GearLevel = 5, TroopLevel = 5 } }
        };
    
}