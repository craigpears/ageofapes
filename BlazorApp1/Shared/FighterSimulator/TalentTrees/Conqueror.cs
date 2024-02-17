namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Conqueror : TalentClass
{
    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedAttack, HalfPercent);
        rootTalent.TalentTreeName = "Conqueror";
        
        // Left Tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedDefence, TwoHalfStepUnits)
            .WithNextTalent(BoostType.TakesLessDamageFromSentryTowers, ThreePercentSteps)
            .WithOptionalTalent(BoostType.IncreasedDamageToSentryTowers, ThreePercentSteps)
            .WithNextTalent(BoostType.IncreasedDefence, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, HalfStepUnits)
            .WithOptionalTalent(BoostType.TakesLessCounterAttackDamage, TwoPercentSteps)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, new List<double> { 1.5, 3.0 })
            .WithOptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithOptionalTalent(BoostType.TakesLessCounterAttackDamage, HalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, new List<double> { 2.0, 4.0, 6.0, 8.0 },
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly)
            .WithNextTalent(BoostType.ReducedDeadUnits, new List<double> { 2.0, 4.0, 6.0, 8.0, 10.0 },
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly);

        // Right Tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedMaxTroops, SingleStepUnits)
            .WithOptionalTalent(BoostType.TakesLessDamage, ThreePercentSteps,
                boostRestrictionType: BoostRestrictionType.HealthAbove90)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, HalfStepUnits)
            .WithOptionalTalent(BoostType.ReducedDamageFromNormalAttacks, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealing, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedMarchingSpeed, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.LeadingRally)
            .WithNextTalent(BoostType.IncreasedHealth, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedDamage, new List<double> { 2.0, 4.0, 6.0, 8.0 },
                boostRestrictionType: BoostRestrictionType.AttackingCitiesOnly);

        return rootTalent;
    }
}