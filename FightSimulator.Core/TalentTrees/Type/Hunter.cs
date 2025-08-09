namespace FightSimulator.Core.TalentTrees.Type;

public class Hunter : TalentClass
{
    public static Talent GetTree()
    {
        // Root: +1.5% marching speed (single level)
        var root = new Talent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
        {
            TalentTreeName = "Hunter"
        };

        // Left tree
        var left = root
            .NextTalent(BoostType.IncreasedAttack, HalfPercentSteps)
                .OptionalTalent(BoostType.IncreasedDamageToNeutralUnits, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.AttackingNeutralUnits)
            .NextTalent(BoostType.IncreasedSkillDamage, FivePercentSteps, boostRestrictionType: BoostRestrictionType.AttackingNeutralUnits)
                .OptionalTalent(BoostType.IncreasedDamageToNeutralUnits, OneAndAHalfPercent, boostRestrictionType: BoostRestrictionType.AttackingNeutralUnits)
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedHealth, OneAndHalfPercentSteps)
                .OptionalTalent(BoostType.IncreasedDefence, OneAndAHalfPercent)
                .OptionalTalent(BoostType.IncreasedHealing, ThreePercentSteps)
            .NextTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
                .OptionalTalent(BoostType.IncreasedDamageToNeutralUnits, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.LeadingRally)
                .OptionalTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
                .OptionalTalent(BoostType.TakesLessDamage, OnePercent, boostRestrictionType: BoostRestrictionType.AttackingNeutralUnits)
                .OptionalTalent(BoostType.DamageTakenReduced, OnePercent, boostRestrictionType: BoostRestrictionType.AttackingNeutralUnits)
            .NextTalent(BoostType.IncreasedDamageToNeutralUnits, ThreePercentSteps, boostRestrictionType: BoostRestrictionType.AttackingNeutralUnits);

        // Right tree
        root
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedAttack, new List<double> { 0.0, 0.0, 0.0 })
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, OneAndAHalfPercent)
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, HalfPercentSteps)
            .NextTalent(BoostType.TakesLessDamage, TwoPercentSteps, boostRestrictionType: BoostRestrictionType.AttackingNeutralUnits)
            .NextTalent(BoostType.IncreasedAttack, new List<double> { 0.0, 0.0, 0.0 });

        return root;
    }
}


