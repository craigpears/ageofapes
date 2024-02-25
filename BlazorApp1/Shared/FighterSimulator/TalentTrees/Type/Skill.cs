namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Skill : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, HalfPercentSteps);
        rootTalent.TalentTreeName = "Skill";

        // Left Tree
        rootTalent
            .NextTalent(BoostType.IncreasedActiveSkillDamage, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedActiveSkillDamage, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.FiveSecondsAfterActiveSkillRelease)
            .OptionalTalent(BoostType.IncreasedMasterFighterSkillDamage, OnePercent)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .OptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedActiveSkillDamage, TwoPercentSteps)
            .WithExtraBoost(BoostType.TroopsTakeLessSkillDamage, SinglePercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedActiveSkillDamage, TwoPercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedMasterFighterSkillDamage, OnePercent)
            .NextTalent(BoostType.RestoreRageAfterActiveSkill, new List<double> { 15.0, 30.0, 45.0, 60.0 });
        
        // Right tree
        var rightTree = rootTalent
            .NextTalent(BoostType.TroopsTakeLessSkillDamage, TwoPercentSteps)
            .NextTalent(BoostType.IncreasedRageOnNormalAttack, ThreePercentSteps)
            .OptionalTalent(BoostType.IncreasedDeputyFighterSkillDamage, OnePercent)
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .OptionalTalent(BoostType.IncreasedActiveSkillDamage, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.FiveSecondsAfterActiveSkillRelease)
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedDeputyFighterSkillDamage, OnePercent);
            
            var finalTalent = rightTree.NextTalent(BoostType.RestoreRageAfterActiveSkill, new List<double>{ 50, 100, 150, 200 });
            finalTalent.Boosts.First().Chance = 10;

        return rootTalent;
    }
}