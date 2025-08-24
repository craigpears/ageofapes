using System.Text;
using FightSimulator.Core.Extensions;

namespace FightSimulator.Core.Repositories;

public interface IFightResultsRepository
{
    void SaveResults(List<AttackResult> results, string outputPath);
    void FlushResults(string outputPaths);
    void SaveFighterResults(List<AttackResult> results, string outputPath);
    void SaveBestConfigs(List<AttackResult> bestResults, string outputPath);
    string ResultsToCsv(List<AttackResult> bestResults, bool includeBreakdown, bool includeHeaders);
    DateTime? GetLastRanDate(string outputFolder, string? prefix);
}
