namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class SimpleMapAttack : FightScenario
{
    public SimpleMapAttack() : base("DirectMapAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.Gathering
        )) {}
    
}