namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class SimpleNeutralUnitsAttack : FightScenario
{
    public SimpleNeutralUnitsAttack() : base("NeutralUnitAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.AttackingNeutralUnits
    )) {}
    
}