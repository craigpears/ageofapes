namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public class SimpleCityAttack : FightScenario
{
    public SimpleCityAttack() : base("DirectCityAttackResults", new FightSimulationOptions()
    {
        Seige = true
    }) {}
    
}