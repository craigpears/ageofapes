namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class SimpleDefence : FightScenario
{
    public SimpleDefence() : base("DefensiveFightResults", new FightSimulationOptions()
    {
        Garrison = true
    }) {}
    
}