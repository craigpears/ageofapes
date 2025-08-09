using FightSimulator.Core.FighterSimulator.Services;

namespace FightSimulator.Core.FighterSimulator.Scenarios;

public class SimpleNeutralUnitsAttack : FightScenario
{
    public SimpleNeutralUnitsAttack() : base("NeutralUnitAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.AttackingNeutralUnits
    )) {}
    
}