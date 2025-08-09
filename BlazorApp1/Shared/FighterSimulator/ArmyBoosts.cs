namespace BlazorApp1.Shared.FighterSimulator;

public class ArmyBoosts
{
    public List<UnitBoosts> UnitBoosts { get; set; } = new List<UnitBoosts>
    {
        new (TroopType.Pilot),
        new (TroopType.Hitter),
        new (TroopType.Shooter),
        new (TroopType.WallBreaker),
    };
    public double DamageDealtByNormalAttacks { get; set; }
    public double DamageDealtBySkillsPercentIncrease { get; set; }
    public double DamageDealtByCounterAttacks { get; set; }
    public double IncreasedMaxTroopsPercent { get; set; }
    public double MaxTroopsMultiplier => (IncreasedMaxTroopsPercent / 100) + 1;
    public List<Boost> PassiveSkills { get; set; } = new List<Boost>();
    public List<Boost> ApplicableBoosts { get; set; }
    public List<BoostGrouping> BoostsByType { get; set; }

    public void AddGearBoosts(List<Troop> troops)
    {
        // Assume an army troops are all of the same level
        foreach (var unitBoost in UnitBoosts)
        {
            var matchingTroop = troops.FirstOrDefault(x => x.TroopType == unitBoost.TroopType);
            if (matchingTroop != null)
            {
                unitBoost.AttackBoostPercent ??= 0;
                unitBoost.AttackBoostPercent += matchingTroop.GearAttackBoost;
                unitBoost.DefenceBoostPercent ??= 0;
                unitBoost.DefenceBoostPercent += matchingTroop.GearDefenceBoost;
            }
        }
    }
}