using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Pilots;

public class Oscar
{
    public static Fighter GetFighter()
    {
        // Dragon's Breath
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 800,
            MaxTargets = 3,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    // Simulate 3s continuous damage after skill release
                    BoostType = BoostType.ContinuousDamage,
                    BoostAmounts = new List<double> { 300 },
                    DurationSeconds = 3,
                    BoostRestrictionType = BoostRestrictionType.AfterActiveSkillRelease
                },
                new Boost
                {
                    // Gains 10% attack after releasing skill
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease
                },
            }
        };

        var passiveSkill1 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    // Pilots and his troop up to 25% attack increase (modeled for Pilots)
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 25 }
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
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamageToNeutralUnits,
                    BoostAmounts = new List<double> { 40 },
                    BoostRestrictionType = BoostRestrictionType.AttackingNeutralUnits
                },
            }
        };

        // Partner in Crime
        var passiveSkill3 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    // 10% chance to gain 50% health for 2s after a normal attack
                    BoostType = BoostType.IncreasedHealth,
                    BoostAmounts = new List<double> { 50 },
                    Chance = 10,
                    DurationSeconds = 2,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                },
                new Boost
                {
                    // 10% chance to gain 30% attack for 2s after a normal attack
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 30 },
                    Chance = 10,
                    DurationSeconds = 2,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                },
                new Boost
                {
                    // Model the heal trigger (healing factor represented in HealingFactor field)
                    BoostType = BoostType.IncreasedHealing,
                    BoostAmounts = new List<double> { 300 },
                    Chance = 10,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                }
            }
        };

        // Talent skills
        var pilotTalent = new TalentSkill
        {
            Name = "On Alert",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    // When troop strength > 70%, pilots gain 20% attack
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    // First 10s: +30% attack (pilots)
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    // First 10s: +30% defence (pilots)
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.FirstTenSecondsOfBattle,
                    BoostAmounts = new List<double> { 30 }
                },
            },
            TalentTree = Pilot.GetTree()
        };

        var hunterTalent = new TalentSkill
        {
            Name = "Legendary Rider",
            Boosts = new List<Boost>(),
            // No special boosts; tree selected for combinations only
            TalentTree = Hunter.GetTree()
        };

        var mobilityTalent = new TalentSkill
        {
            Name = "Exhaust Pipe",
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
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 20 }
                },
            },
            TalentTree = Mobility.GetTree()
        };

        var fighter = new Fighter
        {
            Name = "Oscar",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                activeSkill, passiveSkill1, passiveSkill2, passiveSkill3
            },
            TalentSkills = new List<TalentSkill>
            {
                pilotTalent, hunterTalent, mobilityTalent
            }
        };

        return fighter;
    }
}


