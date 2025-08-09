using FightSimulator.Core.Services;

namespace FightSimulator.Core.Scenarios;

public class SimpleMapAttack : FightScenario
{
    public SimpleMapAttack() : base("DirectMapAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.Gathering
        )) {}
    
}