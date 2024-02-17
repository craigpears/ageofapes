namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public class Gatherer : TalentClass
{

    public static Talent GetTree()
    {
        var rootTalent = new Talent(BoostType.IncreasedDefence, HalfPercent);
        rootTalent.TalentTreeName = "Gatherer";

        // Left Tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedHealth, HalfPercent)
            .WithNextTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .WithOptionalTalent(BoostType.IncreasedLoad, HalfPercent)
            .WithNextTalent(BoostType.ConvertSeriouslyWoundedToLightlyWounded, TwoPercentSteps)
            .WithOptionalTalent(BoostType.IncreasedLoad, HalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, HalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedAttack, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedDefence, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedDefence, new List<Double> { 5.0, 10.0, 15.0, 20.00 }, boostRestrictionType: BoostRestrictionType.GatheringResources)
            .WithNextTalent(BoostType.IncreasedGatheringSpeed, new List<Double> { 5.0, 10.0, 15.0, 20.0, 25.0 });
        
        // Right tree
        rootTalent
            .WithNextTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, FivePercentSteps, troopRestriction: TroopType.WallBreaker)
            .WithOptionalTalent(BoostType.IncreasedMaxTroops, HalfPercent)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, TwoOneAndHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedGatheringSpeed, FivePercentSteps)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithNextTalent(BoostType.IncreasedHealth, TwoHalfStepUnits)
            .WithOptionalTalent(BoostType.IncreasedAttack, HalfPercent)
            .WithNextTalent(BoostType.IncreasedHealing,new List<Double> { 5.0, 10.0, 15.0, 20.0 }, boostRestrictionType: BoostRestrictionType.GatheringResources);

        return rootTalent;
    }
}