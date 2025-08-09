using BlazorApp1.Shared.FighterSimulator.TalentTrees;

namespace BlazorApp1.Shared.FighterSimulator.Fighters.Shooters;

public class Thor
{
    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1700, // TODO: Should be 1000 or 1700 depending on whether he is garrisoning
            MaxTargets = 99, // TODO: This should be targets within a semi-circle
            Boosts = new List<Boost>
            {
            }
        };

        var passiveSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 45 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                },
                // TODO: Normal attack damage increase when defending territory buildings
            }
        };

        var passiveSkill2 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Shooter,
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    BoostAmounts = new List<double> { 40 },
                    Chance = 15, // TODO: Need to check this is implemented
                    DurationSeconds = 3
                },
            }
        };

        var passiveSkill3 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 2 },
                    BoostRestrictionType = BoostRestrictionType.AttackedByMultipleEnemies
                },
            }
        };

        var tallentSkill = new TalentSkill
        {
            Name = "Talent Skill 1",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70,
                    BoostAmounts = new List<double> { 10 }
                },
            },
            TalentTree = Shooter.GetTree()
        };

        var tallentSkill2 = new TalentSkill
        {
            Name = "Talent Skill 2",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.SkillDamageFactor,
                    BoostRestrictionType = BoostRestrictionType.AfterTakingSkillDamage, // TODO: Should also be garrisoning
                    BoostAmounts = new List<double> { 500 },
                    Chance = 50
                },
                new Boost
                {
                    BoostType = BoostType.SkillDamageFactor,
                    BoostRestrictionType = BoostRestrictionType.AfterTakingSkillDamage, // TODO: Should also be garrisoning
                    BoostAmounts = new List<double> { 200 },
                    Chance = 20
                },
                new Boost
                {
                    BoostType = BoostType.SkillDamageFactor,
                    BoostRestrictionType = BoostRestrictionType.AgainstRallys,
                    BoostAmounts = new List<double> { 10 },
                },
            },
            TalentTree = Garrison.GetTree()
        };

        var tallentSkill3 = new TalentSkill
        {
            Name = "Talent SKill 3",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease
                },
            },
            TalentTree = Skill.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Thor",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                activeSkill, passiveSkill, passiveSkill2, passiveSkill3
            },
            TalentSkills = new List<TalentSkill>
            {
                tallentSkill, tallentSkill2, tallentSkill3
            }
        };

        return fighter;
    }
}