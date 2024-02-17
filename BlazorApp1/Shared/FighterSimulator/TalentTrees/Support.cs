namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Support : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedMarchingSpeed, OneAndHalfStepUnits);
        rootTalent.TalentTreeName = "Support";

        // Left Tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealing, ThreePercentSteps)
            .WithNextTalent(BoostType.TroopsTakeLessSkillDamage, TwoPercentSteps)
            .WithOptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, HalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedRageOnNormalAttack, ThreePercentSteps)
            .WithNextTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.ConvertSeriouslyWoundedToLightlyWounded, TwoSingleStepUnits)
            .WithOptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, HalfStepUnits)
            .WithOptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .WithNextTalent(BoostType.IncreasedRageOnReceiveSkillAttack, new List<double> { 10.0 })
            .WithNextTalent(BoostType.TroopsTakeLessSkillDamage, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.ThreeSecondsAfterHitByActiveSkill);
        
        // Right tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedDefence, HalfPercent)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, new List<double> { 5.0, 10.0, 15.0 }, boostRestrictionType: BoostRestrictionType.TenSecondsAfterLeavingCity)
            .WithOptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedHealth, HalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedAttack, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.ThreeSecondsAfterHealing)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .WithOptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .WithNextTalent(BoostType.IncreasedDamage, FivePercentSteps, boostRestrictionType: BoostRestrictionType.FirstFiveSecondsOfBattle);

        return rootTalent;
    }
}