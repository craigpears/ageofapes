using System.Diagnostics;
using System.Text;
using System.Xml.Schema;
using BlazorApp1.Shared.FighterSimulator;
using BlazorApp1.Shared.FighterSimulator.Extensions;
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
        var fightSimulationService = new FightSimulationService();
        
        var statsService = new FighterStatsService();
        
        var defendingArmyFunc = (FighterConfiguration configuration) => new Army
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

        Debug.WriteLine($"Simulating cannon city attacks");
        var cannonCityAttackResults = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                Seige = true,
                UseCannons = true,
                UseShooterUnitSkill = false,
                MapBattle = false
            }, 
            statsService,
            (FighterConfiguration configuration) => new Army
            {
                ArmyBoosts = configuration.ArmyBoosts, 
                FighterConfiguration = configuration,
                Troops = new List<Troop>
                {
                    new() { Count = (int)(100000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.WallBreaker, GearLevel = 5, TroopLevel = 5 }
                }
            },
            defendingArmyFunc);

        Debug.WriteLine($"Simulating shooter unit skill attacks");
        var shooterUnitSkillAttackResults = fightSimulationService.SimulateFight(
            new FightSimulationOptions()
            {
                UseShooterUnitSkill = true,
                MapBattle = true
            }, 
            statsService,
            (FighterConfiguration configuration) => new Army
            {
                ArmyBoosts = configuration.ArmyBoosts, 
                FighterConfiguration = configuration,
                Troops = new List<Troop>
                {
                    new() { Count = (int)(100000 * configuration.ArmyBoosts.MaxTroopsMultiplier), TroopType = TroopType.Shooter, GearLevel = 5, TroopLevel = 5 }
                }
            },
            defendingArmyFunc);
        
        // TODO: Improve this so there is one output with only the best configurations in each category and then breakdowns for each fighter
        
        Debug.WriteLine($"Writing results");
        var outputBaseFolder = @"\\nas-pears\documents\AgeOfApes\FighterOutputs";
        var cannonResults = ResultsToCsv(cannonCityAttackResults, $"{outputBaseFolder}//CannonResults");
        var shooterUnitSkillResults = ResultsToCsv(shooterUnitSkillAttackResults, $"{outputBaseFolder}//ShooterUnitSkillResults");
        
        // TODO: For 1v1s show the results in a matrix
        // TODO: Output summaries of each talent trees
        
    }

    private static string ResultsToCsv(List<AttackResult> results, string outputFolder)
    {
        var bestResults = results
            .GroupBy(x => new
            {
                x.AttackingArmy.FighterConfiguration.Fighter.Name
            }) // Only take the best of each breakdown, there are usually lots of variants with stats that don't matter
            .Select(x => x.ToList().GetBestResult())
            .ToList();

        var resultsCsv = ResultsToCsv(bestResults);

        File.WriteAllText($"{outputFolder}//bestResults.txt", resultsCsv);
        foreach (var bestResult in bestResults)
        {
            var fighterConfig = bestResult.AttackingArmy.FighterConfiguration;
            var fighterSelectedTalents = fighterConfig.SelectedTalents;

            var talentsJson = JsonConvert.SerializeObject(fighterSelectedTalents);
            File.WriteAllText($"{outputFolder}//{fighterConfig.Fighter.Name}.bestConfig.txt", talentsJson);
        }

        var resultsByFighter = results.GroupBy(x => x.AttackingArmy.FighterConfiguration.Fighter.Name);
        foreach (var fighterResults in resultsByFighter)
        {
            var bestFighterResults = fighterResults
                .GroupBy(x => new
                {
                    x.AttackingArmy.FighterConfiguration.TalentBreakdown
                }) // Only take the best of each breakdown, there are usually lots of variants with stats that don't matter
                .Select(x => x.ToList().GetBestResult())
                .ToList();
            
            var fighterResultsCsv = ResultsToCsv(bestFighterResults);
            File.WriteAllText($"{outputFolder}//{fighterResults.Key}.results.txt", fighterResultsCsv);
        }

        return resultsCsv;
    }

    private static string ResultsToCsv(List<AttackResult> bestResults)
    {
        var headers =
            "Fighter\tRoundsTaken\tMaxDamage\tMaxSkillDamage\tFirstDefenderTroopLoss\tTalentBreakdown\tCannons\tSeige\tGathering\tMap Battle\tUse Shooter Unit Skill\t";
        
        // TODO: Break down by type like research, equipment, talent skill etc.
        var boostTypes = bestResults
            .SelectMany(x => x.AttackingArmy.FighterConfiguration.SelectedTalents)
            .SelectMany(x => x.Boosts)
            .OrderBy(x => x.Source)
            .Select(x => $"{x.Source}-{x.BoostType}")
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

            sb.Append($"{result.FightOptions.UseCannons}\t");
            sb.Append($"{result.FightOptions.Seige}\t");
            sb.Append($"{result.FightOptions.Gathering}\t");
            sb.Append($"{result.FightOptions.MapBattle}\t");
            sb.Append($"{result.FightOptions.UseShooterUnitSkill}\t");

            var resultBoosts = result
                .AttackingArmy
                .ArmyBoosts
                .ApplicableBoosts
                .GroupBy(x => $"{x.Source}-{x.BoostType}");

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
        return resultsCsv;
    }
}