using System.Collections;
using System.Globalization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using BlazorApp1.Shared;
using BlazorApp1.Shared.Enums;
using BlazorApp1.Shared.Extensions;
using CsvHelper;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LootingLocationsController : ControllerBase
{
    private readonly ILogger<LootingLocationsController> _logger;

    public LootingLocationsController(ILogger<LootingLocationsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public LootingMap Get([FromQuery] LootingMapFilters? filters)
    {
        var scoutingEvents = new List<ScoutingEvent>();

        var outputFolder = @"\\nas-pears\documents\AgeOfApes\ScoutingScreenshots\FilesToProcess\Output";
        var outputDirectory = new DirectoryInfo(outputFolder);
        var files = outputDirectory.GetFiles();
        foreach (var file in files)
        {
            var day = int.Parse(file.Name.Substring(0, 2));
            var month = int.Parse(file.Name.Substring(2, 2));
            var year = int.Parse(file.Name.Substring(4, 4));

            LoadScoutingEvents(file.FullName, scoutingEvents, new DateTime(year, month, day));
        }
        
        #region notfound
        
        /*
        // Scouted players who can't be found at their previous co-ordinates or have no resources
        scoutingEvents.Add(new ScoutingEvent("Elbatlan", new DateTime(2023, 11, 21)));
        scoutingEvents.Add(new ScoutingEvent("ApeYYWB2M7", new DateTime(2023, 11, 21)));
        scoutingEvents.Add(new ScoutingEvent("polroncopol", new DateTime(2023, 11, 21)));
        scoutingEvents.Add(new ScoutingEvent("ApeYYWB2M7", new DateTime(2023, 11, 25)));
        scoutingEvents.Add(new ScoutingEvent("Allawi1997", new DateTime(2023, 11, 25)));
        scoutingEvents.Add(new ScoutingEvent("Haranbanjo", new DateTime(2023, 11, 25)));
        scoutingEvents.Add(new ScoutingEvent("Ahmt6", new DateTime(2023, 11, 25)));
        scoutingEvents.Add(new ScoutingEvent("Daallusa", new DateTime(2023, 11, 25)));
        scoutingEvents.Add(new ScoutingEvent("AEW", new DateTime(2023, 11, 25)));
        //TODO: Move these events and attack events into a text file that can be more easily modified
        scoutingEvents.Add(new ScoutingEvent("fang8899", new DateTime(2023, 12, 04)));
        scoutingEvents.Add(new ScoutingEvent("noespoepoe", new DateTime(2023, 12, 04)));
        scoutingEvents.Add(new ScoutingEvent("ApeA9DNL5A", new DateTime(2023, 12, 04)));
        scoutingEvents.Add(new ScoutingEvent("Jas308", new DateTime(2023, 12, 04)));
        scoutingEvents.Add(new ScoutingEvent("Alesinaleksej", new DateTime(2023, 12, 05)));
        scoutingEvents.Add(new ScoutingEvent("B99", new DateTime(2024, 1, 2)));
        scoutingEvents.Add(new ScoutingEvent("Esscro", new DateTime(2024, 1, 2)));
        scoutingEvents.Add(new ScoutingEvent("Barbiedawg", new DateTime(2024, 1, 2)));
        scoutingEvents.Add(new ScoutingEvent("Apx", new DateTime(2024, 1, 2)));
        scoutingEvents.Add(new ScoutingEvent("Barbarossa", new DateTime(2024, 1, 2)));
        */
        #endregion
        
        
        var lootingLocations = scoutingEvents
            .OrderBy(x => x.DateLastScouted)
            .GroupBy(x => x.PlayerName.ToLower().Trim())
            .Select(x => new LootingLocation { ScoutingEvents = x.ToList() })
            .ToList();
        
        lootingLocations.ForEach(x => x.ScoutingEvents
            .ForEach(s => s.MaxResources = x.ScoutingEvents.MaxBy(max => max.TotalResources).TotalResources)
        );
        
        lootingLocations
            .Where(x => x.ScoutingEvents.Any(e => e.TotalResources > 0))
            .ToList()
            .ForEach(x => x.ScoutingEvents
            .ForEach(s => s.DateLastScoutedWithResources = x.ScoutingEvents
                .Where(s => s.TotalResources > 0)
                .MaxBy(max => max.DateLastScouted).DateLastScouted)
        );
        
        
        
        lootingLocations.ForEach(x => x.ScoutingEvents
            .ForEach(s => s.DistinctResourceCount = x.ScoutingEvents
                .Select(d => d.TotalResources)
                .Where(n => n > 0)
                .Distinct()
                .Count())
        );
        
        lootingLocations.ForEach(x => x.ScoutingEvents
            .ForEach(s => s.EmptyResourcesCount = x.ScoutingEvents
                .Count(d => d.TotalResources == 0))
        );
        
        var clanBases = new List<MapSpot>
        {
            new (2135, 1125), // TWU
            new (800, 3620)// EMP
        };

        var protectionRadius = 250;
        
        var orderedEvents = lootingLocations
            .Select(x => x.ScoutingEvents.MaxBy(x => x.DateLastScouted))
            .Where(x => x.xCoordinates != 0) // Hide ones that have moved and we haven't been able to find again yet
            
            .Where(x => !clanBases.Any(y => new MapSpot(x.xCoordinates, x.yCoordinates).DistanceToSpot(y) < protectionRadius))
            .OrderBy(x => x.FoodAmount + x.IronAmount)
            .ToList();

        var biggestTotal = 0;
        var biggestTotalX = 0;
        var biggestTotalY = 0;
        
        
        for (var xPos = 0; xPos < 9000; xPos += 50)
        {
            for (var yPos = 0; yPos < 9000; yPos += 50)
            {
                var spot = new MapSpot(xPos, yPos);
                var spotTotal = orderedEvents
                    //.Where(x => x.grid == "0,2")
                    .Where(x => new MapSpot(x.xCoordinates, x.yCoordinates).DistanceToSpot(spot) < 500)
                    .Sum(x => x.TotalResources);

                if (spotTotal > biggestTotal)
                {
                    biggestTotal = spotTotal;
                    biggestTotalX = xPos;
                    biggestTotalY = yPos;
                }
            }
        }
        
        Console.WriteLine($"Biggest total is {biggestTotal.ToMillionsText()} at {biggestTotalX},{biggestTotalY}");

        if (filters?.SpotX != null)
        {
            orderedEvents = orderedEvents
                .Where(x => 
                      Math.Abs(x.xCoordinates - filters.SpotX.Value) 
                    + Math.Abs(x.yCoordinates - filters.SpotY.Value) 
                    < filters.SpotRadius.Value)
                .ToList();
        }
        
        var rankedEvents = orderedEvents.OrderByDescending(x => x.TotalResources / (int)x.Difficulty).Select((x, i) => (x, i)).ToList();
        rankedEvents.ForEach(x => x.x.GlobalRank = x.i);
        
        var regionEvents = orderedEvents
            .GroupBy(x => x.grid)
            .ToList();
        
        foreach (var region in regionEvents)
        {
            var regionRankedEvents = region.ToList().OrderByDescending(x => x.TotalResources / (int)x.Difficulty).Select((x, i) => (x, i)).ToList();
            regionRankedEvents.ForEach(x => x.x.RegionRank = x.i);
        }
        
        // TODO: Split out scouting list to a separate page
        // TODO: Add alongside scouting list top ten targets
        // TODO: Add different filtering options for the map like scouting mode, looting mode etc.
        
        
        var randomLocationsToScout = orderedEvents
            //.Where(x => x.grid == "1,2")
            .OrderBy(x => x.GlobalRank)
            .Take(50).ToList();

        var regionStats = orderedEvents
            .GroupBy(x => x.grid)
            .Select(x =>
            {
                return new RegionStats
                {
                    Name = x.Key,
                    TotalResources = x.Sum(y => y.TotalResources),
                    Count = x.Count()
                };
            })
            .OrderByDescending(x => x.TotalResources)
            .ToList();
        
        try
        {
            var csvOutputPath = @"\\nas-pears\documents\AgeOfApes\scoutingData.csv";
            using var writer = new StreamWriter(csvOutputPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords((IEnumerable)orderedEvents);
        }
        catch (Exception ex)
        {
            
        }

        var mapModel = new LootingMap
        {
            LootingLocations = orderedEvents, 
            ScoutingLocations = randomLocationsToScout,
            RegionStats = regionStats,
            BestSpotTotalResources = biggestTotal,
            BestSpotX = biggestTotalX,
            BestSpotY = biggestTotalY
        };
        
        return mapModel;
    }

    private static void LoadScoutingEvents(string path, List<ScoutingEvent> scoutingEvents, DateTime scoutedDate)
    {
        var data = System.IO.File
            .ReadAllLines(path)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        if (path.Contains("_clear"))
        {
            // Make sure clear files are at the end of the day so they override anything scouted on that day
            scoutedDate = scoutedDate.AddHours(23);
        }
        
        foreach (var location in data) // TODO: Likely to forget to change the number on this, needs improving
        {
            var parts = location.Split(",");
            // TODO: need to remember to change the date below each time
            scoutingEvents.Add(new ScoutingEvent(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]),
                int.Parse(parts[3]), int.Parse(parts[4]), parts[5], parts[6], scoutedDate));
        }
    }
}