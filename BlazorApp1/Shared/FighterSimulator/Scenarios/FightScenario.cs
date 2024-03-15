using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using BlazorApp1.Shared.FighterSimulator.Extensions;
using Newtonsoft.Json;

namespace BlazorApp1.Shared.FighterSimulator.Scenarios;

public abstract class FightScenario
{

    
    private string outputBaseFolder = @"\\nas-pears\documents\AgeOfApes\FighterOutputs";
    public string outputFolder;
    protected double EnemyBoostMultiplier = 1.0;
    protected string headers = "Fighter\tDeputy\tRoundsTaken\tMaxDamage\tMaxSkillDamage\tEnemyLosses\tYourLosses\tTalentBreakdown\tCannons\tSeige\tGathering\tMap Battle\tUse Shooter Unit Skill\t";
    protected StringBuilder bestResultsStringBuilder = new StringBuilder();
        
    private string outputPath => $"{outputBaseFolder}//{outputFolder}";
    public FightSimulationOptions FightSimulationOptions { get; set; }

    public FightScenario(string outputFolder, FightSimulationOptions options)
    {
        this.outputFolder = outputFolder;
        this.FightSimulationOptions = options;
    }
    
    protected new List<UnitBoosts> DefaultEnemyBoosts => GetBoosts(EnemyBoostMultiplier);
    
    protected new List<UnitBoosts> GetBoosts(double multiplier) => new List<UnitBoosts>
    {
        new UnitBoosts 
            { AttackBoostPercent = 100 * multiplier, DefenceBoostPercent = 80 * multiplier, TroopType = TroopType.Pilot },
        new UnitBoosts
            { AttackBoostPercent = 100 * multiplier, DefenceBoostPercent = 80 * multiplier, TroopType = TroopType.Hitter },
        new UnitBoosts
            { AttackBoostPercent = 100 * multiplier, DefenceBoostPercent = 80 * multiplier, TroopType = TroopType.Shooter }
    };

    public virtual Func<Army, Army, Army> EnemyArmyFunc(FighterConfiguration configuration)=>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            // TODO: Defending army should have a fighter so reduced damage from skills gives a benefit
            ArmyBoosts = new ArmyBoosts
            {
                UnitBoosts = DefaultEnemyBoosts
            },
            Troops = new List<Troop>
            {
                new() { TroopType = TroopType.Hitter, Count = 500000, GearLevel = 5, TroopLevel = 5 },
                new() { TroopType = TroopType.Pilot, Count = 500000, GearLevel = 5, TroopLevel = 5 },
                new() { TroopType = TroopType.Shooter, Count = 500000, GearLevel = 5, TroopLevel = 5 },
            }
        };
    
    public virtual Func<Army, Army, Army> YourArmyFunc(FighterConfiguration configuration) =>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = configuration.ArmyBoosts,
            FighterConfiguration = configuration,
            Troops = configuration.PreferredTroopType == TroopType.ThreeUnitTypes
                ? new List<Troop>
                {
                    new()
                    {
                        Count = (int)(140000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = TroopType.Pilot, GearLevel = 5, TroopLevel = 5
                    },
                    new()
                    {
                        Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = TroopType.Hitter, GearLevel = 5, TroopLevel = 5
                    },
                    new()
                    {
                        Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = TroopType.Shooter, GearLevel = 5, TroopLevel = 5
                    }
                }
                : new List<Troop>
                {
                    new Troop()
                    {
                        Count = (int)(150000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = configuration.PreferredTroopType, GearLevel = 5, TroopLevel = 5
                    }
                }
        };

    public void FlushResults()
    {
        var output = $"{headers}\r\n{bestResultsStringBuilder.ToString()}";
        File.WriteAllText($"{outputPath}//bestResults.txt", output);
    }
    
    public void SaveResults(List<AttackResult> results)
    {
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);
        
        var bestResults = results
            .GroupBy(x => new
            {
                x.YourArmy.FighterConfiguration.Fighter.Name,
                DeputyName = x.YourArmy.FighterConfiguration.DeputyFighter?.Name,
            }) // Only take the best of each breakdown, there are usually lots of variants with stats that don't matter
            .Select(x => x.ToList().GetBestResult())
            .ToList();

        var bestResultsCsv = ResultsToCsv(bestResults, false, false);
        bestResultsStringBuilder.AppendLine(bestResultsCsv);

        foreach (var bestResult in bestResults)
        {
            var fighterConfig = bestResult.YourArmy.FighterConfiguration;
            var fighterSelectedTalents = fighterConfig.SelectedTalents;

            var talentsJson = JsonConvert.SerializeObject(fighterSelectedTalents);
            File.WriteAllText($"{outputPath}//{fighterConfig.Fighter.Name}_{fighterConfig.DeputyFighter?.Name}.bestConfig.txt", talentsJson);
        }

        var resultsByFighter = results.GroupBy(x => x.YourArmy.FighterConfiguration.Fighter.Name);
        foreach (var fighterResults in resultsByFighter)
        {
            var bestFighterResults = fighterResults
                .GroupBy(x => new
                {
                    x.YourArmy.FighterConfiguration.TalentBreakdown,
                    DeputyName = x.YourArmy.FighterConfiguration.DeputyFighter?.Name
                }) // Only take the best of each breakdown, there are usually lots of variants with stats that don't matter
                .Select(x => x.ToList().GetBestResult())
                .ToList();
            
            var fighterResultsCsv = ResultsToCsv(bestFighterResults, true, true);
            File.WriteAllText($"{outputPath}//{fighterResults.Key}.results.txt", fighterResultsCsv);
        }
    }

    private string ResultsToCsv(List<AttackResult> bestResults, bool includeBreakdown, bool includeHeaders)
    {
        
        
        // TODO: Break down by type like research, equipment, talent skill etc.
        var boostTypes = bestResults
            .SelectMany(x => x.YourArmy.FighterConfiguration.SelectedTalents)
            .SelectMany(x => x.Boosts)
            .OrderBy(x => x.Source)
            .Select(x => $"{x.Source}-{x.BoostType}")
            .Distinct();

        if (includeBreakdown)
        {
            foreach (var boostType in boostTypes)
            {
                headers += $"{boostType.ToString()}\t";
            }
        }

        var sb = new StringBuilder();

        if (includeHeaders)
        {
            sb.AppendLine(headers);
        }

        foreach (var result in bestResults)
        {
            sb.Append($"{result.YourArmy.FighterConfiguration.Fighter.Name}\t");
            sb.Append($"{result.YourArmy.FighterConfiguration.DeputyFighter?.Name}-Talent{result.YourArmy.FighterConfiguration.DeputySelectedTalent}\t");
            sb.Append($"{result.NumberOfRounds}\t");
            sb.Append($"{result.HighestYourDamage}\t");
            sb.Append($"{result.HighestYourSkillDamage}\t");
            sb.Append($"{result.TotalEnemyLostTroops}\t");
            sb.Append($"{result.TotalYourLostTroops}\t");
            sb.Append($"{result.YourArmy.FighterConfiguration.TalentBreakdown}\t");

            sb.Append($"{result.FightOptions.UseCannons}\t");
            sb.Append($"{result.FightOptions.Seige}\t");
            sb.Append($"{result.FightOptions.Gathering}\t");
            sb.Append($"{result.FightOptions.MapBattle}\t");
            sb.Append($"{result.FightOptions.UseShooterUnitSkill}\t");

            if (includeBreakdown)
            {
                var resultBoosts = result
                    .YourArmy
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
            }

            sb.Append("\r\n");
        }

        var resultsCsv = sb.ToString();
        return resultsCsv;
    }

}