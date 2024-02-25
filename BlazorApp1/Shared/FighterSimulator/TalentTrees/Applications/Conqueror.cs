namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Conqueror : TalentClass
{
    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, HalfPercent);
        rootTalent.TalentTreeName = "Conqueror";
        
        // Left Tree
        rootTalent
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
            .NextTalent(BoostType.TakesLessDamageFromSentryTowers, ThreePercentSteps)
            .OptionalTalent(BoostType.IncreasedDamageToSentryTowers, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedDefence, HalfPercent)
            .OptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, TwoPercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, new List<double> { 1.5, 3.0 })
            .OptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .OptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, new List<double> { 2.0, 4.0, 6.0, 8.0 },
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly)
            .NextTalent(BoostType.ReducedDeadUnits, new List<double> { 2.0, 4.0, 6.0, 8.0, 10.0 },
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly);

        // Right Tree
        rootTalent
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedMaxTroops, SinglePercentSteps)
            .OptionalTalent(BoostType.TakesLessDamage, ThreePercentSteps,
                boostRestrictionType: BoostRestrictionType.HealthAbove90)
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .OptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .OptionalTalent(BoostType.IncreasedHealing, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .OptionalTalent(BoostType.IncreasedMarchingSpeed, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.LeadingRally)
            .NextTalent(BoostType.IncreasedHealth, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedDamage, new List<double> { 2.0, 4.0, 6.0, 8.0 },
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly);

        return rootTalent;
    }
}