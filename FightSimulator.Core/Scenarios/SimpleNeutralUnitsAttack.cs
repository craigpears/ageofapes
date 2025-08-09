using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleNeutralUnitsAttack : FightScenario
{
    public SimpleNeutralUnitsAttack() : base("NeutralUnitAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.AttackingNeutralUnits
    )) {}
    
}