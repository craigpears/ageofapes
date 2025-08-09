using System.Diagnostics;
using System.Xml.Schema;
using BlazorApp1.Shared.FighterSimulator;
using BlazorApp1.Shared.FighterSimulator.TalentTrees;
using Newtonsoft.Json;
using ScoutingParser;
using Syncfusion.Pdf;

namespace Tests.FightSimulationServiceTests;

public class ProcessSkillsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldNotProcessSkillsWhenUsingShooterSkill()
    {
        var sut = new FightSimulationService(); 
        var options = new FightSimulationOptions { UseShooterUnitSkill = true };
        var attackingArmy = GetDefaultAttackingArmy();
        var defendingArmy = new Army();

        var skillsResult = sut.ProcessSkills(attackingArmy, defendingArmy, options);
        
        Assert.That(skillsResult.TotalDamage, Is.EqualTo(0));
        Assert.That(skillsResult.TroopsKilled, Is.EqualTo(0));
    }
    
    [Test]
    public void ShouldNotProcessActiveSkillsWhenUsingCannon()
    {
        var sut = new FightSimulationService(); 
        var options = new FightSimulationOptions { UseCannons = true };
        var attackingArmy = GetDefaultAttackingArmy();
        var defendingArmy = new Army();

        var skillsResult = sut.ProcessSkills(attackingArmy, defendingArmy, options);
        
        Assert.That(skillsResult.TotalDamage, Is.EqualTo(0));
        Assert.That(skillsResult.TroopsKilled, Is.EqualTo(0));
    }
    
    [Test]
    public void ShouldProcessActiveSkillsWhenUsingCannonIfCannonDamageFactor()
    {
        var sut = new FightSimulationService(); 
        var options = new FightSimulationOptions { UseCannons = true };
        var attackingArmy = GetDefaultAttackingArmy();
        attackingArmy.FighterConfiguration.Fighter.ActiveSkill.CannonDamageFactor = 1000;
        var defendingArmy = GetDefaultDefendingArmy();

        var skillsResult = sut.ProcessSkills(attackingArmy, defendingArmy, options);
        
        // TODO: This is just here for regression testing, need real examples
        Assert.That(skillsResult.TotalDamage, Is.EqualTo(100000));  
        Assert.That(skillsResult.TroopsKilled, Is.EqualTo(1000));
    }
    
    [Test]
    public void ShouldDoHalfDamageIfEnemyDefenceDoubles()
    {
        var sut = new FightSimulationService(); 
        var options = new FightSimulationOptions();
        var attackingArmy = GetDefaultAttackingArmy();
        var defendingArmy = GetDefaultDefendingArmy();
        defendingArmy.Troops[0].CalculatedDefence *= 2;

        var skillsResult = sut.ProcessSkills(attackingArmy, defendingArmy, options);
            
        Assert.That(skillsResult.TotalDamage, Is.EqualTo(50000));  
        Assert.That(skillsResult.TroopsKilled, Is.EqualTo(500));
    }
    
    [Test]
    public void ShouldDoDoubleDamageWithHundredPercentSkillDamageBoost()
    {
        var sut = new FightSimulationService(); 
        var options = new FightSimulationOptions();
        var attackingArmy = GetDefaultAttackingArmy();
        attackingArmy.ArmyBoosts.DamageDealtBySkillsPercentIncrease = 100;
        var defendingArmy = GetDefaultDefendingArmy();

        var skillsResult = sut.ProcessSkills(attackingArmy, defendingArmy, options);
            
        Assert.That(skillsResult.TotalDamage, Is.EqualTo(200000));  
        Assert.That(skillsResult.TroopsKilled, Is.EqualTo(2000));
    }
    
    [Test]
    public void ShouldDoDoubleDamageWithDoubledDamageFactor()
    {
        var sut = new FightSimulationService(); 
        var options = new FightSimulationOptions();
        var attackingArmy = GetDefaultAttackingArmy();
        attackingArmy.FighterConfiguration.Fighter.ActiveSkill.DamageFactor *= 2;
        var defendingArmy = GetDefaultDefendingArmy();

        var skillsResult = sut.ProcessSkills(attackingArmy, defendingArmy, options);
            
        Assert.That(skillsResult.TotalDamage, Is.EqualTo(200000));  
        Assert.That(skillsResult.TroopsKilled, Is.EqualTo(2000));
    }
    
    [Test]
    public void ShouldDoPassiveSkillDamage()
    {
        var sut = new FightSimulationService();
        sut.rand = new Random(1);
        var options = new FightSimulationOptions();
        var attackingArmy = GetDefaultAttackingArmy(true);
        attackingArmy.RageLevel = 0;
        var defendingArmy = GetDefaultDefendingArmy();

        var skillsResult = sut.ProcessSkills(attackingArmy, defendingArmy, options);
            
        Assert.That(skillsResult.TotalDamage, Is.EqualTo(100000));  
        Assert.That(skillsResult.TroopsKilled, Is.EqualTo(1000));
    }
    
    [Test]
    public void ShouldUseChanceValueWhenDoingPassiveSkillDamage()
    {
        var sut = new FightSimulationService();
        sut.rand = new Random(1);
        var options = new FightSimulationOptions();
        var attackingArmy = GetDefaultAttackingArmy(true);
        attackingArmy.RageLevel = 0;
        attackingArmy.ArmyBoosts.PassiveSkills[0].Chance = 20;
        var defendingArmy = GetDefaultDefendingArmy();

        var passiveSkillOccurences = RunProcessSkillsTenTimes(sut, attackingArmy, defendingArmy, options);
        Assert.That(passiveSkillOccurences, Is.EqualTo(2));  
        
        attackingArmy.ArmyBoosts.PassiveSkills[0].Chance = 40;
        passiveSkillOccurences = RunProcessSkillsTenTimes(sut, attackingArmy, defendingArmy, options);
        Assert.That(passiveSkillOccurences, Is.EqualTo(4));  
    }

    private int RunProcessSkillsTenTimes(FightSimulationService sut, Army attackingArmy, Army defendingArmy,
        FightSimulationOptions options)
    {
        var skillResults = new List<ProcessSkillsResult>();
        for (int i = 0; i < 10; i++)
        {
            skillResults.Add(sut.ProcessSkills(attackingArmy, defendingArmy, options));
        }

        return skillResults.Count(x => x.TotalDamage > 0);
    }

    private static Army GetDefaultAttackingArmy(bool includePassiveSkill = false)
    {
        var army = new Army
        {
            Troops = new List<Troop>
            {
                new Troop
                {
                    TroopType = TroopType.Hitter,
                    Count = 10000,
                    CalculatedAttack = 100,
                    CalculatedDefence = 100,
                    CalculatedHealth = 100
                }
            },
            FighterConfiguration = new FighterConfiguration
            {
                Fighter = new Fighter
                {
                    FighterSkills = new List<FighterSkill>
                    {
                        new()
                        {
                            FighterSkillType = FigherSkillType.Active,
                            DamageFactor = 1000,
                            RageRequired = 1000
                        }
                    }
                },
            },
            RageLevel = 1000
        };

        if (includePassiveSkill)
        {
            army.ArmyBoosts.PassiveSkills = new List<Boost>
            {
                new Boost
                {
                    BoostAmounts = new List<double> { 1000 },
                    Chance = 100
                }
            };
        }

        return army;
    }
    
    private static Army GetDefaultDefendingArmy()
    {
        return new Army
        {
            Troops = new List<Troop>
            {
                new Troop
                {
                    TroopType = TroopType.Hitter,
                    Count = 10000,
                    CalculatedAttack = 100,
                    CalculatedDefence = 100,
                    CalculatedHealth = 100
                }
            }
        };
    }
}