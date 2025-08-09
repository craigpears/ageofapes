namespace FightSimulator.Core.FighterSimulator.Services;

public class FightSimulationOptions
{
    public FightSimulationOptions(params ApplicabilityGroup[] applicabilityGroups)
    {
        ApplicabilityGroups = applicabilityGroups.ToList();
    }
    
    public List<ApplicabilityGroup> ApplicabilityGroups { get; set; }
    public bool UseCannons { get; set; }
    public bool UseShooterUnitSkill { get; set; }
    public bool RecalculateArmies { get; set; }
    public bool RunToAllPlayersDead { get; set; }
}