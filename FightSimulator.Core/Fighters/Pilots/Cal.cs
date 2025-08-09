using FightSimulator.Core.FighterSimulator.TalentTrees.Applications;
using FightSimulator.Core.FighterSimulator.TalentTrees.Type;
using FightSimulator.Core.FighterSimulator.TalentTrees.Unit;

namespace FightSimulator.Core.FighterSimulator.Fighters.Pilots;

public class Cal
{
    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1700,
            MaxTargets = 4,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.RestoreRageAfterActiveSkill,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
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
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedRageOnNormalAttack,
                    BoostAmounts = new List<double> { 50 },
                    Chance = 5
                },
                new Boost
                {
                    BoostType = BoostType.ReduceEnemyRageOnNormalAttack,
                    BoostAmounts = new List<double> { 50 },
                    Chance = 5
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
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70,
                    BoostAmounts = new List<double> { -10 }
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
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle
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
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle,
                    BoostAmounts = new List<double> { 10 }
                },
            },
            TalentTree = Pilot.GetTree()
        };



        var tallentSkill2 = new TalentSkill
        {
            Name = "Talent Skill 2",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 10 }
                },
                // TODO: Add distributor attack
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    BoostRestrictionType = BoostRestrictionType.AttackingCitiesOnly,
                    BoostAmounts = new List<double> { 10 }
                },
            },
            TalentTree = Balanced.GetTree()
        };

        var tallentSkill3 = new TalentSkill
        {
            Name = "Talent SKill 3",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterTakingSkillDamage
                },
                new Boost
                {
                    BoostType = BoostType.Intimidate,
                    BoostAmounts = new List<double> { 1 },
                    Chance = 30,
                    DurationSeconds = 2,
                    BoostRestrictionType = BoostRestrictionType.AfterActiveSkillRelease
                },
            },
            TalentTree = Skill.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Cal",
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