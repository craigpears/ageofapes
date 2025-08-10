using FightSimulator.Core.Scenarios;
using FightSimulator.Core.Services;

var fightSimulationService = new FightSimulationService();
var statsService = new FighterStatsService();
var lightRun = true;

var scenarios = new List<FightScenario>
{
    new MapVersusPilots(lightRun),
    new MapVersusHitters(),
    new WhaleSeige(),
    new MapVersusShooters(lightRun),
    new MapWallBreakers(),
    new CannonAttack(),     
    new ShooterUnitSkill(),
    new SimpleCityAttack(),
    new SimpleNeutralUnitsAttack(),
    new SimpleMapAttack(),
    new SimpleDefence()
}.OrderBy(x => x.GetLastRanDate());

foreach(var scenario in scenarios)
{
    // TODO: Put a task scheduling library around this
    fightSimulationService.SimulateFight(scenario, statsService);
};

// TODO: Add filters for light runs on scenarios, like only run for pilots or leaders
// TODO: Add redis for caching
// TODO: Update output to split FighterTalents into one grouping per tree
// TODO: Write the detailed logs for the best results
// TODO: Improve collating best results so it can read from files instead of needing entire scenarios to run from end to end 
// TODO: Improve outputs to show stats similar to in the game, like the unit buffs
// TODO: For 1v1s show the results in a matrix
// TODO: Simulate the differences in gear level
// TODO: Simulate how much research contributes vs gears etc relative to their coin cost
// TODO: Ideally do a budget test or show how to get the best early game setup fastest etc.
// TODO: Output summaries of each talent trees
// TODO: Show coin costs cumulatively 
// TODO: Add healing speedups consumed by defender
// TODO: Simulate attacks on gates where there are more defender number limitations and equal damage to each side but fast reinforcements for both
// TODO: Simulate fights where there are all kill rates?
// TODO: Add stress testing support - how strong does an attacker need to get in each scenario to inflict kills etc.
// TODO: Add scenarios for 3v1, 4v1, 5v1
// TODO: Add a rankings sheet where it shows how each ranked in each scenario
// TODO: Add whale seige with varied troop counts and strength on both sides, show results in a matrix
// TODO: Add kill ratio to output
// TODO: Add fighter score to output - sum of important stats
// TODO: Add a rally leader scenario where you have three unit types
// TODO: Add outputs ranking fighters in all scenarios by killing speed, kill ratio, survival time
// TODO: Add a survival scenario to see how well each fighter survives
// TODO: Do the reverse of whale seige to see who is the most efficient attacker and to test different tactics?
// TODO: Add cannon support to large seige scenarios to test how it affects kill ratio
// TODO: Add cannon siege option where you are attacked by frenemies to generate huge amounts of rage
// TODO: Add in statue buffs
// TODO: Check update to Elmo and Tiffany
// TODO: Add some attrition scenarios.  What does it take to exhaust someone like spectre with mediocre fighters and force him to use healing speedups?
// TODO: Add in some scenarios that test the effectiveness of support from fighters like buzz or vance that provide healing or shields
// TODO: Test two titans fighting and what impact support fighters can have, whether through healing, allied troop boosts, shields, intimidation, equipment boosts or other things
// TODO: Add multiple fighter support for team testing and add contribution scores for any support given to the strongest fighters
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