﻿// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var sb = new StringBuilder();

// TODO: Would be good to have a separate images folder that it scans and batches into groups of 50 for uploading

var resultsDirectory = @"C:\Users\craigrpears\Downloads\ScoutingScreenshots\Results";
var errorsDirectory = @"C:\Users\craigrpears\Downloads\ScoutingScreenshots\Errors";
var directories = Directory.GetDirectories(resultsDirectory);
foreach (var directory in directories)
{
    var files = Directory.GetFiles(directory);
    foreach (var file in files)
    {
        // TODO: add a try catch and move any failures into a folder, move any successes into a success folder
        try
        {
            var lines = File.ReadLines(file)
                .ToList()
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
            var iron = numberLines[1];
            var troops = numberLines[2];
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

            var x = parts[0];
            var y = parts[1];

            sb.Append($"{x},{y},{food},{iron},{troops},{clan},{name}\r\n");
        
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            File.Move(file, $"{errorsDirectory}/error_{DateTime.Now.Ticks}");
        }
    }

}

// TODO: Put this into an output file
// TODO: Would be nice to have a similar program that scans for names of known players in screenshots that have moved from their previous co-ordinates, or even use a crawler of some kind that does this automatically


var output = sb.ToString();
Console.Write(output);