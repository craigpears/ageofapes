namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Support : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedMarchingSpeed, OneAndHalfPercentSteps);
        rootTalent.TalentTreeName = "Support";

        // Left Tree
        rootTalent
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .OptionalTalent(BoostType.IncreasedHealing, ThreePercentSteps)
            .NextTalent(BoostType.TroopsTakeLessSkillDamage, TwoPercentSteps)
            .OptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedRageOnNormalAttack, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.ConvertSeriouslyWoundedToLightlyWounded, TwoSinglePercentSteps)
            .OptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .NextTalent(BoostType.IncreasedRageOnReceiveSkillAttack, new List<double> { 10.0 })
            .NextTalent(BoostType.TroopsTakeLessSkillDamage, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.ThreeSecondsAfterHitByActiveSkill);
        
        // Right tree
        rootTalent
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, new List<double> { 5.0, 10.0, 15.0 }, boostRestrictionType: BoostRestrictionType.TenSecondsAfterLeavingCity)
            .OptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedAttack, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.ThreeSecondsAfterHealing)
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.IncreasedDamage, FivePercentSteps, boostRestrictionType: BoostRestrictionType.FirstFiveSecondsOfBattle);

        return rootTalent;
    }
}