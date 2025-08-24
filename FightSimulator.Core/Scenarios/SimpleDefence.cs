using FightSimulator.Core.Models;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleDefence : FightScenario
{
    public SimpleDefence(IFightResultsRepository fightResultsRepository) : base("DefensiveFightResults", new FightSimulationOptions(
        ApplicabilityGroup.Garrison
    ), fightResultsRepository) {}
    
}