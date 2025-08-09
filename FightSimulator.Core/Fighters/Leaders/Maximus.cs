using FightSimulator.Core.FighterSimulator.TalentTrees.Applications;
using FightSimulator.Core.FighterSimulator.TalentTrees.Type;
using FightSimulator.Core.FighterSimulator.TalentTrees.Unit;

namespace FightSimulator.Core.FighterSimulator.Fighters.Leaders;

public class Maximus
{
    public static Fighter GetFighter()
    {
        var devastatingBlow = new FighterSkill {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 1830, // TODO: Inflated this for now, put back to 1400 when all features have been implemented
            ChanceToAttackTwice = 14,
            MaxTargets = 3,
            Boosts = new List<Boost>
            {
                
            }
        };
        
        var thrillOfBattle = new FighterSkill{
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.SkillDamageFactor,
                    BoostAmounts = new List<double> { 1000 },  
                    Chance = 15,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode
                },
                
            }
        };
        
        var revitalizingPresence = new FighterSkill {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 50 },
                    Chance = 20,
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf,
                    DurationSeconds = 2
                },
                new Boost { 
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 25 },
                    Chance = 20,
                    BoostRestrictionType = BoostRestrictionType.HealthBelowHalf,
                    DurationSeconds = 2
                }
            }
        };
        
        var seigeCommander = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.ThreeUnitTypes,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.ThreeUnitTypes, 
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost { 
                    BoostType = BoostType.ConvertDefeatedToSeriouslyWounded,
                    TroopRestriction = TroopType.ThreeUnitTypes, 
                    BoostAmounts = new List<double> { 10 }
                },
            }
        };

        // TODO: Add healing and enemy attack reduces to hold the advantage
        var holdTheAdvantage = new TalentSkill
        {
            Name = "Hold the Advantage",
            Boosts = new List<Boost>
            {
            },
            TalentTree = Leader.GetTree()
        };
                  
        var championOfTheArena = new TalentSkill
        {
            Name = "Champion of the Arena",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 10 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode, 
                    BoostAmounts = new List<double> { 15 }
                },
            },
            TalentTree = Conqueror.GetTree()
        };
        
        var maskedGladiator = new TalentSkill
        {
            Name = "Masked Gladiator",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { -10 }
                }
            },
            TalentTree = Attacker.GetTree()                                                               
        };
        
        var maximus = new Fighter
        {
            Name = "Maximus",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                devastatingBlow, thrillOfBattle, revitalizingPresence, seigeCommander
            },
            TalentSkills = new List<TalentSkill>
            {
                holdTheAdvantage, championOfTheArena, maskedGladiator
            }
        };

        return maximus;

    }
}