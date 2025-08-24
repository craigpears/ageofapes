using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class CannonAttack : FightScenario
{
    public CannonAttack(IFightResultsRepository fightResultsRepository) : base("CannonResults", new FightSimulationOptions(
            ApplicabilityGroup.Siege
        )
    {
        UseCannons = true,
    }, fightResultsRepository)
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
                    Count = (int)(150000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                    TroopType = TroopType.WallBreaker, GearLevel = 5, TroopLevel = 5
                }
            }
        };
}