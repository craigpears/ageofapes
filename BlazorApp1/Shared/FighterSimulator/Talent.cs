using System.Text.Json.Serialization;

namespace BlazorApp1.Shared.FighterSimulator;

public class Talent
{
    public static int NextTalentId = 0;
    public Talent()
    {
        
    }
    
    public Talent(BoostType boostType, List<Double> boostAmounts)
    {
        Boosts = new List<Boost> { new() { BoostType = boostType, BoostAmounts = boostAmounts }};
        RootTalent = this;
    }
    
    public Talent(BoostType boostType, List<Double> boostAmounts, TroopType? troopRestriction, BoostRestrictionType? boostRestrictionType)
    {
        Boosts = new List<Boost>
        {
            new()
            {
                BoostType = boostType, BoostAmounts = boostAmounts, TroopRestriction = troopRestriction,
                BoostRestrictionType = boostRestrictionType
            }
        };
        RootTalent = this;
    }

    public List<Boost> Boosts { get; set; } = new List<Boost>();
    public List<Talent> NextTalents { get; set; } = new List<Talent>();
    public bool Optional { get; set; } = false;

    public int TalentDepth { get; set; } = 0;
    
    public Talent RootTalent { get; set; }
    
    public string TalentTreeName { get; set; }
    public int TalentId = NextTalentId++;

    public Talent LastRequiredTalent { get; set; }
    public int TalentPointCost => Boosts.Any() ? Boosts.FirstOrDefault().BoostAmounts.Count() : 0;
    
    public Talent WithOptionalTalent(BoostType boostType, List<Double> boostAmounts, TroopType? troopRestriction = null, BoostRestrictionType? boostRestrictionType = null)
    {
        var optionalTalent = WithNextTalent(boostType, boostAmounts, troopRestriction, boostRestrictionType);
        optionalTalent.Optional = true;
        // Set the parent talent so when traversing it can easily get back to the main path
        optionalTalent.LastRequiredTalent = LastRequiredTalent;
        // Move the talent so that it links to this one
        LastRequiredTalent.NextTalents.Remove(optionalTalent);
        NextTalents.Add(optionalTalent);
        return optionalTalent;
    }
    
    public Talent WithNextTalent(BoostType boostType, List<Double> boostAmounts, TroopType? troopRestriction = null, BoostRestrictionType? boostRestrictionType = null)
    {
        var nextTalent = new Talent(boostType, boostAmounts, troopRestriction, boostRestrictionType);
        // Set a few properties for debugging help
        nextTalent.TalentDepth = TalentDepth + 1;
        nextTalent.RootTalent = RootTalent;
        nextTalent.LastRequiredTalent = nextTalent;
        (LastRequiredTalent ?? this).NextTalents.Add(nextTalent);
        return nextTalent;
    }

    public Talent WithExtraBoost(BoostType boostType, List<double> boostAmounts, TroopType? troopRestriction = null, BoostRestrictionType? boostRestrictionType = null)
    {
        Boosts.Add(new()
        {
            BoostType = boostType, BoostAmounts = boostAmounts, TroopRestriction = troopRestriction,
            BoostRestrictionType = boostRestrictionType
        });
        return this;
    }
}