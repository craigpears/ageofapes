using FightSimulator.Core.FighterSimulator.Services;

namespace FightSimulator.Core.FighterSimulator.Scenarios;

public class ShooterUnitSkill : FightScenario
{
    public ShooterUnitSkill() : base("ShooterUnitSkillResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle)
    {
        UseShooterUnitSkill = true
    }) {}

    public override Func<Army, Army, Army> YourArmyFunc(FighterConfiguration configuration) =>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = configuration.ArmyBoosts,
            FighterConfiguration = configuration,
            Troops = new List<Troop>
            {
                new()
                {
                    Count = (int)(100000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Shooter,
                    GearLevel = 5, TroopLevel = 5
                }
            }
        };
}