using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Pilots;

public class TNY
{
    public static Fighter GetFighter()
    {
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1300,
            MaxTargets = 3,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.Intimidate,
                    BoostAmounts = new List<double> { 1 },
                    DurationSeconds = 2
                }
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
                }
            }
        };

        var passiveSkill2 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDamageToNeutralUnits,
                    BoostAmounts = new List<double> { 40 }
                }
            }
        };

        var passiveSkill3 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.ReduceEnemyDefence,
                    BoostAmounts = new List<double> { 20 },
                    Chance = 30,
                    DurationSeconds = 5,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                },
                new Boost
                {
                    BoostType = BoostType.ReduceEnemyAttack,
                    BoostAmounts = new List<double> { 10 },
                    Chance = 30,
                    DurationSeconds = 5,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                }
            }
        };

        var talentSkill1 = new TalentSkill
        {
            Name = "Pilot Talent",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 30 },
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 30 },
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle
                }
            },
            TalentTree = Pilot.GetTree()
        };

        var talentSkill2 = new TalentSkill
        {
            Name = "Hunter Talent",
            Boosts = new List<Boost>
            {
                // No valid boosts for hunter tree as mentioned by user
            },
            TalentTree = Hunter.GetTree()
        };

        var talentSkill3 = new TalentSkill
        {
            Name = "Skill Talent",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 30 },
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease
                }
            },
            TalentTree = Skill.GetTree()
        };

        var fighter = new Fighter
        {
            Name = "TNY",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                activeSkill, passiveSkill, passiveSkill2, passiveSkill3
            },
            TalentSkills = new List<TalentSkill>
            {
                talentSkill1, talentSkill2, talentSkill3
            }
        };

        return fighter;
    }
}
