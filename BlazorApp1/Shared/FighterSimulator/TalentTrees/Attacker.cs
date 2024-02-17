namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Attacker : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedMaxTroops, HalfPercent);
        rootTalent.TalentTreeName = "Attacker";

        // Left Tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            // Anyone interested in this tool is going to have 5 star fighters, so don't model attack per star
            .WithNextTalent(BoostType.IncreasedAttack, HalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedCounterAttackDamage, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedDamage, TwoPercentSteps)
                .WithExtraBoost(BoostType.EnemySkillDamageReduced, OnePercentSteps)
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .WithNextTalent(BoostType.IncreasedDamage, ThreePercentSteps,
                boostRestrictionType: BoostRestrictionType.FirstFiveSecondsOfBattle);
        
        // Right tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedDefence, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedDamage, OneAndHalfStepUnits,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .WithNextTalent(BoostType.IncreasedRageOnReceiveNormalAttack, TwoPercentSteps)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .WithOptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedAttack, SingleStepUnits)
                .WithExtraBoost(BoostType.IncreasedDefence, MinusOnePercentSteps)
            .WithNextTalent(BoostType.IncreasedDefence, TwoSingleStepUnits)
            .WithNextTalent(BoostType.IncreasedSkillDamage, new List<double> { 3.0, 6.0, 9.0, 12.0 },
                boostRestrictionType: BoostRestrictionType.FiveSecondsAfterActiveSkillRelease);

        return rootTalent;
    }
}