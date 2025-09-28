using System.Text;
using System.Text.Json;
using FightSimulator.Core.DatabaseEntities;
using FightSimulator.Core.Extensions;
using FightSimulator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FightSimulator.Core.Repositories;

public class DatabaseFightResultsRepository : IFightResultsRepository
{
    private readonly FightResultsDbContext _context;
    private readonly StringBuilder _bestResultsStringBuilder = new();
    protected string _headers = "Fighter\tDeputy\tRoundsTaken\tMaxDamage\tMaxSkillDamage\tTotalNormalDamage\tTotalSkillDamage\tEnemyLosses\tYourLosses\tYourRemainingTroops\tEnemyRemainingTroops\tKillRatio\tTalentBreakdown\t";

    public DatabaseFightResultsRepository(FightResultsDbContext context)
    {
        _context = context;
    }

    public void SaveResults(List<AttackResult> results, string outputPath)
    {
        var bestResults = results
            .GroupBy(x => new
            {
                x.YourArmy.FighterConfiguration.Fighter.Name,
                DeputyName = x.YourArmy.FighterConfiguration.DeputyFighter?.Name,
                x.YourArmy.FighterConfiguration.DeputySelectedTalent
            })
            .Select(x => x.ToList().GetBestResult())
            .ToList();

        var bestResultsCsv = ResultsToCsv(bestResults, false, false);
        _bestResultsStringBuilder.AppendLine(bestResultsCsv);

        SaveBestConfigs(bestResults, outputPath);
        SaveFighterResults(results, outputPath);
        SaveStructuredFightResults(results, outputPath);
    }

    public void FlushResults(string outputPath)
    {
        var output = $"{_headers}\r\n{_bestResultsStringBuilder.ToString()}";
        var fileName = "bestResults.txt";
        
        var existingEntity = _context.BestResults
            .FirstOrDefault(x => x.FileName == fileName && x.OutputPath == outputPath);

        if (existingEntity != null)
        {
            existingEntity.Content = output;
            existingEntity.LastModifiedDate = DateTime.UtcNow;
        }
        else
        {
            _context.BestResults.Add(new BestResultsEntity
            {
                FileName = fileName,
                OutputPath = outputPath,
                Content = output,
                CreatedDate = DateTime.UtcNow
            });
        }

        _context.SaveChanges();
        _bestResultsStringBuilder.Clear();
    }

    public void SaveFighterResults(List<AttackResult> results, string outputPath)
    {
        var resultsByFighter = results.GroupBy(x => x.YourArmy.FighterConfiguration.Fighter.Name);
        
        foreach (var fighterResults in resultsByFighter)
        {
            var bestFighterResults = fighterResults
                .GroupBy(x => new
                {
                    x.YourArmy.FighterConfiguration.TalentBreakdown,
                    DeputyName = x.YourArmy.FighterConfiguration.DeputyFighter?.Name,
                    x.YourArmy.FighterConfiguration.DeputySelectedTalent
                })
                .Select(x => x.ToList().GetBestResult())
                .ToList();
            
            var fighterResultsCsv = ResultsToCsv(bestFighterResults, true, true);
            var fileName = fighterResults.Key;
            
            var existingEntity = _context.FighterResults
                .FirstOrDefault(x => x.FileName == fileName && x.OutputPath == outputPath);

            if (existingEntity != null)
            {
                existingEntity.Content = fighterResultsCsv;
                existingEntity.LastModifiedDate = DateTime.UtcNow;
            }
            else
            {
                _context.FighterResults.Add(new FighterResultsEntity
                {
                    FileName = fileName,
                    OutputPath = outputPath,
                    Content = fighterResultsCsv,
                    CreatedDate = DateTime.UtcNow
                });
            }
        }
        
        _context.SaveChanges();
    }

    public void SaveBestConfigs(List<AttackResult> bestResults, string outputPath)
    {
        var entitiesToSave = new List<BestConfigsEntity>();
        var fileNames = new List<string>();
        
        foreach (var bestResult in bestResults)
        {
            var fighterConfig = bestResult.YourArmy.FighterConfiguration;
            var fighterSelectedTalents = fighterConfig.SelectedTalents;

            var talentsJson = JsonSerializer.Serialize(fighterSelectedTalents);
            var fileName = $"{fighterConfig.Fighter.Name}_{fighterConfig.DeputyFighter?.Name}_{fighterConfig.DeputySelectedTalent}";
            fileNames.Add(fileName);
            
            entitiesToSave.Add(new BestConfigsEntity
            {
                FileName = fileName,
                OutputPath = outputPath,
                Content = talentsJson,
                CreatedDate = DateTime.UtcNow
            });
        }

        var existingEntities = _context.BestConfigs
            .Where(x => x.OutputPath == outputPath && fileNames.Contains(x.FileName))
            .ToDictionary(x => new { x.FileName, x.OutputPath }, x => x);

        var entitiesToAdd = new List<BestConfigsEntity>();
        
        foreach (var entity in entitiesToSave)
        {
            var key = new { entity.FileName, entity.OutputPath };
            
            if (existingEntities.TryGetValue(key, out var existingEntity))
            {
                existingEntity.Content = entity.Content;
                existingEntity.LastModifiedDate = DateTime.UtcNow;
            }
            else
            {
                entitiesToAdd.Add(entity);
            }
        }

        if (entitiesToAdd.Any())
        {
            _context.BestConfigs.AddRange(entitiesToAdd);
        }
        
        _context.SaveChanges();
    }

    public void SaveStructuredFightResults(List<AttackResult> results, string outputPath)
    {
        var entitiesToSave = new List<FightResultEntity>();
        
        foreach (var result in results)
        {
            var fighterConfig = result.YourArmy.FighterConfiguration;
            var configurationJson = JsonSerializer.Serialize(new
            {
                SelectedTalents = fighterConfig.SelectedTalents,
                TalentBreakdown = fighterConfig.TalentBreakdown,
                DeputyFighter = fighterConfig.DeputyFighter?.Name,
                DeputySelectedTalent = fighterConfig.DeputySelectedTalent
            });

            entitiesToSave.Add(new FightResultEntity
            {
                FighterName = fighterConfig.Fighter.Name,
                DeputyName = fighterConfig.DeputyFighter?.Name,
                DeputySelectedTalent = fighterConfig.DeputySelectedTalent,
                TalentBreakdown = fighterConfig.TalentBreakdown,
                NumberOfRounds = result.NumberOfRounds,
                HighestYourDamage = result.HighestYourDamage,
                HighestYourSkillDamage = result.HighestYourSkillDamage,
                TotalNormalDamage = (int)result.AttackLogs.Sum(x => x.YourNormalDamage),
                TotalSkillDamage = (int)result.AttackLogs.Sum(x => x.YourSkillDamage),
                EnemyLosses = result.TotalEnemyLostTroops,
                YourLosses = result.TotalYourLostTroops,
                YourRemainingTroops = result.YourRemainingTroops,
                EnemyRemainingTroops = result.EnemyRemainingTroops,
                KillRatio = result.KillRatio,
                OutputPath = outputPath,
                ConfigurationJson = configurationJson,
                CreatedDate = DateTime.UtcNow
            });
        }

        _context.FightResults.AddRange(entitiesToSave);
        _context.SaveChanges();
    }

    public string ResultsToCsv(List<AttackResult> bestResults, bool includeBreakdown, bool includeHeaders)
    {
        var sourceBoostTypes = bestResults
            .SelectMany(x => x.YourArmy.FighterConfiguration.SelectedTalents)
            .SelectMany(x => x.Boosts)
            .OrderBy(x => x.Source)
            .Select(x => $"{x.Source}-{x.BoostType}")
            .Distinct();
        
        var boostTypes = bestResults
            .SelectMany(x => x.YourArmy.FighterConfiguration.SelectedTalents)
            .SelectMany(x => x.Boosts)
            .Select(x => $"{x.BoostType}")
            .Distinct();

        var fileHeaders = _headers;

        if (includeBreakdown)
        {
            foreach (var boostType in boostTypes)
            {
                fileHeaders += $"{boostType}\t";
            }
            
            foreach (var boostType in sourceBoostTypes)
            {
                fileHeaders += $"{boostType}\t";
            }
        }

        var sb = new StringBuilder();

        if (includeHeaders)
        {
            sb.AppendLine(fileHeaders);
        }

        foreach (var result in bestResults)
        {
            sb.Append($"{result.YourArmy.FighterConfiguration.Fighter.Name}\t");
            sb.Append($"{result.YourArmy.FighterConfiguration.DeputyFighter?.Name}-Talent{result.YourArmy.FighterConfiguration.DeputySelectedTalent}\t");
            sb.Append($"{result.NumberOfRounds}\t");
            sb.Append($"{result.HighestYourDamage}\t");
            sb.Append($"{result.HighestYourSkillDamage}\t");
            sb.Append($"{result.AttackLogs.Sum(x => x.YourNormalDamage)}\t");
            sb.Append($"{result.AttackLogs.Sum(x => x.YourSkillDamage)}\t");
            sb.Append($"{result.TotalEnemyLostTroops}\t");
            sb.Append($"{result.TotalYourLostTroops}\t");
            sb.Append($"{result.YourRemainingTroops}\t");
            sb.Append($"{result.EnemyRemainingTroops}\t");
            sb.Append($"{result.KillRatio}\t");
            sb.Append($"{result.YourArmy.FighterConfiguration.TalentBreakdown}\t");
            
            if (includeBreakdown)
            {
                var resultBoosts = result
                    .YourArmy
                    .ArmyBoosts
                    .ApplicableBoosts
                    .GroupBy(x => $"{x.BoostType}");
                
                var resultBoostsWithSource = result
                    .YourArmy
                    .ArmyBoosts
                    .ApplicableBoosts
                    .OrderBy(x => x.Source)
                    .GroupBy(x => $"{x.Source}-{x.BoostType}");

                AddBoostDetails(boostTypes, resultBoosts, sb);
                AddBoostDetails(sourceBoostTypes, resultBoostsWithSource, sb);
            }

            sb.Append("\r\n");
        }

        var resultsCsv = sb.ToString();
        return resultsCsv;
    }

    public DateTime? GetLastRanDate(string outputFolder, string? prefix)
    {
        var query = _context.BestResults
            .Where(x => x.OutputPath == outputFolder);

        if (!string.IsNullOrEmpty(prefix))
        {
            query = query.Where(x => x.FileName.StartsWith(prefix));
        }

        var earliestDate = query
            .Min(x => (DateTime?)x.CreatedDate);

        return earliestDate;
    }

    public List<FightResultEntity> GetFightResults(string outputPath, string? fighterName = null)
    {
        var query = _context.FightResults
            .Where(x => x.OutputPath == outputPath);

        if (!string.IsNullOrEmpty(fighterName))
        {
            query = query.Where(x => x.FighterName == fighterName);
        }

        return query.OrderByDescending(x => x.CreatedDate).ToList();
    }

    public List<FightResultEntity> GetBestFightResults(string outputPath)
    {
        return _context.FightResults
            .Where(x => x.OutputPath == outputPath)
            .GroupBy(x => new { x.FighterName, x.DeputyName, x.DeputySelectedTalent })
            .Select(g => g.OrderByDescending(x => x.KillRatio).First())
            .ToList();
    }

    public FightResultEntity? GetBestFightResultForFighter(string outputPath, string fighterName, string? deputyName = null, int? deputyTalent = null)
    {
        var query = _context.FightResults
            .Where(x => x.OutputPath == outputPath && x.FighterName == fighterName);

        if (!string.IsNullOrEmpty(deputyName))
        {
            query = query.Where(x => x.DeputyName == deputyName);
        }

        if (deputyTalent.HasValue)
        {
            query = query.Where(x => x.DeputySelectedTalent == deputyTalent);
        }

        return query.OrderByDescending(x => x.KillRatio).FirstOrDefault();
    }

    private static void AddBoostDetails(IEnumerable<string> sourceBoostTypes, IEnumerable<IGrouping<string, Boost>> resultBoostsWithSource, StringBuilder sb)
    {
        foreach (var boostType in sourceBoostTypes)
        {
            var matchingBoost = resultBoostsWithSource.SingleOrDefault(x => x.Key == boostType);
            if (matchingBoost != null)
            {
                sb.Append($"{matchingBoost.ToList().Sum(b => b.MaxBoostAmount)}");
            }

            sb.Append("\t");
        }
    }
}
