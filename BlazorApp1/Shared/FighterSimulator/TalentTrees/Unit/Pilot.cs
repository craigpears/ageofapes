namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Pilot : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, SinglePercentSteps);
        rootTalent.TalentTreeName = "Pilot";

        // Left Tree
        var leftTree = rootTalent
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .OptionalTalent(BoostType.IncreasedDamage, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedDamageToCounteredUnit, ThreePercentSteps)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .NextTalent(BoostType.IncreasedHealth, OnePercent)
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedHealth, OnePercent)
            .OptionalTalent(BoostType.IncreasedAttack, OnePercent)
            .OptionalTalent(BoostType.IncreasedRageOnNormalAttack, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedDefence, OnePercentSteps)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.IncreasedAttack, OnePercentSteps);
            
       var destabilize = leftTree.NextTalent(BoostType.ReduceEnemyAttack, new List<double>{ 3.0, 6.0, 9.0, 12.0 });
       destabilize.Boosts.First().Chance = 10;
       destabilize.Boosts.First().DurationSeconds = 2;
            
       destabilize.NextTalent(BoostType.IncreasedDamage, new List<double>{ 3.0, 6.0, 9.0, 12.0, 15.0 }, boostRestrictionType: BoostRestrictionType.FirstTenSecondsOfBattle);
       throw new NotImplementedException();
        
        // Right tree
        //rootTalent;

        return rootTalent;
    }
}