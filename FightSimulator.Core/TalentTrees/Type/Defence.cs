using FightSimulator.Core.Models;

namespace FightSimulator.Core.TalentTrees.Type;

public class Defence : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, TwoHalfPercentSteps);
        rootTalent.TalentTreeName = "Defence";

        // Left Tree
        var leftTree = rootTalent
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, new List<double> { 0.6, 1.2, 1.8 })
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .NextTalent(BoostType.TroopsTakeLessSkillDamage, TwoPercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .OptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.TakesLessDamage, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent);

        var combatFitness = leftTree.NextTalent(BoostType.DamageTakenReduced, new List<double> { 3.0, 6.0, 9.0, 12.0 });
        combatFitness.Boosts.First().Chance = 10;
        combatFitness.Boosts.First().DurationSeconds = 1;

        combatFitness.NextTalent(BoostType.DamageTakenReduced, new List<double> { 1.5, 3.0, 4.5, 6.0 }, boostRestrictionType: BoostRestrictionType.HealthBelowHalf);
        
        // Right tree
        rootTalent
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedCounterAttackDamage, HalfPercentSteps)
            .NextTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.IncreasedRageOnReceiveNormalAttack, new List<double> { 6, 12, 18, 24 })
            .OptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .OptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.TakesLessDamage, OnePercentSteps).WithExtraBoost(BoostType.IncreasedDamage, MinusHalfPercentSteps)
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, new List<double> { 1.5, 3.0, 4.5, 6.0 }, boostRestrictionType: BoostRestrictionType.TwoSecondsAfterActiveSkillRelease);
        
        return rootTalent;
    }
}