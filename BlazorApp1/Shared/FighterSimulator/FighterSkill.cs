namespace BlazorApp1.Shared.FighterSimulator;

public class FighterSkill
{
    public FigherSkillType FighterSkillType { get; set; }
    public int RageRequired { get; set; }
    public int DamageFactor { get; set; }
    public int? CannonDamageFactor { get; set; }
    public List<Boost> Boosts { get; set; } = new List<Boost>();
    public int HealingFactor { get; set; }
    public int ChanceToAttackTwice { get; set; }
    public int MaxTargets { get; set; }
    public int AdditionalDamageChance { get; set; }
    public int AdditionalDamageFactor { get; set; }
    public int Chance { get; set; }
    public int ShieldFactor { get; set; }
    public int AdditionalShieldFactor { get; set; }
}