using FightSimulator.Core.Fighters;
using FightSimulator.Core.Models;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleDefence : FightScenario
{
    public SimpleDefence(bool lightRun, IFightResultsRepository fightResultsRepository) : base("DefensiveFightResults",
        new FightSimulationOptions(
            ApplicabilityGroup.Garrison
        ), fightResultsRepository)
    {
        if (lightRun)
        {
            RunOptions = new RunOptions
            {
                IncludePilots = true,
                IncludeHitters = true,
                IncludeLeaders = true
            };
        }
    }
    
}