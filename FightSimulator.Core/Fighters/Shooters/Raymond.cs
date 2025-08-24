using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Shooters;

public class Raymond
{
    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1700,
            MaxTargets = 5,
            Boosts = new List<Boost>
            {
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
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 45 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Shooter,
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamageToCounteredUnit,
                    TroopRestriction = TroopType.Shooter,
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
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Shooter,
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.ReflectTargetSkillDamage,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 10 }
                },new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.AfterTakingSkillDamage,
                    BoostAmounts = new List<double> { 20 },
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
                    BoostType = BoostType.Intimidate,
                    BoostAmounts = new List<double> { 1 },
                    Chance = 3,
                    DurationSeconds = 1
                },
                new Boost
                {
                    BoostType = BoostType.TakesLessCounterAttackDamage,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf
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
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70,
                    BoostAmounts = new List<double> { 20 }
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
                    BoostType = BoostType.IncreasedMaxTroops,
                    BoostAmounts = new List<double> { 5 },
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 10 },
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                    BoostAmounts = new List<double> { 5 },
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
                    BoostType = BoostType.TakesLessCounterAttackDamage,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack,
                    Chance = 10,
                    DurationSeconds = 2
                },
            },
            TalentTree = Defence.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Raymond",
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