using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Leaders;

public class Derrick
{
    
    public static Fighter GetFighter()
    {

        var mobiliseAndManeuver = new FighterSkill {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 500,
            CannonDamageFactor = 1500,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.FiveSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 10 },
                    DisabledInCannonMode = true
                },
                new Boost { 
                    BoostType = BoostType.IncreasedAttack,
                    BoostRestrictionType = BoostRestrictionType.FiveSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 20 },
                    DisabledInCannonMode = true
                },
                new Boost { 
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.FiveSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 20 },
                    DisabledInCannonMode = true
                },
                new Boost { 
                    BoostType = BoostType.IncreasedHealth,
                    BoostRestrictionType = BoostRestrictionType.FiveSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 20 },
                    DisabledInCannonMode = true
                },
            }
        };
        
        var seigeAndCapture = new FighterSkill{
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.WallBreaker, // TODO: This should be all wall breakers only
                    BoostAmounts = new List<double> { 40 }
                },
            }
        };
        
        var superArmor = new FighterSkill {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.AttackingCitiesOnly,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedDamage,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 20 },
                    Chance = 10, // TODO: This should be after normal attacks only
                    DurationSeconds = 3
                },
            }
        };
        
        var lockAndLoaded = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 20 }
                },  
                new Boost { 
                    BoostType = BoostType.IncreasedCounterAttackDamage,
                    BoostAmounts = new List<double> { 10 },
                    BoostRestrictionType = BoostRestrictionType.HealthAbove70
                },
            }
        };
        
        var armedAssault = new TalentSkill
        {
            Name = "Armed Assault",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedMarchingSpeed,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 30 }
                }
            },
            TalentTree = Leader.GetTree()                                                               
        };

        

        var moraleBoost = new TalentSkill
        {
            Name = "Morale Boost",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.ReducedDeadUnits,
                    BoostRestrictionType = BoostRestrictionType.AttackingCitiesOnly,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.AttackingCitiesOnly,
                    BoostAmounts = new List<double> { 10 }
                }
            },
            TalentTree = Conqueror.GetTree()
        };

        
                  
        var simpleAndEffective = new TalentSkill
        {
            Name = "Simple & Effective",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedSkillDamage,
                    BoostAmounts = new List<double> { -10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 10 }
                }
            },
            TalentTree = Attacker.GetTree()
        };
        
        

        var derrick = new Fighter
        {
            Name = "Derrick",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                mobiliseAndManeuver, seigeAndCapture, superArmor, lockAndLoaded
            },
            TalentSkills = new List<TalentSkill>
            {
                armedAssault, moraleBoost, simpleAndEffective
            }
        };

        return derrick;
    }
}