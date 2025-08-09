using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Pilots;

public class Elmo
{
    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1500,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.SilenceTarget,
                    Chance = 20
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
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.Garrison
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamageToCounteredUnit,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamageFromCounteringUnit,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 5 }
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
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.CrowdControlShield,
                    Chance = 10,
                    DurationSeconds = 2,
                    BoostAmounts = new List<double> { 1 }
                },
                new Boost
                {
                    BoostType = BoostType.EnemySkillDamageReduced,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.Garrison
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
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.Garrison
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.MapBattle
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
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 15 }
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
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                    BoostAmounts = new List<double> { 10 }
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
                    BoostType = BoostType.TakesLessDamage,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterTakingSkillDamage
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.AfterTakingSkillDamage
                },
            },
            TalentTree = Defence.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Elmo",
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