using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Gatherers;

public class Vance
{
    public static Fighter GetFighter()
    {
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            ShieldFactor = 600, // TODO: Needs implementing
            AdditionalShieldFactor = 300, // TODO: Needs implementing
            MaxTargets = 3,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostAmounts = new List<double> { 30 },
                    BoostType = BoostType.IncreasedAttack,
                    BoostsAllies = true,  // TODO: Needs implementing
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease
                },
                new Boost
                {
                    BoostAmounts = new List<double> { 30 },
                    BoostsAllies = true,
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease
                }
            }
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
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.GatheringResources,
                    BoostAmounts = new List<double> { 20 }
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
                    BoostAmounts = new List<double> { 20 },
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.WallBreaker,
                },
                new Boost
                {
                    BoostAmounts = new List<double> { 3 },
                    BoostType = BoostType.IncreasedRageOnReceiveNormalAttack,
                },
                new Boost
                {
                    BoostAmounts = new List<double> { 3 },
                    BoostType = BoostType.IncreasedRageOnReceiveNormalAttack,
                    BoostRestrictionType = BoostRestrictionType.GatheringResources,
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
                    BoostAmounts = new List<double> { 20 },
                    TroopRestriction = TroopType.WallBreaker,
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    BoostAmounts = new List<double> { 20 },
                    TroopRestriction = TroopType.WallBreaker,
                },
            },
            TalentTree = Leader.GetTree()
        };



        var tallentSkill2 = new TalentSkill
        {
            Name = "Talent Skill 2",
            Boosts = new List<Boost>
            {
                
            },
            TalentTree = Gatherer.GetTree()
        };

        var tallentSkill3 = new TalentSkill
        {
            Name = "Talent SKill 3",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.ReducedDamageFromNormalAttacks,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease
                },
                new Boost
                {
                    BoostType = BoostType.RestoreRageAfterActiveSkill,
                    BoostAmounts = new List<double> { 30 },
                    BoostRestrictionType = BoostRestrictionType.AfterActiveSkillRelease,
                    Chance = 10
                },
            },
            TalentTree = Support.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Vance",
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