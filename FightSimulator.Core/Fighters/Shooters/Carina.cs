using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Shooters;

public class Carina
{
    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1600,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.ReduceEnemyDefence,
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 20 }
                },
            }
        };

        var passiveSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            DamageFactor = 300,
            Chance = 10,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.ReduceEnemyHealing,
                    BoostAmounts = new List<double> { 20 },
                    Chance = 10
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
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 3 },
                    BoostRestrictionType = BoostRestrictionType.MultipliedByAdjacentAllies // TODO: Needs Implementing
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
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 40 },
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { -10 }
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
                    BoostType = BoostType.IncreasedDamageToCounteredUnit,
                    TroopRestriction = TroopType.Shooter,
                    BoostAmounts = new List<double> { 5 }
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
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    BoostRestrictionType = BoostRestrictionType.MapBattle,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf,
                    BoostAmounts = new List<double> { 10 }
                },
                
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf,
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
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 15 },
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease
                },
            },
            TalentTree = Skill.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Carina",
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