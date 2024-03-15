namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class CannonAttack : FightScenario
{
    public CannonAttack() : base("CannonResults", new FightSimulationOptions()
    {
        Seige = true,
        UseCannons = true,
    })
    {
        EnemyBoostMultiplier = 4.0;
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
                    Count = (int)(100000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                    TroopType = TroopType.WallBreaker, GearLevel = 5, TroopLevel = 5
                }
            }
        };
}