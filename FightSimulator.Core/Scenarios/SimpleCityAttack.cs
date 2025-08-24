using FightSimulator.Core.Models;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleCityAttack : FightScenario
{
    public SimpleCityAttack(IFightResultsRepository fightResultsRepository) : base("DirectCityAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.Siege
        ), fightResultsRepository) {}
    
}