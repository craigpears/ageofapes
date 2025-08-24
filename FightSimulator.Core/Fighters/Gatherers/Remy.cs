using FightSimulator.Core.Models;
using FightSimulator.Core.TalentTrees.Applications;
using FightSimulator.Core.TalentTrees.Type;
using FightSimulator.Core.TalentTrees.Unit;

namespace FightSimulator.Core.Fighters.Gatherers;

public class Remy
{
    
    public static Fighter GetFighter()
    {
        var devotedProtector = new FighterSkill {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            Boosts = new List<Boost>
            {
                // TODO: This should be able to boost two allied troops as well
                new Boost { 
                    BoostType = BoostType.IncreasedHealth,
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterHitByActiveSkill,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterHitByActiveSkill,
                    BoostAmounts = new List<double> { 20 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedAttack,
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterHitByActiveSkill,
                    BoostAmounts = new List<double> { 15 }
                }
            }
        };
        
        var spicedUp = new FighterSkill {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedDefence,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedDefence,
                    BoostRestrictionType = BoostRestrictionType.GatheringResources,
                    BoostAmounts = new List<double> { 5 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.GatheringResources,
                    BoostAmounts = new List<double> { 10 },
                    Chance = 10
                }
            }
        };
        
        var criticalCargo = new TalentSkill
        {
            Name = "Critical Cargo",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.ThreeSecondsAfterSuccessfulChance,
                    BoostAmounts = new List<double> { 15 },
                    Chance = 10
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.GatheringResources,
                    BoostAmounts = new List<double> { 10 }
                }
            },
            TalentTree = Leader.GetTree()                                                               
        };

        

        var foodAddiction = new TalentSkill
        {
            Name = "Food Addiction",
            Boosts = new List<Boost>
            {
            },
            TalentTree = Gatherer.GetTree()
        };

        
                  
        var peacefulCourage = new TalentSkill
        {
            Name = "Secret Recipe",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.RestoreRageAfterActiveSkill,
                    BoostRestrictionType = BoostRestrictionType.AttackingGatherers,
                    BoostAmounts = new List<double> { 60 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    Chance = 10,
                    BoostAmounts = new List<double> { 30 }
                }
            },
            TalentTree = Support.GetTree()
        };
        
        

        var remy = new Fighter
        {
            Name = "Remy",
            CanTalentLeap = false,
            FighterSkills = new List<FighterSkill>
            {
                spicedUp, devotedProtector
            },
            TalentSkills = new List<TalentSkill>
            {
                criticalCargo, foodAddiction, peacefulCourage
            }
        };

        return remy;
    }
}