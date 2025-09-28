using FightSimulator.Core.Models;

namespace FightSimulator.Core.TalentTrees.Unit;

public class Hitter : TalentClass
{
    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedDefence, TwoHalfPercentSteps);
        rootTalent.TalentTreeName = "Hitter";

        // Left Tree
        var leftTree = rootTalent
            .NextTalent(BoostType.IncreasedAttack, TwoSinglePercentSteps, TroopType.Hitter)
            .OptionalTalent(BoostType.IncreasedDefence, TwoPercentSteps, TroopType.Hitter, boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .NextTalent(BoostType.IncreasedDamageToCounteredUnit, ThreePercentSteps)
            .OptionalTalent(BoostType.TroopsTakeLessSkillDamage, OnePercent)
            .NextTalent(BoostType.IncreasedHealth, TwoSinglePercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps);
            
        var conditionalDefence = leftTree.OptionalTalent(BoostType.IncreasedDefence, TwoPercentSteps, 
            boostRestrictionType: BoostRestrictionType.AfterNormalAttack);
        conditionalDefence.Boosts.First().Chance = 10;
        conditionalDefence.Boosts.First().DurationSeconds = 2;
        
        leftTree
            .NextTalent(BoostType.IncreasedAttack, TwoSinglePercentSteps, TroopType.Hitter)
            .NextTalent(BoostType.IncreasedDefence, TwoSinglePercentSteps, TroopType.Hitter)
            .OptionalTalent(BoostType.IncreasedHealth, OnePercent, TroopType.Hitter)
            .NextTalent(BoostType.IncreasedHealth, OnePercent, TroopType.Hitter)
            .NextTalent(BoostType.TakesLessDamage, OnePercentSteps, TroopType.Hitter)
            .NextTalent(BoostType.IncreasedDefence, new List<double> { 0.5, 1.0, 1.5, 2.0, 2.5 }, TroopType.Hitter)
            .WithExtraBoost(BoostType.IncreasedHealth, new List<double> { 0.5, 1.0, 1.5, 2.0, 2.5 }, TroopType.Hitter);

        // Right Tree
        var rightTree = rootTalent
            .NextTalent(BoostType.IncreasedDefence, TwoSinglePercentSteps, TroopType.Hitter)
            .NextTalent(BoostType.IncreasedDamage, HalfPercentSteps, boostRestrictionType: BoostRestrictionType.AfterNormalAttack)
            .NextTalent(BoostType.IncreasedAttack, TwoSinglePercentSteps, TroopType.Hitter)
            .NextTalent(BoostType.IncreasedDefence, TwoSinglePercentSteps, TroopType.Hitter)
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, OnePercentSteps)
            .NextTalent(BoostType.IncreasedHealth, TwoSinglePercentSteps, TroopType.Hitter)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .OptionalTalent(BoostType.TroopsTakeLessSkillDamage, new List<double> { 2.0 })
            .NextTalent(BoostType.IncreasedDefence, TwoSinglePercentSteps, TroopType.Hitter)
            .OptionalTalent(BoostType.IncreasedHealth, OnePercent, TroopType.Hitter)
            .NextTalent(BoostType.IncreasedRageOnNormalAttack, new List<double> { 3.0, 6.0, 9.0, 12.0 });

        return rootTalent;
    }
}
