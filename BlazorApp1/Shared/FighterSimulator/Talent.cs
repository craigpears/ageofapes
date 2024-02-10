using System.Text.Json.Serialization;

namespace BlazorApp1.Shared.FighterSimulator;

public class Talent
{
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
    
    public List<Boost> Boosts { get; set; }
    public List<Talent> NextTalents { get; set; } = new List<Talent>();
    public bool Optional { get; set; } = false;
    
    public int TalentDepth { get; set; }
    
    [JsonIgnore]
    public Talent RootTalent { get; set; }
    
    public Talent WithOptionalTalent(BoostType boostType, List<Double> boostAmounts)
    {
        WithOptionalTalent(boostType, boostAmounts, null, null);
        return this;
    }
    
    public Talent WithOptionalTalent(BoostType boostType, List<Double> boostAmounts, TroopType? troopRestriction = null, BoostRestrictionType? boostRestrictionType = null)
    {
        var optionalTalent = WithNextTalent(boostType, boostAmounts, troopRestriction, boostRestrictionType);
        optionalTalent.Optional = true;
        return this;
    }
    
    public Talent WithNextTalent(BoostType boostType, List<Double> boostAmounts)
    {
        var nextTalent = new Talent(boostType, boostAmounts);
        // Set a few properties for debugging help
        nextTalent.TalentDepth = TalentDepth + 1; 
        nextTalent.RootTalent = RootTalent;
        NextTalents.Add(nextTalent);
        return nextTalent;
    }
    
    public Talent WithNextTalent(BoostType boostType, List<Double> boostAmounts, TroopType? troopRestriction = null, BoostRestrictionType? boostRestrictionType = null)
    {
        var nextTalent = new Talent(boostType, boostAmounts, troopRestriction, boostRestrictionType);
        // Set a few properties for debugging help
        nextTalent.TalentDepth = TalentDepth + 1;
        nextTalent.RootTalent = RootTalent;
        NextTalents.Add(nextTalent);
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