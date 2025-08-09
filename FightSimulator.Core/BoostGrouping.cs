namespace FightSimulator.Core;

public class BoostGrouping
{
    protected double _totalMaxBoostAmount { get; set; }
    public BoostType BoostType { get; set; }
    public TroopType? TroopRestriction { get; set; }
    public ApplicabilityGroup? ApplicabilityGroup { get; set; }
    public List<Boost> Boosts { get; set; }

    public double TotalMaxBoostAmount { get; set; }

    private sealed class BoostGroupingEqualityComparer : IEqualityComparer<BoostGrouping>
    {
        public bool Equals(BoostGrouping x, BoostGrouping y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.BoostType == y.BoostType && x.TroopRestriction == y.TroopRestriction && x.TotalMaxBoostAmount.Equals(y.TotalMaxBoostAmount);
        }

        public int GetHashCode(BoostGrouping obj)
        {
            return HashCode.Combine((int)obj.BoostType, obj.TroopRestriction, obj.TotalMaxBoostAmount);
        }
    }

    public static IEqualityComparer<BoostGrouping> BoostGroupingComparer { get; } = new BoostGroupingEqualityComparer();

}