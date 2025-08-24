namespace FightSimulator.Core.Models;

public class Boost
{
    public BoostType BoostType { get; set; }
    public List<Double> BoostAmounts { get; set; } = new List<double>();
    public BoostRestrictionType? BoostRestrictionType { get; set; }
    public TroopType? TroopRestriction { get; set; }
    public int? BoostChancePercent { get; set; }
    public int? BoostDurationSeconds { get; set; }
    public double MaxBoostAmount => BoostAmounts.Any() ? BoostAmounts.Max() : 1;
    public bool DisabledInCannonMode { get; set; }
    public int Chance { get; set; }
    public int DurationSeconds { get; set; }
    public string Source { get; set; }
    public string Name { get; set; }
    public bool BoostsAllies { get; set; }
    public ApplicabilityGroup? ApplicabilityGroup { get; set; }
}