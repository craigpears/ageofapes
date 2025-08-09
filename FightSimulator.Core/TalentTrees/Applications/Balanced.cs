namespace FightSimulator.Core.TalentTrees.Applications;

public class Balanced : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, OneAndHalfPercentSteps);
        rootTalent.TalentTreeName = "Balanced";

        // Left Tree
        rootTalent
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .OptionalTalent(BoostType.IncreasedSentryTowerDamage, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedDefence, ThreePercentSteps,
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly)
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedDamage, OnePercentSteps,
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly)
            .NextTalent(BoostType.IncreasedSentryTowerDamage, new List<double> { 3.0, 6.0, 9.0, 12.0, 15.0 });
        
        // Right tree
        rootTalent
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.FewerActionPoints, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedDamageToSentryTowers, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedRageOnReceiveNormalAttack, TwoPercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedHealing, HalfPercentSteps)
            .OptionalTalent(BoostType.TroopsTakeLessSkillDamage, new List<double> { 2.0 })
            .NextTalent(BoostType.IncreasedNormalAttackDamage, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.FirstFiveSecondsOfBattle);

        return rootTalent;
    }
}