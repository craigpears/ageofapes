using FightSimulator.Core.FighterSimulator.Services;

namespace FightSimulator.Core.FighterSimulator.Scenarios;

public class MapVersusPilots : FightScenario
{
    public MapVersusPilots() : base("MapVersusPilotsResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle
    )) {}
    
    public override Func<Army, Army, Army> EnemyArmyFunc(FighterConfiguration configuration)=>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = new ArmyBoosts
            {
                UnitBoosts = GetBoosts(1.25)
            },
            Troops = new List<Troop>
            {
                new() { TroopType = TroopType.Pilot, Count = 500000, GearLevel = 5, TroopLevel = 5 },
            }
        };
}