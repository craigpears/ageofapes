using System.Diagnostics;
using System.Text;
using System.Xml.Schema;
using BlazorApp1.Shared.FighterSimulator;
using Newtonsoft.Json;
using ScoutingParser;

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
        /*
         *
         * Cayicep stats are +350% for his hitters, +300% for defence of hitters.  Other stats are quite small, like +45% counter to pilots and +17.5% normal damage
         */
        var repo = new FightersRepository();
        var statsService = new FighterStatsService();
        var fightSimulationService = new FightSimulationService();
        var fightOptions = new FightSimulationOptions()
        {
            Seige = true,
            UseCannons = true
        };

        var fighters = repo.GetFighters();
        var results = new List<AttackResult>();
        foreach (var fighter in fighters)
        {
            var configurations = statsService.GetConfigurationsForFighter(fighter, fightOptions);
            var i = 0;
            foreach (var configuration in configurations)
            {
                if (i++ % 10000 == 0)
                {
                    Debug.WriteLine($"Simulating attack {i}/{configurations.Count()}");
                }
                
                var army = new Army
                {
                    ArmyBoosts = configuration.ArmyBoosts, 
                    FighterConfiguration = configuration,
                    Troops = new List<Troop>
                    {
                        new() { Count = (int)(100000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.WallBreaker, GearLevel = 5, TroopLevel = 5 }
                    }
                };
                
                
                var defendingArmy = new Army
                {
                    ArmyBoosts = new ArmyBoosts
                    {
                         UnitBoosts = new List<UnitBoosts>
                         {
                             new UnitBoosts { AttackBoostPercent = 1040, DefenceBoostPercent = 640, TroopType = TroopType.Pilot },
                             new UnitBoosts { AttackBoostPercent = 1040, DefenceBoostPercent = 640, TroopType = TroopType.Hitter },
                             new UnitBoosts { AttackBoostPercent = 1040, DefenceBoostPercent = 640, TroopType = TroopType.Shooter }
                         }
                    },
                    Troops = new List<Troop>
                    {
                        new() { TroopType = TroopType.Hitter, Count = 250000, GearLevel = 5, TroopLevel = 5 },
                        new() { TroopType = TroopType.Pilot, Count = 250000, GearLevel = 5, TroopLevel = 5 },
                        new() { TroopType = TroopType.Shooter, Count = 250000, GearLevel = 5, TroopLevel = 5 },
                    }
                };
                
                var attackResult = fightSimulationService.SimulateCityAttack(army, defendingArmy, fightOptions);
                results.Add(attackResult);
            }
            
        }

        var bestResults = results
            .OrderByDescending(x => x.AttackLogs.First().AttackerDamage)
            .GroupBy(x => x.AttackingArmy.FighterConfiguration.TalentBreakdown)// Only take the best of each breakdown, there are usually lots of variants with stats that don't matter
            .Select(x => x.ToList().First())
            .ToList();

        // TODO: Improve this so that army boosts are separated out into columns
        var headers = "Fighter\tRoundsTaken\tMaxDamage\tMaxSkillDamage\tFirstDefenderTroopLoss\tTalentBreakdown\t";
        var boostTypes = bestResults
            .SelectMany(x => x.AttackingArmy.FighterConfiguration.SelectedTalents)
            .SelectMany(x => x.Boosts)
            .Select(x => x.BoostType)
            .Distinct();

        foreach (var boostType in boostTypes)
        {
            headers += $"{boostType.ToString()}\t";
        }
        
        var sb = new StringBuilder();

        sb.AppendLine(headers);
        
        foreach (var result in bestResults)
        {
            sb.Append($"{result.AttackingArmy.FighterConfiguration.Fighter.Name}\t");
            sb.Append($"{result.AttackLogs.Count}\t");
            sb.Append($"{result.AttackLogs.Max(a => a.AttackerDamage)}\t");
            sb.Append($"{result.AttackLogs.Max(a => a.AttackerSkillDamage)}\t");
            sb.Append($"{result.AttackLogs.First().DefenderLostTroops}\t");
            sb.Append($"{result.AttackingArmy.FighterConfiguration.TalentBreakdown}\t");
            
            var resultBoosts = result
                .AttackingArmy
                .FighterConfiguration
                .SelectedTalents
                .SelectMany(x => x.Boosts)
                .GroupBy(x => x.BoostType);
            
            foreach (var boostType in boostTypes)
            {
                var matchingBoost = resultBoosts.SingleOrDefault(x => x.Key == boostType);
                if (matchingBoost != null)
                {
                    sb.Append($"{matchingBoost.ToList().Sum(b => b.BoostAmounts.Max())}");
                }

                sb.Append("\t");
            }

            sb.Append("\r\n");
        }

        var resultsCsv = sb.ToString();

    }
}