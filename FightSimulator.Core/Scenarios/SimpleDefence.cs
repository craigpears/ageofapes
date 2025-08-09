using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleDefence : FightScenario
{
    public SimpleDefence() : base("DefensiveFightResults", new FightSimulationOptions(
        ApplicabilityGroup.Garrison
    )) {}
    
}