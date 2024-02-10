namespace BlazorApp1.Shared.FighterSimulator.Fighters;

public class Derrick
{
    private static List<double> HalfStepUnits = new List<double> { 0.5, 1, 1.5};
    private static List<double> SingleStepUnits = new List<double> { 1, 2, 3};
    private static List<double> OneAndHalfStepUnits = new List<double> { 1.5, 3.0, 4.5};
    
    private static List<double> OnePercentSteps = new List<double> { 1.0, 2.0, 3.0 };
    private static List<double> TwoPercentSteps = new List<double> { 2.0, 4.0, 6.0 };
    private static List<double> ThreePercentSteps = new List<Double> { 3.0, 6.0, 9.0 };
    
    private static List<double> TwoHalfStepUnits = new List<double> { 0.5, 1};
    private static List<double> TwoSingleStepUnits = new List<double> { 1.0, 2.0};
    
    private static List<double> HalfPercent = new List<double> { 0.5 };
    private static List<double> OnePercent = new List<double> { 1 };
    private static List<double> OneAndAHalfPercent = new List<double> { 1.5 };
    
    private static List<double> MinusOnePercentSteps = new List<double> { -1, -2, -3 };
    
    public static Fighter GetFighter()
    {
        var armedAssault = new TalentSkill
        {
            Name = "Armed Assault",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 10, 15, 20, 25, 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 10, 15, 20, 25, 30 }
                }
            },
            RootTalent = new Talent(BoostType.IncreasedAttack, HalfPercent)
        };

        // Left Tree
        armedAssault.RootTalent
            .WithNextTalent(BoostType.IncreasedAttack, HalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedDamage, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedMaxTroops, SingleStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            // Not modelling dependency on previous optional talent, shouldn't affect outcomes significantly
            .WithOptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, HalfStepUnits)
            .WithOptionalTalent(BoostType.TakesLessDamage, HalfStepUnits, TroopType.ThreeUnitTypes)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealing, OnePercent)
            .WithNextTalent(BoostType.IncreasedDamage, new List<Double> { 2, 4, 6, 8 }, TroopType.ThreeUnitTypes)
            .WithNextTalent(BoostType.IncreasedAttack, new List<Double> { 2, 4, 6, 8, 10 },
                boostRestrictionType: BoostRestrictionType.LeadingRally);

        // Right Tree
        armedAssault.RootTalent
            .WithNextTalent(BoostType.IncreasedDefence, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealing, new List<Double> { 3, 6, 9 })
            .WithNextTalent(BoostType.IncreasedRageOnReceiveNormalAttack, new List<Double> { 2, 4, 6 })
            .WithNextTalent(BoostType.IncreasedAttack, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedAttack, OneAndHalfStepUnits,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .WithExtraBoost(BoostType.IncreasedDefence, OneAndHalfStepUnits,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, OneAndHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, new List<Double> { 20.0 },
                boostRestrictionType: BoostRestrictionType.TwoSecondsAfterActiveSkillRelease);

        var moraleBoost = new TalentSkill
        {
            Name = "Morale Boost",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.ReducedDeadUnits,
                    BoostRestrictionType = BoostRestrictionType.AttackingCitiesOnly,
                    BoostAmounts = new List<double> { 3, 4, 6, 8, 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.AttackingCitiesOnly,
                    BoostAmounts = new List<double> { 3, 4, 6, 8, 10 }
                }
            },
            RootTalent = new Talent(BoostType.IncreasedAttack, HalfPercent)
        };

        // Left Tree
        moraleBoost.RootTalent
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
        moraleBoost.RootTalent.WithNextTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
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

        var simpleAndEffective = new TalentSkill
        {
            Name = "Simple & Effective",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { -10, -10, -10, -10, -10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 6, 9, 12, 16, 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 3, 4, 6, 8, 10 }
                }
            },
            RootTalent = new Talent(BoostType.IncreasedMaxTroops, HalfPercent)
        };
        
        // Left Tree
        simpleAndEffective.RootTalent
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            // Anyone interested in this tool is going to have 5 star fighters, so don't model attack per star
            .WithNextTalent(BoostType.IncreasedAttack, HalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedCounterAttackDamage, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .WithOptionalTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedDamage, TwoPercentSteps)
                .WithExtraBoost(BoostType.EnemySkillDamageReduced, OnePercentSteps)
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedHealing, OneAndAHalfPercent)
            .WithNextTalent(BoostType.IncreasedDamage, ThreePercentSteps,
                boostRestrictionType: BoostRestrictionType.FirstFiveSecondsOfBattle);
        
        // Right tree
        simpleAndEffective.RootTalent
            .WithNextTalent(BoostType.IncreasedDefence, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedDamage, OneAndHalfStepUnits,
                boostRestrictionType: BoostRestrictionType.HealthBelowHalf)
            .WithNextTalent(BoostType.IncreasedRageOnReceiveNormalAttack, TwoPercentSteps)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithNextTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .WithOptionalTalent(BoostType.IncreasedDefence, HalfPercent)
            .WithOptionalTalent(BoostType.IncreasedAttack, SingleStepUnits)
                .WithExtraBoost(BoostType.IncreasedDefence, MinusOnePercentSteps)
            .WithNextTalent(BoostType.IncreasedDefence, TwoSingleStepUnits)
            .WithNextTalent(BoostType.IncreasedSkillDamage, new List<double> { 3.0, 6.0, 9.0, 12.0 },
                boostRestrictionType: BoostRestrictionType.FiveSecondsAfterActiveSkillRelease);

        var derrick = new Fighter
        {
            Name = "Derrick",
            CanTalentLeap = true,
            TalentSkills = new List<TalentSkill>
            {
                armedAssault, moraleBoost, simpleAndEffective
            }
        };

        return derrick;
    }
}