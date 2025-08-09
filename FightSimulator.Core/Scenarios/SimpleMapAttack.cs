using FightSimulator.Core.FighterSimulator.Services;

namespace FightSimulator.Core.FighterSimulator.Scenarios;

public class SimpleMapAttack : FightScenario
{
    public SimpleMapAttack() : base("DirectMapAttackResults", new FightSimulationOptions(
        ApplicabilityGroup.MapBattle,
        ApplicabilityGroup.Gathering
        )) {}
    
}