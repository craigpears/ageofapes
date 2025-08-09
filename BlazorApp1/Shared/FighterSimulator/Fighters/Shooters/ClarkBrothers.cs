using BlazorApp1.Shared.FighterSimulator.TalentTrees;

namespace BlazorApp1.Shared.FighterSimulator.Fighters.Shooters;

public class ClarkBrothers
{
    public static Fighter GetFighter()
    {
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 550,
            MaxTargets = 3,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { -10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 20 }
                },
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
                    BoostAmounts = new List<double> { 25 }
                },
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostRestrictionType = BoostRestrictionType.NotDisguised,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    BoostRestrictionType = BoostRestrictionType.NotDisguised,
                    BoostAmounts = new List<double> { -20 }
                },
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
                    BoostAmounts = new List<double> { 25 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.HealthBelow70
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
                    BoostType = BoostType.ReducedDamageFromNormalAttacks,
                    BoostAmounts = new List<double> { 5 },
                    BoostRestrictionType = BoostRestrictionType.DefendingAgainstMultipleTroops
                },
                new Boost
                {
                    BoostType = BoostType.CrowdControlShield,
                    BoostAmounts = new List<double> { 1 },
                    Chance = 10
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
                    BoostAmounts = new List<double> { 30 },
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 30 },
                    Chance = 10,
                    DurationSeconds = 3
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
                    BoostType = BoostType.IncreasedAttack,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 30 },
                    Chance = 10,
                    DurationSeconds = 3
                },
                new Boost
                {
                    BoostType = BoostType.ConvertDefeatedToSeriouslyWounded,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 5 }
                },
            },
            TalentTree = Conqueror.GetTree()
        };

        var tallentSkill3 = new TalentSkill
        {
            Name = "Talent SKill 3",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    // TODO: This needs implementing
                    BoostType = BoostType.IncreasedActiveSkillDuration,
                    BoostAmounts = new List<double> { 1 },
                    Chance = 30
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 30 },
                    DurationSeconds = 3
                },
            },
            TalentTree = Skill.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Clark Brothers",
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