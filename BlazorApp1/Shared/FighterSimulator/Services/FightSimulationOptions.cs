namespace BlazorApp1.Shared.FighterSimulator;

public class FightSimulationOptions
{
    public bool MapBattle { get; set; }
    public bool Seige { get; set; }
    public bool UseCannons { get; set; }
    public bool UseShooterUnitSkill { get; set; }
    public bool Gathering { get; set; }
    
    public bool Garrison { get; set; }
    public bool AttackingNeutralUnits { get; set; }
    public bool RecalculateArmies { get; set; }
}