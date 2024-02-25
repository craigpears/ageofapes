using BlazorApp1.Shared.FighterSimulator.TalentTrees;

namespace BlazorApp1.Shared.FighterSimulator.Fighters.Shooters;

public class Mike
{
    public static Fighter GetFighter()
    {

        var activeSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Active,
            RageRequired = 1000,
            DamageFactor = 500,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.FiveSecondsAfterActiveSkillRelease,
                    BoostAmounts = new List<double> { 10 },
                    DisabledInCannonMode = true
                },
            }
        };

        var passiveSkill = new FighterSkill
        {
            FighterSkillType = FigherSkillType.Passive,
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.WallBreaker, // TODO: This should be all wall breakers only
                    BoostAmounts = new List<double> { 40 }
                },
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
                    BoostAmounts = new List<double> { 30 }
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
                    BoostType = BoostType.IncreasedAttack,
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 40 }
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
                    TroopRestriction = TroopType.WallBreaker,
                    BoostAmounts = new List<double> { 10, 15, 20, 25, 30 }
                },
            },
            TalentTree = Leader.GetTree()
        };



        var tallentSkill2 = new TalentSkill
        {
            Name = "Talent Skill 2",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.ReducedDeadUnits,
                    BoostRestrictionType = BoostRestrictionType.AttackingCitiesOnly,
                    BoostAmounts = new List<double> { 3, 4, 6, 8, 10 }
                },
            },
            TalentTree = Conqueror.GetTree()
        };

        var tallentSkill3 = new TalentSkill
        {
            Name = "Talent SKill 3",
            Boosts = new List<Boost>
            {
                new Boost
                {
                    BoostType = BoostType.IncreasedDamage,
                    BoostRestrictionType = BoostRestrictionType.SeigeMode,
                    BoostAmounts = new List<double> { 3, 4, 6, 8, 10 }
                }
            },
            TalentTree = Attacker.GetTree()
        };



        var fighter = new Fighter
        {
            Name = "Mike",
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