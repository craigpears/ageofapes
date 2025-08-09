namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class MapVersusShooters : FightScenario
{
    public MapVersusShooters(bool lightRun) : base("MapVersusShootersResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle
    ){
        RunToAllPlayersDead = true
    })
    {
        if (lightRun)
        {
            RunOptions = new RunOptions
            {
                IncludePilots = true
            };
        }
    }
    
    public override Func<Army, Army, Army> EnemyArmyFunc(FighterConfiguration configuration)=>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = new ArmyBoosts
            {
                UnitBoosts = GetBoosts(1.0)
            },
            Troops = new List<Troop>
            {
                new() { TroopType = TroopType.Shooter, Count = 100000, GearLevel = 5, TroopLevel = 5, PlayerNumber = 0 }
            }
        };
}