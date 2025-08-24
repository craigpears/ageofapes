using FightSimulator.Core.Fighters;
using FightSimulator.Core.Repositories;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleNeutralUnitsAttack : FightScenario
{
    public SimpleNeutralUnitsAttack(bool lightRun, IFightResultsRepository fightResultsRepository) : base("NeutralUnitAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.AttackingNeutralUnits
    ), fightResultsRepository) 
    {
        if (lightRun)
        {
            RunOptions = new RunOptions
            {
                IncludeHunters = true
            };
        }
    }
    
}