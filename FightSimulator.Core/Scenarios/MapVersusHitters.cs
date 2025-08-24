using FightSimulator.Core.Models;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class MapVersusHitters : FightScenario
{
        public MapVersusHitters(IFightResultsRepository fightResultsRepository) : base("MapVersusHittersResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle
    ), fightResultsRepository) {}
    
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