using System.Diagnostics;
using System.Xml.Schema;
using BlazorApp1.Shared.FighterSimulator;
using BlazorApp1.Shared.FighterSimulator.TalentTrees;
using Newtonsoft.Json;
using ScoutingParser;

namespace Tests;

public class FighterStatsServiceTests : TalentClass
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetTreeCombinations_ShouldCombineBothSidesOfATalentTree()
    {
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfStepUnits);
        talentTree.WithNextTalent(BoostType.IncreasedMaxTroops, HalfStepUnits);
        talentTree.WithNextTalent(BoostType.IncreasedDamage, HalfStepUnits);

        var sut = new FighterStatsService();
        var combinations = sut.GetTreeCombinations(talentTree);
        
        Assert.AreEqual(4, combinations.Count());
        
        // One should be just the root talent on its own
        Assert.True(combinations.Any(x => x.Count == 1 && x.Contains(talentTree)));

        // One should have all three
        Assert.AreEqual(1, combinations.Count(x => x.Count == 3));
        
        // The remaining two should have the root and one of the leaf nodes
        Assert.AreEqual(2, combinations.Count(x => x.Count == 2));

    }

    [Test]
    public void GetTreeCombinations_ShouldTraverseRequiredTalents()
    {
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfStepUnits);
            
        // Left half of tree
        talentTree.WithNextTalent(BoostType.IncreasedMaxTroops, HalfStepUnits)
            .WithNextTalent(BoostType.EnemySkillDamageReduced, SingleStepUnits);
            
        // Right half of tree    
        talentTree.WithNextTalent(BoostType.IncreasedDamage, HalfStepUnits);

        var sut = new FighterStatsService();
        var combinations = sut.GetTreeCombinations(talentTree);
        
        Assert.AreEqual(6, combinations.Count());
        
        Assert.True(combinations.Any(x => x.Count == 1 && x.Contains(talentTree)));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 4));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 3));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 2));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 1));
    }

    [Test]
    public void GetTreeCombinations_ShouldTraverseMultipleOptionalTalents()
    {
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfStepUnits);
            
        // Left half of tree
        talentTree.WithNextTalent(BoostType.IncreasedMaxTroops, HalfStepUnits)
            .WithNextTalent(BoostType.EnemySkillDamageReduced, SingleStepUnits)
            .WithOptionalTalent(BoostType.IncreasedHealing, SingleStepUnits)
            .WithOptionalTalent(BoostType.IncreasedDamageToCounteredUnit, SingleStepUnits);
            
        // Right half of tree    
        talentTree.WithNextTalent(BoostType.IncreasedDamage, HalfStepUnits);

        var sut = new FighterStatsService();
        var combinations = sut.GetTreeCombinations(talentTree);
        
        Assert.AreEqual(10, combinations.Count());
        
        Assert.True(combinations.Any(x => x.Count == 1 && x.Contains(talentTree)));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 4 && x.All(x => !x.Optional)));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 3 && x.All(x => !x.Optional)));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 2 && x.All(x => !x.Optional)));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 1 && x.All(x => !x.Optional)));
        
        Assert.AreEqual(1, combinations.Count(x => x.Count == 4 && x.Count(x => x.Optional) == 1));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 5 && x.Count(x => x.Optional) == 1));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 5 && x.Count(x => x.Optional) == 2));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 6 && x.Count(x => x.Optional) == 2));

    }
    
    [Test]
    public void GetTreeCombinations_ShouldCombineBothSidesOfALargerTalentTree()
    {
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfStepUnits);
        
        talentTree
            .WithNextTalent(BoostType.IncreasedMaxTroops, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, ThreePercentSteps);
        
        talentTree
            .WithNextTalent(BoostType.IncreasedDamage, HalfStepUnits)
            .WithNextTalent(BoostType.IncreasedMarchingSpeed, ThreePercentSteps);

        var sut = new FighterStatsService();
        var combinations = sut.GetTreeCombinations(talentTree);
        
        
        Assert.AreEqual(1, combinations.Count(x => x.Count == 5 && x.All(x => !x.Optional)));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 4 && x.All(x => !x.Optional)));
        Assert.AreEqual(3, combinations.Count(x => x.Count == 3 && x.All(x => !x.Optional)));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 2 && x.All(x => !x.Optional)));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 1 && x.All(x => !x.Optional)));

        Assert.AreEqual(9, combinations.Count());
    }

}