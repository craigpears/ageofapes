using FightSimulator.Core.Models;

namespace FightSimulator.Core.TalentTrees.Type;

public class Attacker : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedMaxTroops, HalfPercent);
        rootTalent.TalentTreeName = "Attacker";

        // Left Tree
        rootTalent
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            // Anyone interested in this tool is going to have 5 star fighters, so don't model attack per star
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .OptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .NextTalent(BoostType.IncreasedCounterAttackDamage, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .OptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .OptionalTalent(BoostType.IncreasedDamage, TwoPercentSteps)
                .WithExtraBoost(BoostType.EnemySkillDamageReduced, OnePercentSteps)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedDamage, ThreePercentSteps,
                boostRestrictionType: BoostRestrictionType.FirstFiveSecondsOfBattle);
        
        // Right tree
        rootTalent
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedDamage, OneAndHalfPercentSteps,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .NextTalent(BoostType.IncreasedRageOnReceiveNormalAttack, TwoPercentSteps)
            .OptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .OptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .OptionalTalent(BoostType.IncreasedAttack, SinglePercentSteps)
                .WithExtraBoost(BoostType.IncreasedDefence, MinusOnePercentSteps)
            .NextTalent(BoostType.IncreasedDefence, TwoSinglePercentSteps)
            .NextTalent(BoostType.IncreasedSkillDamage, new List<double> { 3.0, 6.0, 9.0, 12.0 },
                boostRestrictionType: BoostRestrictionType.FiveSecondsAfterActiveSkillRelease);

        return rootTalent;
    }
}