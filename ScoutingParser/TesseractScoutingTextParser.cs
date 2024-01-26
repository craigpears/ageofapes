using System.Text.RegularExpressions;
using BlazorApp1.Shared;

namespace ScoutingParser;

public class TesseractScoutingTextParser :IScoutingTextParser
{
    /*
     * Example:
[The]UTCalexalbonMdo Piunderable Resources
X:4583 Y:6608

Power: 4,357,252

Z 2,945,101 Aer 2,116,374

 

TROOP INFO &X 98,600

 

     */
    public ScoutingEvent ParseScoutingText(List<string> text)
    {
        var nonBlankLines = text.Where(x => !string.IsNullOrEmpty(x));
        var troopInfoLine = nonBlankLines.SingleOrDefault(x => x.Contains("TROOP"));
        Console.WriteLine("Finding co-ordinates");
        var coordinatesLine = nonBlankLines.Single(x => x.Contains("X:", StringComparison.InvariantCultureIgnoreCase));
        Console.WriteLine("Finding name line");
        var nameLine = nonBlankLines.First();
        var numberRegex = new Regex("[0-9]+");
        
        Console.WriteLine("Finding resources line");
        var resourcesLine = nonBlankLines
            .SingleOrDefault(x => 
                x != troopInfoLine
                && x != coordinatesLine 
                && x != nameLine
                && !x.Contains("Power")
                && numberRegex.IsMatch(x)
                )
            ?.Replace(",", "");

        Console.WriteLine("Parsing name");
        var name = nameLine
            .Replace("Plunderable Resources", "")
            .Replace("PlunderableResources", "")
            .Replace("PiunderableResources", "")
            .Trim();
        var clan = "";
        
        var nameRegex = new Regex("\\[(.+)\\]([a-zA-Z0-9]+)");
        var match = nameRegex.Match(name);
        if (match.Success)
        {
            clan = match.Groups[1].Value;
            name = match.Groups[2].Value;
        }

        Console.WriteLine("Parsing resources");
        var food = -1;
        var iron = -1;
        if (resourcesLine != null)
        {
            var matches = numberRegex.Matches(resourcesLine);

            food = int.Parse(matches[0].Value);
            if(matches.Count > 1)
                iron = int.Parse(matches[1].Value);
        }

        Console.WriteLine("Parsing co-ordinates");
        var coordinatesMatches = numberRegex.Matches(coordinatesLine);
        
        var x = int.Parse(coordinatesMatches[0].Value);
        var y = int.Parse(coordinatesMatches[1].Value);

        Console.WriteLine("Parsing troops");
        var troops = -1;

        if (troopInfoLine != null)
        {

            var troopInfo = troopInfoLine.Trim()
                .Split(" ")
                .Last()
                .Replace(",", "")
                .Replace("R", "")
                .Replace("X", "");
            if (int.TryParse(troopInfo, out var parsedTroopInfo))
            {
                troops = parsedTroopInfo;
            }
        }

        var scoutingEvent = new ScoutingEvent(x, y, food, iron, troops, clan, name, DateTime.Now);
        return scoutingEvent;
    }
}