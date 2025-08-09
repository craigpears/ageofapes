using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Leaders;

public class Rodruigez
{
    public static Fighter GetFighter()
    {
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 400,  //TODO: Implement continuous damage being lower
            MaxTargets = 3,
            Boosts = new List<Boost> { }
        };

        var passiveSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                
            }
        };

        var passiveSkill2 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedMaxTroops,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 5 }
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
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.TroopsTakeLessSkillDamage,
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
                    BoostType = BoostType.TakesLessDamage,
                    BoostAmounts = new List<double> { 15 },
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease
                },
            },
            TalentTree = Leader.GetTree()
        };



        var tallentSkill2 = new TalentSkill
        {
            Name = "Talent Skill 2",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    BoostAmounts = new List<double> { 20 },
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
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 15 },
                    Chance = 30
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    BoostAmounts = new List<double> { -10 },
                    DurationSeconds = 3
                },
            },
            TalentTree = Attacker.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Rodruigez",
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