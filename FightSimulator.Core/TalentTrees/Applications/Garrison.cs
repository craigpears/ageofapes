namespace FightSimulator.Core.TalentTrees.Applications;

public class Garrison : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, HalfPercent);
        rootTalent.TalentTreeName = "Garrison";

        // Left Tree
        var leftTree = rootTalent
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, HalfPercent)
            .OptionalTalent(BoostType.IncreasedDamage, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.Garrison)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .OptionalTalent(BoostType.IncreasedSentryTowerDamage, FivePercentSteps, boostRestrictionType: BoostRestrictionType.Garrison)
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedHealing, TwoOneAndHalfPercentSteps)
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .OptionalTalent(BoostType.EnemySkillDamageReduced, FivePercentSteps, boostRestrictionType: BoostRestrictionType.Garrison)
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
            .NextTalent(BoostType.DamageTakenReduced, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.DefendingAgainstMultipleTroops)
            .NextTalent(BoostType.IncreasedAttack, new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 }, boostRestrictionType: BoostRestrictionType.Garrison)
            .WithExtraBoost(BoostType.IncreasedDefence, new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 }, boostRestrictionType: BoostRestrictionType.Garrison)
            .WithExtraBoost(BoostType.IncreasedHealth, new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 }, boostRestrictionType: BoostRestrictionType.Garrison);
            
        // Right tree
        rootTalent
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, HalfPercent)
            .OptionalTalent(BoostType.DamageTakenReduced, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.Garrison)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedDefenceOfSentryTowers, FivePercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercentSteps)
            .OptionalTalent(BoostType.RestoreRageWhenAttacked, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.Garrison)
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .NextTalent(BoostType.IncreasedActiveSkillDamage, FivePercentSteps, boostRestrictionType: BoostRestrictionType.Garrison);

        return rootTalent;
    }
}