using FightSimulator.Core.FighterSimulator.Services;

namespace FightSimulator.Core.FighterSimulator.Scenarios;

public class SimpleCityAttack : FightScenario
{
    public SimpleCityAttack() : base("DirectCityAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.Siege
        )) {}
    
}