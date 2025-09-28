using FightSimulator.Core.Fighters;
using FightSimulator.Core.Models;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleCityAttack : FightScenario
{
    public SimpleCityAttack(bool lightRun, IFightResultsRepository fightResultsRepository) : base("DirectCityAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.Siege
        ), fightResultsRepository) {
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