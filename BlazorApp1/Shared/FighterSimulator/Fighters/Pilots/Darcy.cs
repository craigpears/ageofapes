using BlazorApp1.Shared.FighterSimulator.TalentTrees;

namespace BlazorApp1.Shared.FighterSimulator.Fighters.Pilots;

public class Darcy
{
    public static Fighter GetFighter()
    {
        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 600,
            MaxTargets = 3,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.Intimidate,
                    DurationSeconds = 1,
                    BoostRestrictionType = BoostRestrictionType.AfterActiveSkillRelease
                },
                new Boost
                {
                    BoostType = BoostType.SkillDamageFactor,
                    BoostAmounts = new List<double> { 600 },
                    BoostRestrictionType = BoostRestrictionType.AttackingNeutralUnits
                }
            }
        };

        var passiveSkill1 = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    BoostAmounts = new List<double> { 15 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 }
                }
                ,
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 5 },
                    BoostRestrictionType = BoostRestrictionType.WithOscar
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
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamageToNeutralUnits,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.AttackingNeutralUnits
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
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { 30 },
                    BoostRestrictionType = BoostRestrictionType.AttackingNeutralUnits
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 5 },
                    TroopRestriction = TroopType.Pilot
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { 20 },
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70
                }
            }
        };

        var pilotTalent = new TalentSkill
        {
            Name = "Seasoned Bravery",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.Pilot,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.Pilot,
                    BoostRestrictionType = BoostRestrictionType.AfterNormalAttack,
                    Chance = 10,
                    DurationSeconds = 2,
                    BoostAmounts = new List<double> { 30 }
                }
            },
            TalentTree = Pilot.GetTree()
        };

        var hunterTalent = new TalentSkill
        {
            Name = "Hitstreak",
            Boosts = new List<Boost>(),
            TalentTree = Attacker.GetTree()
        };

        var mobilityTalent = new TalentSkill
        {
            Name = "Rehabilitate",
            Boosts = new List<Boost>(),
            TalentTree = Support.GetTree()
        };

        var fighter = new Fighter
        {
            Name = "Darcy",
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


