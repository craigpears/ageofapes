namespace BlazorApp1.Shared.FighterSimulator;

public class Boost
{
    public BoostType BoostType { get; set; }
    public List<Double> BoostAmounts { get; set; }
    public BoostRestrictionType? BoostRestrictionType { get; set; }
    public TroopType? TroopRestriction { get; set; }
    public int? BoostChancePercent { get; set; }
    public int? BoostDurationSeconds { get; set; }
    public double MaxBoostAmount => BoostAmounts.Max();
    public bool DisabledInCannonMode { get; set; }
    public int Chance { get; set; }
    public int DurationSeconds { get; set; }
}