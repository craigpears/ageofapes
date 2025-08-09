namespace FightSimulator.Core.FighterSimulator.TalentTrees.Applications;

public class Gatherer : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedDefence, HalfPercent);
        rootTalent.TalentTreeName = "Gatherer";

        // Left Tree
        rootTalent
            .NextTalent(BoostType.IncreasedHealth, HalfPercent)
            .NextTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .OptionalTalent(BoostType.IncreasedLoad, HalfPercent)
            .NextTalent(BoostType.ConvertSeriouslyWoundedToLightlyWounded, TwoPercentSteps)
            .OptionalTalent(BoostType.IncreasedLoad, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, HalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedAttack, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedDefence, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedDefence, new List<Double> { 5.0, 10.0, 15.0, 20.00 }, boostRestrictionType: BoostRestrictionType.GatheringResources)
            .NextTalent(BoostType.IncreasedGatheringSpeed, new List<Double> { 5.0, 10.0, 15.0, 20.0, 25.0 });
        
        // Right tree
        rootTalent
            .NextTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .OptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, FivePercentSteps, troopRestriction: TroopType.WallBreaker)
            .OptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .NextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .NextTalent(BoostType.IncreasedHealth, TwoHalfPercentSteps)
            .OptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .NextTalent(BoostType.IncreasedHealing,new List<Double> { 5.0, 10.0, 15.0, 20.0 }, boostRestrictionType: BoostRestrictionType.GatheringResources);

        return rootTalent;
    }
}