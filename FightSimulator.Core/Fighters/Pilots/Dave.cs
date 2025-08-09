using FightSimulator.Core.FighterSimulator.TalentTrees.Applications;
using FightSimulator.Core.FighterSimulator.TalentTrees.Type;
using FightSimulator.Core.FighterSimulator.TalentTrees.Unit;

namespace FightSimulator.Core.FighterSimulator.Fighters.Pilots;

public class Dave
{
    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1400,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.SkillDamageFactor,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 400 },    
                    Chance = 9 // TODO: Need to improve this so it is an extension of the active skill, this is a good enough approximation though
                },
                new Boost
                {
                    BoostType = BoostType.ReduceEnemyMarchingSpeed,
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.Intimidate,
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease
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
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostRestrictionType = BoostRestrictionType.TroopNumberGreaterThanEnemy,
                    BoostAmounts = new List<double> { 10 }
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
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { -5 }
                },
                new Boost
                {
                    // TODO: Need to implement the detail for the extra damage at various percentages, just give him some global extra damage for now
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 10 }
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
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.Intimidate,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 15 }
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
                    BoostAmounts = new List<double> { 40 },
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 20 },
                    // TODO: Approximating for now until boost type is implemented
                    // BoostAmounts = new List<double> { 40 },
                    // Chance = 10,
                    // BoostRestrictionType = BoostRestrictionType.AfterNormalAttack,
                    // DurationSeconds = 4,
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
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.AfterTakingSkillDamage,
                    BoostAmounts = new List<double> { 25 },
                    DurationSeconds = 1
                },
            },
            TalentTree = Conqueror.GetTree()
        };

        var tallentSkill3 = new TalentSkill
        {
            Name = "Talent Skill 3",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.SkillDamageFactor,
                    BoostAmounts = new List<double> { 500 },
                    BoostRestrictionType = BoostRestrictionType.AfterActiveSkillRelease,
                    Chance = 30,
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 40 },
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease,
                    Chance = 30,
                },
            },
            TalentTree = Skill.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Dave",
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