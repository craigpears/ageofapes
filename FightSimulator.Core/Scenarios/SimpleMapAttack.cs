using FightSimulator.Core.Models;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleMapAttack : FightScenario
{
    public SimpleMapAttack(IFightResultsRepository fightResultsRepository) : base("DirectMapAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.Gathering
        ), fightResultsRepository) {}
    
}