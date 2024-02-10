namespace BlazorApp1.Shared.FighterSimulator;

public class Boost
{
    public BoostType BoostType { get; set; }
    public List<Double> BoostAmounts { get; set; }
    public BoostRestrictionType? BoostRestrictionType { get; set; }
    public TroopType? TroopRestriction { get; set; }
    public int? BoostChancePercent { get; set; }
    public int? BoostDurationSeconds { get; set; }
}