using BlazorApp1.Shared.Enums;

namespace BlazorApp1.Shared;

public class ScoutingEvent
{
    public string PlayerName { get; set; }
    public string ClanName { get; set; }

    public string FullName => string.IsNullOrEmpty(ClanName) ? PlayerName : $"[{ClanName}]{PlayerName}";
    public int ArmyCount { get; set; }
    // Might need power to get this accurate.  Defeated a 9M power city with 380k troops using around 100 ammo, mainly firing with Derrick
    public int EstimatedCannonAmmoRequired => ArmyCount / 5000;

    public Difficulty Difficulty
    {
        get
        {
            // TODO: make this configurable in the UI so adjust to different player power, base it on power somehow?
            if (ArmyCount < 2500) return Difficulty.Undefended; // No need for mechs, just send an army
            if (ArmyCount < 50000) return Difficulty.MinimalLosses; // Good idea to soften up with a mech first to minimise losses
            if (ArmyCount < 100000) return Difficulty.MediumLosses; // Going to hurt the mechs a bit
            if (ArmyCount < 250000) return Difficulty.HighLosses; // Causes a few million in rss costs to mechs with cannon support
            if (ArmyCount < 500000) return Difficulty.HeavyLosses; 
            return Difficulty.CrazyLosses; 
        }
        
    }
    
    public int IronAmount { get; set; }
    public int FoodAmount { get; set; }
    public int TotalResources => IronAmount + FoodAmount;
    public int MaxResources { get; set; }
    public int DistinctResourceCount { get; set; }
    public int EmptyResourcesCount { get; set; }

    public int ResourceToArmyRatio => TotalResources / (ArmyCount > 0 ? ArmyCount : 1);

    // TODO: Can use this to create a heatmap to see which areas of the map you need to update/scout
    public DateTime DateLastScouted { get; set; }
    public DateTime DateLastScoutedWithResources { get; set; }
    public int DaysSinceScouted => (DateTime.Now - DateLastScouted).Days;
    public int DaysSinceScoutedWithResources => (DateTime.Now - DateLastScoutedWithResources).Days;
    public int xCoordinates { get; set; }
    public int yCoordinates { get; set; }

    public string grid => $"{xCoordinates / 4000},{yCoordinates / 2500}";

    public Double Decay => (DaysSinceScouted / 100.0);
    public int GlobalRank { get; set; }
    public int RegionRank { get; set; }

    public ScoutingEvent()
    {
        
    }

    // For when we've tried to scout someone and they aren't at their previous position
    public ScoutingEvent(string name, DateTime lastScouted)
    {
        PlayerName = name;
        DateLastScouted = lastScouted;
    }

    public ScoutingEvent(int x, int y, int food, int iron, int army, string clan, string name, DateTime lastScouted)
    {
        xCoordinates = x;
        yCoordinates = y;
        FoodAmount = food;
        IronAmount = iron;
        ArmyCount = army;
        ClanName = clan;
        PlayerName = name;
        DateLastScouted = lastScouted;
    }
    
    public ScoutingEvent(int x, int y, string playerName)
    {
        xCoordinates = x;
        yCoordinates = y;
        PlayerName = playerName;
    }

    public string ToOutputLine() => $"{xCoordinates},{yCoordinates},{FoodAmount},{IronAmount},{ArmyCount},{ClanName},{PlayerName}\r\n";
}