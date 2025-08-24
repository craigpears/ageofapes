using FightSimulator.Core.Fighters;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class MapVersusPilots : FightScenario
{
    public MapVersusPilots(bool lightRun, IFightResultsRepository fightResultsRepository) : base("MapVersusPilotsResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle
    ), fightResultsRepository)
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
                UnitBoosts = GetBoosts(1.25)
            },
            Troops = new List<Troop>
            {
                new() { TroopType = TroopType.Pilot, Count = 150000, GearLevel = 5, TroopLevel = 5 },
            }
        };
}