using FightSimulator.Core.Models;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class MapWallBreakers : FightScenario
{
    public MapWallBreakers(IFightResultsRepository fightResultsRepository) : base("MapWallBreakers", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle), fightResultsRepository)
    {
        
    }

    public override Func<Army, Army, Army> YourArmyFunc(FighterConfiguration configuration) =>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = configuration.ArmyBoosts,
            FighterConfiguration = configuration,
            Troops = new List<Troop>
            {
                new()
                {
                    Count = (int)(300000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                    TroopType = TroopType.WallBreaker, GearLevel = 5, TroopLevel = 5
                }
            }
        };
}