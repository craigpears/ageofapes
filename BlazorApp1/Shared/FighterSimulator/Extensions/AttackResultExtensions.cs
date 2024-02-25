namespace BlazorApp1.Shared.FighterSimulator.Extensions;

public static class AttackResultExtensions
{
    public static AttackResult GetBestResult(this List<AttackResult> results)
    {
        var bestResult = results
            .OrderBy(x => x.AttackLogs.Count)
            .ThenByDescending(x => x.AttackLogs.Max(a => a.AttackerDamage))
            .First();

        return bestResult;
    }
}