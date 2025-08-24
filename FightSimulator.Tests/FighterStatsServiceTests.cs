using System.Diagnostics;
using System.Xml.Schema;
using FightSimulator.Core;
using FightSimulator.Core.Models;
using FightSimulator.Core.Services;
using FightSimulator.Core.TalentTrees;
using FightSimulator.Core.Repositories;
using Newtonsoft.Json;

namespace Tests;

public class FighterStatsServiceTests : TalentClass
{
    private ITalentCombinationsRepository _mockRepository;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new MockTalentCombinationsRepository();
    }

    [Test]
    public void GetTreeCombinations_ShouldCombineBothSidesOfATalentTree()
    {
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfPercentSteps);
        talentTree.NextTalent(BoostType.IncreasedMaxTroops, HalfPercentSteps);
        talentTree.NextTalent(BoostType.IncreasedDamage, HalfPercentSteps);

        var sut = new FighterStatsService(_mockRepository);
        var combinations = sut.GetTreeCombinations(talentTree);
        
        Assert.That(combinations.Count(), Is.EqualTo(4));
        
        // One should be just the root talent on its own
        Assert.True(combinations.Any(x => x.Count == 1 && x.Contains(talentTree)));

        // One should have all three
        Assert.That(combinations.Count(x => x.Count == 3), Is.EqualTo(1));
        
        // The remaining two should have the root and one of the leaf nodes
        Assert.That(combinations.Count(x => x.Count == 2), Is.EqualTo(2));

    }

    [Test]
    public void GetTreeCombinations_ShouldTraverseRequiredTalents()
    {
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfPercentSteps);
            
        // Left half of tree
        talentTree.NextTalent(BoostType.IncreasedMaxTroops, HalfPercentSteps)
            .NextTalent(BoostType.EnemySkillDamageReduced, SinglePercentSteps);
            
        // Right half of tree    
        talentTree.NextTalent(BoostType.IncreasedDamage, HalfPercentSteps);

        var sut = new FighterStatsService(_mockRepository);
        var combinations = sut.GetTreeCombinations(talentTree);
        
        Assert.AreEqual(6, combinations.Count());
        
        Assert.True(combinations.Any(x => x.Count == 1 && x.Contains(talentTree)));
        Assert.That(combinations.Count(x => x.Count == 4), Is.EqualTo(1));
        Assert.That(combinations.Count(x => x.Count == 3), Is.EqualTo(2));
        Assert.That(combinations.Count(x => x.Count == 2), Is.EqualTo(2));
        Assert.That(combinations.Count(x => x.Count == 1), Is.EqualTo(1));
    }

    [Test]
    public void GetTreeCombinations_ShouldTraverseMultipleOptionalTalents()
    {
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfPercentSteps);
            
        // Left half of tree
        talentTree.NextTalent(BoostType.IncreasedMaxTroops, HalfPercentSteps)
            .NextTalent(BoostType.EnemySkillDamageReduced, SinglePercentSteps)
            .OptionalTalent(BoostType.IncreasedHealing, SinglePercentSteps)
            .OptionalTalent(BoostType.IncreasedDamageToCounteredUnit, SinglePercentSteps);
            
        // Right half of tree    
        talentTree.NextTalent(BoostType.IncreasedDamage, HalfPercentSteps);

        var sut = new FighterStatsService(_mockRepository);
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
        var talentTree = new Talent(BoostType.IncreasedAttack, HalfPercentSteps);
        
        talentTree
            .NextTalent(BoostType.IncreasedMaxTroops, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, ThreePercentSteps);
        
        talentTree
            .NextTalent(BoostType.IncreasedDamage, HalfPercentSteps)
            .NextTalent(BoostType.IncreasedMarchingSpeed, ThreePercentSteps);

        var sut = new FighterStatsService(_mockRepository);
        var combinations = sut.GetTreeCombinations(talentTree);
        
        
        Assert.AreEqual(1, combinations.Count(x => x.Count == 5 && x.All(x => !x.Optional)));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 4 && x.All(x => !x.Optional)));
        Assert.AreEqual(3, combinations.Count(x => x.Count == 3 && x.All(x => !x.Optional)));
        Assert.AreEqual(2, combinations.Count(x => x.Count == 2 && x.All(x => !x.Optional)));
        Assert.AreEqual(1, combinations.Count(x => x.Count == 1 && x.All(x => !x.Optional)));

        Assert.AreEqual(9, combinations.Count());
    }

    private class MockTalentCombinationsRepository : ITalentCombinationsRepository
    {
        public List<List<Talent>> GetCachedCombinations(string cacheKey) => null;
        public void SaveCombinations(string cacheKey, List<List<Talent>> combinations) { }
        public List<List<Talent>> GetCachedTreeCombinations(string talentTreeName) => null;
        public void SaveTreeCombinations(string talentTreeName, List<List<Talent>> combinations) { }
        public bool HasCachedCombinations(string cacheKey) => false;
        public bool HasCachedTreeCombinations(string talentTreeName) => false;
    }
}