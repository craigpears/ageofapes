namespace BlazorApp1.Shared;

public class LootingMap
{
    public List<ScoutingEvent> LootingLocations { get; set; }
    public List<ScoutingEvent> ScoutingLocations { get; set; } = new List<ScoutingEvent>();
    public List<RegionStats> RegionStats { get; set; } = new List<RegionStats>();
    public int BestSpotTotalResources { get; set; }
    public int BestSpotX { get; set; }
    public int BestSpotY { get; set; }
}