using System.Diagnostics;
using System.Xml.Schema;
using FightSimulator.Core;
using FightSimulator.Core.Models;
using FightSimulator.Core.Services;
using Newtonsoft.Json;

namespace Tests.FightSimulationServiceTests;

public class RefreshArmyTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldCallArmyFunctionIfNoCurrentArmy()
    {
        var sut = new FightSimulationService(); var options = new FightSimulationOptions();
        
        var freshArmy = new Army();
        Army ArmyFunc(Army army, Army army2) => freshArmy;

        var refreshedArmy = sut.RefreshArmy(ArmyFunc, null, null, options);
        
        Assert.AreEqual(freshArmy, refreshedArmy);
    }       
    
    [Test]
    public void ShouldCallArmyFunctionIfRecalculateArmiesOptionTrue()
    {
        var sut = new FightSimulationService();
        var options = new FightSimulationOptions
        {
            RecalculateArmies = true
        };
        
        var currentArmy = new Army();
        var freshArmy = new Army();
        Army ArmyFunc(Army army, Army army2) => freshArmy;

        var refreshedArmy = sut.RefreshArmy(ArmyFunc, currentArmy, null, options);
        
        Assert.AreEqual(freshArmy, refreshedArmy);
    }
    
    [Test]
    public void ShouldNotCallArmyFunctionIfExistingArmy()
    {
        var sut = new FightSimulationService();
        var options = new FightSimulationOptions();
        
        var currentArmy = new Army();
        var freshArmy = new Army();
        Army ArmyFunc(Army army, Army army2) => freshArmy;

        var refreshedArmy = sut.RefreshArmy(ArmyFunc, currentArmy, null, options);
        
        Assert.AreEqual(currentArmy, refreshedArmy);
    }
    
    [Test]
    public void ShouldAddGearBoostsForNewArmies()
    {
        var sut = new FightSimulationService();
        var options = new FightSimulationOptions();
        
        var freshArmy = new Army
        {
            Troops = new List<Troop>
            {
                new Troop
                {
                    TroopType = TroopType.Hitter,
                    Count = 500,
                    GearLevel = 5,
                    TroopLevel = 5
                }
            }
        };
        
        Army ArmyFunc(Army army, Army army2) => freshArmy;

        var refreshedArmy = sut.RefreshArmy(ArmyFunc, null, null, options);
        var troop = refreshedArmy.Troops[0];
        
        Assert.That(troop.CalculatedAttack, Is.EqualTo(292));
        Assert.That(troop.CalculatedDefence, Is.EqualTo(308));
        Assert.That(troop.CalculatedHealth, Is.EqualTo(183));
    }
    
    [Test]
    public void ShouldAddUnitSpecificBoosts()
    {
        var sut = new FightSimulationService();
        var options = new FightSimulationOptions();
        
        var freshArmy = new Army
        {
            Troops = new List<Troop>
            {
                new Troop
                {
                    TroopType = TroopType.Hitter,
                    Count = 500,
                    GearLevel = 5,
                    TroopLevel = 5
                }
            },
            ArmyBoosts = new ArmyBoosts
            {
                UnitBoosts = new List<UnitBoosts>
                {
                    new UnitBoosts
                    {
                        AttackBoostPercent = 10,
                        TroopType = TroopType.Hitter
                    }
                }
            }
        };
        
        Army ArmyFunc(Army army, Army army2) => freshArmy;

        var refreshedArmy = sut.RefreshArmy(ArmyFunc, null, null, options);
        var troop = refreshedArmy.Troops[0];
        
        Assert.That(troop.CalculatedAttack, Is.EqualTo(313));
        Assert.That(troop.CalculatedDefence, Is.EqualTo(308));
        Assert.That(troop.CalculatedHealth, Is.EqualTo(183));
    }

}