namespace BlazorApp1.Shared.FighterSimulator;

public class ArmyBoosts
{
    public List<UnitBoosts> UnitBoosts { get; set; } = new List<UnitBoosts>();
    public double DamageDealtByNormalAttacks { get; set; }
    public double DamageDealtBySkills { get; set; }
    public double DamageDealtByCounterAttacks { get; set; }

    public void AddGearBoosts(List<Troop> troops)
    {
        // Assume an army troops are all of the same level
        foreach (var unitBoost in UnitBoosts)
        {
            var matchingTroop = troops.SingleOrDefault(x => x.TroopType == unitBoost.TroopType);
            if (matchingTroop != null)
            {
                unitBoost.Attack += matchingTroop.GearAttackBoost;
                unitBoost.Defence += matchingTroop.GearDefenceBoost;
            }
        }
    }
}