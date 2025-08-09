using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleCityAttack : FightScenario
{
    public SimpleCityAttack() : base("DirectCityAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.Siege
        )) {}
    
}