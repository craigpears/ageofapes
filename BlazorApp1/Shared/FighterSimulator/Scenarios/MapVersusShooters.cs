namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class MapVersusShooters : FightScenario
{
    public MapVersusShooters() : base("MapVersusShootersResults", new FightSimulationOptions()
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
                new() { TroopType = TroopType.Shooter, Count = 500000, GearLevel = 5, TroopLevel = 5 },
            }
        };
}