using System.Text;
using FightSimulator.Core.DatabaseEntities;
using FightSimulator.Core.Extensions;
using FightSimulator.Core.Models;

namespace FightSimulator.Core.Repositories;

public interface IFightResultsRepository
{
    void SaveResults(List<AttackResult> results, string outputPath);
    void FlushResults(string outputPaths);
    void SaveFighterResults(List<AttackResult> results, string outputPath);
    void SaveBestConfigs(List<AttackResult> bestResults, string outputPath);
    void SaveStructuredFightResults(List<AttackResult> results, string outputPath);
    string ResultsToCsv(List<AttackResult> bestResults, bool includeBreakdown, bool includeHeaders);
    DateTime? GetLastRanDate(string outputFolder, string? prefix);
    List<FightResultEntity> GetFightResults(string outputPath, string? fighterName = null);
    List<FightResultEntity> GetBestFightResults(string outputPath);
    FightResultEntity? GetBestFightResultForFighter(string outputPath, string fighterName, string? deputyName = null, int? deputyTalent = null);
}
