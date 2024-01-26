using BlazorApp1.Shared;

namespace ScoutingParser;

public interface IScoutingTextParser
{
    public ScoutingEvent ParseScoutingText(List<string> text);
}