﻿namespace BlazorApp1.Shared.FighterSimulator.Extensions;

public static class AttackResultExtensions
{
    public static AttackResult GetBestResult(this List<AttackResult> results)
    {
        var bestResult = results
            .OrderByDescending(x => x.TotalEnemyLostTroops)
            .ThenByDescending(x => x.YourRemainingTroops)
            .ThenBy(x => x.NumberOfRounds)
            .ThenByDescending(x => x.AttackLogs.Max(a => a.YourDamage))
            .First();

        return bestResult;
    }
}