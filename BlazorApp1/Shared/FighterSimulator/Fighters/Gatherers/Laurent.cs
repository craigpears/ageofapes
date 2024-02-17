using BlazorApp1.Shared.FighterSimulator.TalentTrees;

namespace BlazorApp1.Shared.FighterSimulator.Fighters;

public class Laurent
{
    
    public static Fighter GetFighter()
    {

        var devotedProtector = new FighterSkill {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            HealingFactor = 1100
        };
        
        var standingGuard = new FighterSkill {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost { 
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.GatheringResources,
                    BoostAmounts = new List<double> { 30 }
                },
                new Boost { 
                    BoostType = BoostType.IncreasedHealth,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.GatheringResources,
                    BoostAmounts = new List<double> { 30 }
                }
            }
        };
        
        var engineTuning = new TalentSkill
        {
            Name = "Engine Tuning",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 50 }
                },
                new Boost
                {
                    BoostType = BoostType.IncreasedDefence,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostRestrictionType = BoostRestrictionType.TwoSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 30 }
                }
            },
            TalentTree = Leader.GetTree()                                                               
        };

        

        var carefulPlanning = new TalentSkill
        {
            Name = "Careful Planning",
            Boosts = new List<Boost>
            {
            },
            TalentTree = Gatherer.GetTree()
        };

        
                  
        var peacefulCourage = new TalentSkill
        {
            Name = "Peaceful Courage",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.RestoreRageAfterActiveSkill,
                    Chance = 10,
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
        
        

        var derrick = new Fighter
        {
            Name = "Derrick",
            CanTalentLeap = true,
            FighterSkills = new List<FighterSkill>
            {
                standingGuard, devotedProtector
            },
            TalentSkills = new List<TalentSkill>
            {
                engineTuning, carefulPlanning, peacefulCourage
            }
        };

        return derrick;
    }
}