using BlazorApp1.Shared.Enums;

namespace BlazorApp1.Shared;

public class ScoutingEvent
{
    public string PlayerName { get; set; }
    public string ClanName { get; set; }
    // TODO: Put this and power into scouting event as well, can be used to see if a city is dead or just recovering by the trends
    public int ArmyCount { get; set; }

    public Difficulty Difficulty
    {
        get
        {
            if (ArmyCount < 2500) return Difficulty.Undefended;
            if (ArmyCount < 15000) return Difficulty.MinimalLosses; // Losses should be in the low tens, a good fighter can solo this
            if (ArmyCount < 50000) return Difficulty.MediumLosses; // Could get into the dozens or hundreds, better use two fighters
            if (ArmyCount < 100000) return Difficulty.HighLosses; // Can lose hundreds or thousands, definitely need the cannon and efficient attacking to soften this up first
            
            // Example: 350k defenders with walls taken down first, lost about 12k troops.  They were very low tech and T2 at most however.
            if (ArmyCount < 500000) return Difficulty.HeavyLosses; // There are going to be heavy losses that will take days to replace on this
            return Difficulty.CrazyLosses; // Going to lose significant chunks of your army even with a cannon on this, the resources really need to be worth it
        }
        
    }
    public int Power { get; set; }
    
    // TODO: Resources and scout time should be a list of scout events so you can see a history.  Attacking should be a special type
    // TODO: Turn these into enums for ranges for the more common ones to make data input easier
    public int IronAmount { get; set; }
    public int FoodAmount { get; set; }
    public int TotalResources => IronAmount + FoodAmount;

    public int ResourceToArmyRatio => TotalResources / (ArmyCount > 0 ? ArmyCount : 1);

    // TODO: Can use this to create a heatmap to see which areas of the map you need to update/scout
    public DateTime DateLastScouted { get; set; }
    public DateTime? DateLastAttacked { get; set; }
    
    public bool Recovering => DateLastAttacked >= DateLastScouted; 
    public int xCoordinates { get; set; }
    public int yCoordinates { get; set; }

    public string grid => $"{xCoordinates / 800},{yCoordinates / 800}";
    
    [Obsolete]
    public int CityLevel { get; set; }
    
    // TODO: Make this more granular to build up a picture of how much power is needed to achive minimal losses, should be a list of attacks
    public int LossesOnLastAttack { get; set; }  

    public ScoutingEvent()
    {
        
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
}