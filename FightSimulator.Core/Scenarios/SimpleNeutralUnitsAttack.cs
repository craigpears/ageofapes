using FightSimulator.Core.Fighters;
using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleNeutralUnitsAttack : FightScenario
{
    public SimpleNeutralUnitsAttack(bool lightRun) : base("NeutralUnitAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.AttackingNeutralUnits
    )) 
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