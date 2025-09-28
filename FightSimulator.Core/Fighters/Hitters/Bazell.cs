using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Hitters;

public class Bazell
{
    public static Fighter GetFighter()
    {
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            HealingFactor = 800,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostAmounts = new List<double> { 20 },
                    DurationSeconds = 3
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    BoostAmounts = new List<double> { 20 },
                    DurationSeconds = 3
                }
            }
        };

        var counterAttackRampage = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 300 },
                    Chance = 10,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                }
            }
        };

        var thickSkinned = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.ReducedDamageFromNormalAttacks,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost
                {
                    BoostType = BoostType.ReducedDamageFromNormalAttacks,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.AttackedByMultipleEnemies
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    BoostAmounts = new List<double> { 5 },
                    BoostRestrictionType = BoostRestrictionType.AttackedByMultipleEnemies
                }
            }
        };

        var indomitableWill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf,
                    BoostAmounts = new List<double> { 20 }
                }
                // TODO: Implement active skill duration extension - 25% chance to extend damage reduction and counter-attack damage effects by 1 second after active skill release
            }
        };

        var hitterMastery = new TalentSkill
        {
            Name = "Hitter Mastery",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Hitter,
                    BoostAmounts = new List<double> { 30 },
                    Chance = 30,
                    DurationSeconds = 3,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                }
            },
            TalentTree = Hitter.GetTree()
        };

        var standingFirm = new TalentSkill
        {
            Name = "Standing Firm",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostRestrictionType = BoostRestrictionType.Garrison,
                    BoostAmounts = new List<double> { 15 }
                }
            },
            TalentTree = Garrison.GetTree()
        };

        var defensiveMastery = new TalentSkill
        {
            Name = "Defensive Mastery",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedHealing,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.DamageTakenReduced,
                    BoostAmounts = new List<double> { 30 },
                    Chance = 10,
                    DurationSeconds = 3,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack
                }
            },
            TalentTree = Defence.GetTree()
        };

        var bazell = new Fighter
        {
            Name = "Bazell",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                activeSkill, counterAttackRampage, thickSkinned, indomitableWill
            },
            TalentSkills = new List<TalentSkill>
            {
                hitterMastery, standingFirm, defensiveMastery
            }
        };

        return bazell;
    }
}
