using FightSimulator.Core.Fighters;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class MapVersusShooters : FightScenario
{
    public MapVersusShooters(bool lightRun, IFightResultsRepository fightResultsRepository) : base("MapVersusShootersResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle
    ){
        RunToAllPlayersDead = true
    }, fightResultsRepository)
    {
        if (lightRun)
        {
            RunOptions = new RunOptions
            {
                IncludeShooters = true
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