namespace BlazorApp1.Shared.Extensions;

public static class NumberExtensions
{
    private const int ONE_MILLION = 1000000;
    public static string ToMillionsText(this int number) => Math.Round((double)number / ONE_MILLION, 1) + "M";
}