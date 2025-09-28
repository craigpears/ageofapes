using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Hitters;

public class Fiona
{
    public static Fighter GetFighter()
    {
        // Active Skill: Rage (1000 rage required)
        // Increases attack by 20% for 3 seconds
        // Throws consecutive punches for 3 seconds, dealing 500 damage per second to targets
        // Inflicts jewel mark on targets (not implemented - see comments)
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            // TODO: Implement consecutive punches (3 attacks with 500 damage factor each)
            // For now, modeling as one big attack for 1500 damage factor
            DamageFactor = 1500,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 20 },
                    DurationSeconds = 3
                }
                // TODO: Implement duel mark debuff - not currently modeled in the system
            }
        };

        // Passive Skill 1: Hitters gain 15% increased attack
        // Deals 5% more damage to targets with jewel marks (modeled as straight 5% damage increase)
        // Reduces damage dealt by jewel-marked targets by 20% (modeled as 20% damage taken reduction)
        var jewelMarkMastery = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 5 }
                    // TODO: This should only apply to targets with duel marks - not currently implemented
                },
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostAmounts = new List<double> { 20 }
                    // TODO: This should only apply when attacked by duel-marked enemies - not currently implemented
                }
            }
        };

        // Passive Skill 2: Hitters gain 15% increased defence
        // Targets with jewel marks have 10% chance to activate power punch (500 damage factor)
        // Power punch inflicts knockout (unable to counterattack for 1 second) and increases normal attack by 50% in that round
        var defensiveTechnique = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 500 },
                    Chance = 10,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                    // TODO: This should only trigger against duel-marked targets - not currently implemented
                    // TODO: Implement knockout effect (prevents counterattack for 1 second)
                    // TODO: Implement 50% normal attack increase for that round
                }
            }
        };

        // Passive Skill 3: Blocking Technique
        // Gains 15% increased health when troop strength is less than 50%
        // Attack is increased by 20%, defence is reduced by 10%
        // Jewel-marked enemies have 10% chance to reduce damage taken by 20%
        var blockingTechnique = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 15 },
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { -10 }
                    // Negative boost to reduce defence
                },
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostAmounts = new List<double> { 20 },
                    Chance = 10
                    // TODO: This should only apply when attacked by duel-marked enemies - not currently implemented
                }
            }
        };

        // Talent 1: Attack and Defend (Hitter talent)
        // When troop is comprised of only hitters, they take 20% less normal attack damage
        // Attacks have 10% chance to increase hitter attack by 20% and hitter defense by 10% for 3 seconds
        var attackAndDefend = new TalentSkill
        {
            Name = "Attack and Defend",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.ReducedDamageFromNormalAttacks,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 20 }
                    // TODO: This should only apply when troop is comprised of only hitters - not currently implemented
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 20 },
                    Chance = 10,
                    DurationSeconds = 3,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 10 },
                    Chance = 10,
                    DurationSeconds = 3,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                }
            },
            TalentTree = Hitter.GetTree()
        };

        // Talent 2: Confrontation (Balanced talent)
        // When fighting on the map, troops gain 20% defense, 10% march speed, and seriously wounded conversion increases by 5%
        var confrontation = new TalentSkill
        {
            Name = "Confrontation",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.MapBattle
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.MapBattle
                },
                new Boost
                {
                    BoostType = BoostType.ConvertSeriouslyWoundedToLightlyWounded,
                    BoostAmounts = new List<double> { 5 },
                    BoostRestrictionType = BoostRestrictionType.MapBattle
                }
            },
            TalentTree = Balanced.GetTree()
        };

        // Talent 3: Reinforced Punch (Skill talent)
        // Increases normal attack damage by 10%
        // After releasing a skill, enhances attack by 20%
        // Increases normal attack damage by 20%
        var reinforcedPunch = new TalentSkill
        {
            Name = "Reinforced Punch",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterActiveSkillRelease
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    BoostAmounts = new List<double> { 20 }
                }
            },
            TalentTree = Skill.GetTree()
        };

        var fiona = new Fighter
        {
            Name = "Fiona",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                activeSkill, jewelMarkMastery, defensiveTechnique, blockingTechnique
            },
            TalentSkills = new List<TalentSkill>
            {
                attackAndDefend, confrontation, reinforcedPunch
            }
        };

        return fiona;
    }
}
