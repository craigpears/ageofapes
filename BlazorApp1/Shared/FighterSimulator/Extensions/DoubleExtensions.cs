namespace BlazorApp1.Shared.FighterSimulator.Extensions;

public static class DoubleExtensions
{
    public static double ToMultiplier(this double percentage)
    {
        var multiplier = 1 + (percentage / 100.0);
        return multiplier;
    }
}