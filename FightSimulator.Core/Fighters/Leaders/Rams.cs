using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Leaders;

public class Rams
{
    public static Fighter GetFighter()
    {
        // Active Skill: Rage Required 1000, resists 1000 shield damage, troops gain 15% increased damage and take 15% less skill damage for 2 seconds
        var rageRequired = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1000, // TODO: This should be shield damage resistance, not damage inflicted
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 15 },
                    DurationSeconds = 2
                },
                new Boost
                {
                    BoostType = BoostType.TroopsTakeLessSkillDamage,
                    BoostAmounts = new List<double> { 15 },
                    DurationSeconds = 2
                }
            }
        };

        // Passive Skill 1: Eye for an Eye - 10% chance for troops to gain 50% increased attack for 2 seconds when attacking
        // When receiving an active skill attack from enemy, 15% chance to deal 1000 damage factor in return
        var eyeForAnEye = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 50 },
                    Chance = 10,
                    DurationSeconds = 2,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                },
                // TODO: Implement retaliation damage when receiving active skill attacks (15% chance, 1000 damage factor)
            }
        };

        // Passive Skill 2: Hero Against Many - Troop Defense increased by 2%, when attacked by multiple troops gains 20% normal attack damage reduction
        var heroAgainstMany = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 2 }
                },
                new Boost
                {
                    BoostType = BoostType.ReducedDamageFromNormalAttacks,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.DefendingAgainstMultipleTroops
                }
            }
        };

        // Passive Skill 3: Troops gain 10% increased health, when number of units is below 50% they gain 20% increased normal attack damage
        var troopDefense = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedNormalAttackDamage,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf
                }
            }
        };

        // Leader Talent: Troops under Rams get 10% increased capacity and 10% increased rally capacity
        // When number of troops in rally is higher than 50%, they gain 15% increased defence
        var leaderTalent = new TalentSkill
        {
            Name = "Leader",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedMaxTroops,
                    BoostAmounts = new List<double> { 10 }
                },
                // TODO: Implement rally capacity increase (10%)
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 15 },
                    BoostRestrictionType = BoostRestrictionType.LeadingRally // TODO: Should be when rally troops > 50%
                }
            },
            TalentTree = Leader.GetTree()
        };

        // Garrison Talent: When defending cities and territory buildings, gains 10% damage reduction and deals 10% more damage to enemy rally troops
        var garrisonTalent = new TalentSkill
        {
            Name = "Garrison",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.Garrison
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.AgainstRallys
                }
            },
            TalentTree = Garrison.GetTree()
        };

        // Defence Talent: Total Defence - Troops gain 10% increased counter-attack damage
        // Normal attacks have 10% chance to increase troop defence by 50% for 2 seconds
        var defenceTalent = new TalentSkill
        {
            Name = "Total Defence",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 50 },
                    Chance = 10,
                    DurationSeconds = 2,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                }
            },
            TalentTree = Defence.GetTree()
        };

        var rams = new Fighter
        {
            Name = "Rams",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                rageRequired, eyeForAnEye, heroAgainstMany, troopDefense
            },
            TalentSkills = new List<TalentSkill>
            {
                leaderTalent, garrisonTalent, defenceTalent
            }
        };

        return rams;
    }
}
