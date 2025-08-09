using FightSimulator.Core.FighterSimulator.Services;

namespace FightSimulator.Core.FighterSimulator.Scenarios;

public class SimpleDefence : FightScenario
{
    public SimpleDefence() : base("DefensiveFightResults", new FightSimulationOptions(
        ApplicabilityGroup.Garrison
    )) {}
    
}