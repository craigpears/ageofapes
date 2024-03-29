﻿namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class MapVersusHitters : FightScenario
{
    public MapVersusHitters() : base("MapVersusHittersResults", new FightSimulationOptions()
    {
        MapBattle = true
    }) {}
    
    public override Func<Army, Army, Army> EnemyArmyFunc(FighterConfiguration configuration)=>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = new ArmyBoosts
            {
                UnitBoosts = GetBoosts(1.25)
            },
            Troops = new List<Troop>
            {
                new() { TroopType = TroopType.Hitter, Count = 500000, GearLevel = 5, TroopLevel = 5 },
            }
        };
}