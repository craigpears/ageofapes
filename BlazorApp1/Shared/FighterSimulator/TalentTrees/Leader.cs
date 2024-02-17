namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Leader : TalentClass
{
    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, HalfPercent);
        rootTalent.TalentTreeName = "Leader";
        
        // Left Tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedAttack, HalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedDamage, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedMaxTroops, SingleStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            // Not modelling dependency on previous optional talent, shouldn't affect outcomes significantly
            .WithOptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, HalfStepUnits)
            .WithOptionalTalent(BoostType.TakesLessDamage, HalfStepUnits, TroopType.ThreeUnitTypes)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealing, OnePercent)
            .WithNextTalent(BoostType.IncreasedDamage, new List<Double> { 2, 4, 6, 8 }, TroopType.ThreeUnitTypes)
            .WithNextTalent(BoostType.IncreasedAttack, new List<Double> { 2, 4, 6, 8, 10 },
                boostRestrictionType: BoostRestrictionType.LeadingRally);

        // Right Tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedDefence, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealing, new List<Double> { 3, 6, 9 })
            .WithNextTalent(BoostType.IncreasedRageOnReceiveNormalAttack, new List<Double> { 2, 4, 6 })
            .WithNextTalent(BoostType.IncreasedAttack, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedAttack, OneAndHalfStepUnits,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .WithExtraBoost(BoostType.IncreasedDefence, OneAndHalfStepUnits,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, OneAndHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, new List<Double> { 20.0 },
                boostRestrictionType: BoostRestrictionType.TwoSecondsAfterActiveSkillRelease);

        return rootTalent;
    }
}