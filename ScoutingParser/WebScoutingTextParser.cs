using System.Text.RegularExpressions;
using BlazorApp1.Shared;

namespace ScoutingParser;

public class WebScoutingTextParser : IScoutingTextParser
{
    public ScoutingEvent ParseScoutingText(List<string> textLines)
    {
        var lines = textLines
            .Where(x => !string.IsNullOrEmpty(x))
            .Where(x => !x.Contains("TROOP"))
            .Where(x => !x.Contains("Plundered"));

        var numberLines = lines
            .Where(x => int.TryParse(x.Replace(",",""), out var result))
            .Select(x => int.Parse(x.Replace(",","")))
            .ToList();
            
        var stringLines = lines.Where(x => !int.TryParse(x, out var result)).ToList();
            
        var powerLine = stringLines.Single(x => x.Contains("Power"));
        var coordinatesLine = stringLines.Single(x => x.Contains("X:"));
        var nameLine = stringLines.First();
        if (nameLine.Contains("X:"))
        {
            nameLine = nameLine.Substring(0, nameLine.IndexOf("X:"));
        }
            
        var name = nameLine;
        var coordinates = coordinatesLine.Substring(coordinatesLine.IndexOf("X:"));
        var power = powerLine;
        var food = numberLines[0];
        var iron = -1;
        if (numberLines.Count > 1)
        {
            iron = numberLines[1];
        }

        var troops = -1;
        if (numberLines.Count > 2)
        {
            troops = numberLines[2];
        }
        var clan = "";

        var nameRegex = new Regex("\\[(.+)\\](.+)");
        var match = nameRegex.Match(name);
        if (match.Success)
        {
            clan = match.Groups[1].Value;
            name = match.Groups[2].Value;
        }

        coordinates = coordinates.Replace("X:", "");
        coordinates = coordinates.Replace("Y:", "");
        var parts = coordinates.Split(" ");

        var x = int.Parse(parts[0]);
        var y = int.Parse(parts[1]);

        var scoutingEvent = new ScoutingEvent(x, y, food, iron, troops, clan, name, DateTime.Now);
        return scoutingEvent;
    }
}