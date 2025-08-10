using FightSimulator.Core.FighterSimulator;
using FightSimulator.Core.FighterSimulator.TalentTrees;

namespace FightSimulator.Core.TalentTrees.Type;

public class Mobility : TalentClass
{
    public static Talent GetTree()
    {
        var root = new Talent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
        {
            TalentTreeName = "Mobility"
        };

        // Left tree
        root
            .NextTalent(BoostType.IncreasedDefence, OneAndAHalfPercent)
                .OptionalTalent(BoostType.IncreasedCounterAttackDamage, OneAndHalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
                .OptionalTalent(BoostType.NonCombat, new List<double> { 0.0, 0.0, 0.0 })
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercentSteps)
                .OptionalTalent(BoostType.NonCombat, new List<double> { 0.0, 0.0, 0.0 })
                .OptionalTalent(BoostType.NonCombat, new List<double> { 0.0, 0.0, 0.0 })
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
                .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
                .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
                .OptionalTalent(BoostType.NonCombat, HalfPercent)
            .NextTalent(BoostType.NonCombat, ThreeHalfPercentSteps)
            .NextTalent(BoostType.NonCombat, ThreeHalfPercentSteps);

        // Right tree
        root.NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.NonCombat, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, ThreeHalfPercentSteps)
            .NextTalent(BoostType.NonCombat, ThreeHalfPercentSteps)
            .NextTalent(BoostType.NonCombat, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .NextTalent(BoostType.NonCombat, ThreeHalfPercentSteps);

        return root;
    }
}


