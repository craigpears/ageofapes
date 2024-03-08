namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Defence : TalentClass
{

    public static Talent GetTree()
    {
        throw new NotImplementedException();
        var rootTalent = new Talent(BoostType.IncreasedAttack, OnePercent);
        rootTalent.TalentTreeName = "Defence";

        // Left Tree
        var leftTree = rootTalent
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .NextTalent(BoostType.IncreasedDamageToCounteredUnit, ThreePercentSteps)
            .OptionalTalent(BoostType.IncreasedAttack, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .NextTalent(BoostType.IncreasedHealth, OnePercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedDefence, OnePercent);
            
        leftTree.NextTalents.Add(new Talent
            {
                 Boosts = new List<Boost>
                 {
                     new Boost
                     {
                         BoostAmounts = new List<double> { 2.0, 4.0, 6.0 },
                         Chance = 10,
                         DurationSeconds = 2
                     }
                 },
                  LastRequiredTalent = leftTree.LastRequiredTalent,
                  Optional = true,
                  TalentDepth = leftTree.TalentDepth,
                  RootTalent = leftTree.RootTalent,
                  TalentTreeName = leftTree.TalentTreeName
            });

        leftTree.NextTalent(BoostType.IncreasedAttack, OnePercentSteps)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, OnePercent)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.ReduceEnemyDefence, new List<double> { 1.5, 3.0, 4.5, 6.0 })
            .NextTalent(BoostType.IncreasedSkillDamage, new List<double> { 2.0, 4.0, 6.0, 8.0, 10.0 });
        
        // Right tree
        rootTalent
            .NextTalent(BoostType.IncreasedHealth, TwoSinglePercentSteps)
            .NextTalent(BoostType.IncreasedAttack, OnePercentSteps)
            .NextTalent(BoostType.IncreasedDefence, OnePercentSteps)
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedRageOnNormalAttack, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedAttack, OnePercentSteps)
            .OptionalTalent(BoostType.IncreasedDefence, OnePercent)
            .OptionalTalent(BoostType.IncreasedHealth, OnePercent)
            .NextTalent(BoostType.IncreasedAttack, OnePercent)
            .NextTalent(BoostType.IncreasedHealth, OnePercent)
            .NextTalent(BoostType.IncreasedDamage, new List<double> { 0.5, 1.0, 1.5, 2.0 });

        return rootTalent;
    }
}