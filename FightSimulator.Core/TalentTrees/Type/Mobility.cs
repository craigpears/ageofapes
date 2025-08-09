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
                .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent);
        
        // TODO:
        /*
         * Okay, finishing off that left tree. The next talent is two steps of half a percent increased defense with an optional talent, a single step that just isn't relevant, so use that non-combat boost for that. The next talent, again, is non-combat. There are three steps. And the next one after that, again, is a non-combat thing with three steps.
         * Then going on to the right-hand side of the tree, there is a talent for half a percent increased attack, single step, and then the next talent is a single step that's non-combat related. The talent after that is three half a percent increased defense steps. The next talent is three non-combat steps. The talent after that is two non-combat steps. The talent after that is two half a percent increased health. And the talent after that is three non-combat steps.
         */

        return root;
    }
}


