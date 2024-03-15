using System.Diagnostics;
using System.Text;
using System.Xml.Schema;
using BlazorApp1.Shared.FighterSimulator;
using BlazorApp1.Shared.FighterSimulator.Extensions;
using BlazorApp1.Shared.FighterSimulator.Scenarios;
using Newtonsoft.Json;
using ScoutingParser;
using Syncfusion.Pdf;

namespace Tests;

public class FighterSimulatorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldDoStuff()
    {
        
        var fightSimulationService = new FightSimulationService();
        var statsService = new FighterStatsService();
        var scenarios = new List<FightScenario>
        {
            new CannonAttack(),     
            new ShooterUnitSkill(),
            new SimpleCityAttack(),
            new SimpleNeutralUnitsAttack(),
            new SimpleMapAttack(),
            new SimpleDefence(), 
            new MapVersusPilots(),
            new MapVersusHitters(),
            new MapVersusShooters(),
            new WhaleSeige()
        };

        foreach (var scenario in scenarios)
        {
            fightSimulationService.SimulateFight(scenario, statsService);
        }
        
        
        // TODO: For 1v1s show the results in a matrix
        // TODO: Simulate the differences in gear level
        // TODO: Simulate how much research contributes vs gears etc relative to their coin cost
        // TODO: Ideally do a budget test or show how to get the best early game setup fastest etc.
        // TODO: Output summaries of each talent trees
        // TODO: Show coin costs cumulatively 
        // TODO: Add deputy fighter support
        // TODO: Add healing speedups consumed by defender
        // TODO: Simulate attacks on gates where there are more defender number limitations and equal damage to each side but fast reinforcements for both
        // TODO: Simulate fights where there are all kill rates?
        // TODO: Add stress testing support - how strong does an attacker need to get in each scenario to inflict kills etc.
        // TODO: Add scenarios for vs hitters, vs shooters, vs pilots, vs three types, 3v1, 4v1, 5v1
        // TODO: Add test where they fight the unit they counter
        // TODO: Add scenario where they fight against what counters them
        // TODO: Add a rankings sheet where it shows how each ranked in each scenario
        // TODO: Add whale seige with varied troop counts and strength on both sides, show results in a matrix
        // TODO: Add kill ratio to output
        // TODO: Add fighter score to output - sum of important stats
        // TODO: Add a rally leader scenario where you have three unit types
        // TODO: Add outputs ranking fighters in all scenarios by killing speed, kill ratio, survival time
        // TODO: Add a survival scenario to see how well each fighter survives
        // TODO: Do the reverse of whale seige to see who is the most efficient attacker and to test different tactics?
        // TODO: Add cannon support to large seige scenarios to test how it affects kill ratio
        // TODO: Add a siege option with ten dave fighters all attacking a city to see how intimidation affects them
        // TODO: Add a siege defence scenario with lots of small troops attacking to see how that affects skill damage
        // TODO: Add cannon siege option where you are attacked by frenemies to generate huge amounts of rage
        // TODO: Add in statue buffs
        // TODO: Add in tests where all conditional talents are always on like after taking skill damage
        // TODO: Add a layer to work out who the best teams are for cannons and shooters working together
        // TODO: Check update to Elmo and Tiffany
        /*
         *
         * Stress testing:
            With your army stats as +85% and +80% attack/defence with 1.5m troops being hit by a 7.5m rally with 1.2m actively swapping reinforcements.

            At +128% and+105% they are able to kill some of your troops.
            At +133% and +125% they zero you but lose 6m troops doing so
            At +218% and +180% they zero you still with 4.5m losses at a healing cost of 500M to your allies
            At +288% and +220% they still take 2m losses but your allies healing cost jumps to 2.5B 
         */
        
        // TODO: Make sure scenarios match realistic armies
        /*
         * Seen +469% attack and +336% defence on pilots for ares
         * Alkhadi has 424 and 318 for hitters with 132 health and 42 damage etc.
         */
    }

    

    
}