namespace BlazorApp1.Shared.FighterSimulator;

public class FighterSkill
{
    public FigherSkillType FighterSkillType { get; set; }
    public int RageRequired { get; set; }
    public int DamageFactor { get; set; }
    public int? CannonDamageFactor { get; set; }
    public List<Boost> Boosts { get; set; }
    public int HealingFactor { get; set; }
}