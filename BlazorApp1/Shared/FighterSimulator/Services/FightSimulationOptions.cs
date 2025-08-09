namespace BlazorApp1.Shared.FighterSimulator;

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