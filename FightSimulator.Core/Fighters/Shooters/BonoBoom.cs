using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Shooters;

public class BonoBoom
{
    
    private const int MajorGeneralHonourFactor = 5;

    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 600,
            MaxTargets = 3,
            AdditionalDamageChance = 25,
            AdditionalDamageFactor = 550 * MajorGeneralHonourFactor,
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
                    BoostType = BoostType.IncreasedDefence, // Assuming equal clan honours for a fair fight here
                    BoostAmounts = new List<double> { 20 }
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
                    // TODO: Is attack bonus the same as increased attack?
                    BoostAmounts = new List<double> { 10 + (2 * MajorGeneralHonourFactor) },
                    BoostRestrictionType = BoostRestrictionType.HealthAbove80
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    // TODO: Is attack bonus the same as increased attack?
                    BoostAmounts = new List<double> { 10 + (2 * MajorGeneralHonourFactor) },
                    BoostRestrictionType = BoostRestrictionType.HealthBelow80
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
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.EnemySkillDamageReduced,
                    BoostAmounts = new List<double> { 10 }
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
                    BoostType = BoostType.SkillDamageFactor,
                    Chance = 10,
                    BoostAmounts = new List<double> { 24 * MajorGeneralHonourFactor }
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
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                    BoostAmounts = new List<double> { 15 }
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
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    BoostAmounts = new List<double> { -5 },
                    TroopRestriction = TroopType.Hitter
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    BoostAmounts = new List<double> { -5 },
                    TroopRestriction = TroopType.Pilot
                },
            },
            TalentTree = Attacker.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Bono Boom",
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