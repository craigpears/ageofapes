namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Leader : TalentClass
{
    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, HalfPercent);
        rootTalent.TalentTreeName = "Leader";
        
        // Left Tree
        rootTalent
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedDamage, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedMaxTroops, SinglePercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            // Not modelling dependency on previous optional talent, shouldn't affect outcomes significantly
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .OptionalTalent(BoostType.TakesLessDamage, HalfPercentSteps, TroopType.ThreeUnitTypes)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedHealing, OnePercent)
            .NextTalent(BoostType.IncreasedDamage, new List<Double> { 2, 4, 6, 8 }, TroopType.ThreeUnitTypes)
            .NextTalent(BoostType.IncreasedAttack, new List<Double> { 2, 4, 6, 8, 10 },
                boostRestrictionType: BoostRestrictionType.LeadingRally);

        // Right Tree
        rootTalent
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedHealing, new List<Double> { 3, 6, 9 })
            .NextTalent(BoostType.IncreasedRageOnReceiveNormalAttack, new List<Double> { 2, 4, 6 })
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .OptionalTalent(BoostType.IncreasedAttack, OneAndHalfPercentSteps,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .WithExtraBoost(BoostType.IncreasedDefence, OneAndHalfPercentSteps,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .NextTalent(BoostType.IncreasedAttack, new List<Double> { 20.0 },
                boostRestrictionType: BoostRestrictionType.TwoSecondsAfterActiveSkillRelease);

        return rootTalent;
    }
}