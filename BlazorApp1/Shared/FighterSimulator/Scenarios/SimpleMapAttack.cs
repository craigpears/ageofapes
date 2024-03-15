namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class SimpleMapAttack : FightScenario
{
    public SimpleMapAttack() : base("DirectMapAttackResults", new FightSimulationOptions()
    {
        MapBattle = true,
        Gathering = true // Show the gatherers at their best as if they are defending while gathering resources
    }) {}
    
}