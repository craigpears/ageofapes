namespace BlazorApp1.Shared.FighterSimulator;

public class ArmyBoosts
{
    public List<Boost> ProbabilityBoosts = new List<Boost>();
    public List<UnitBoosts> UnitBoosts { get; set; } = new List<UnitBoosts>();
    public double DamageDealtByNormalAttacks { get; set; }
    public double DamageDealtBySkillsPercentIncrease { get; set; }
    public double DamageDealtByCounterAttacks { get; set; }
    public double IncreasedMaxTroopsPercent { get; set; }
    public double MaxTroopsMultiplier => (IncreasedMaxTroopsPercent / 100) + 1;
    public List<Boost> ApplicableBoosts { get; set; }

    public void AddGearBoosts(List<Troop> troops)
    {
        // Assume an army troops are all of the same level
        foreach (var unitBoost in UnitBoosts)
        {
            var matchingTroop = troops.SingleOrDefault(x => x.TroopType == unitBoost.TroopType);
            if (matchingTroop != null)
            {
                unitBoost.AttackBoostPercent += matchingTroop.GearAttackBoost;
                unitBoost.DefenceBoostPercent += matchingTroop.GearDefenceBoost;
            }
        }
    }
}