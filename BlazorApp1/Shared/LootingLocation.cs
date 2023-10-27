using BlazorApp1.Shared.Enums;

namespace BlazorApp1.Shared;

public class LootingLocation
{
    public string PlayerName { get; set; }
    public string ClanName { get; set; }
    public int CityLevel { get; set; }
    // TODO: Put this and power into scouting event as well, can be used to see if a city is dead or just recovering by the trends
    public int ArmyCount { get; set; } 
    public int Power { get; set; }
    
    // TODO: Resources and scout time should be a list of scout events so you can see a history.  Attacking should be a special type
    // TODO: Turn these into enums for ranges for the more common ones to make data input easier
    public int IronAmount { get; set; }
    public int FoodAmount { get; set; }
    public ResourceLevel ResourceLevel { get; set; }
    public int TotalResources
    {
        get
        {
            switch (ResourceLevel)
            {
                case ResourceLevel.None:
                case ResourceLevel.Low:
                case ResourceLevel.Medium:
                    return (int)ResourceLevel;
                default:
                    return IronAmount + FoodAmount;
            }
        }
    }

    // TODO: Can use this to create a heatmap to see which areas of the map you need to update/scout
    public DateTime DateLastScouted { get; set; }
    public DateTime? DateLastAttacked { get; set; }
    
    public bool Recovering => DateLastAttacked >= DateLastScouted; 
    public int xCoordinates { get; set; }
    public int yCoordinates { get; set; }
    
    // TODO: Make this more granular to build up a picture of how much power is needed to achive minimal losses, should be a list of attacks
    public int LossesOnLastAttack { get; set; }  

    public LootingLocation()
    {
        
    }

    public LootingLocation(int x, int y, int food, int iron, int army, string clan, string name, DateTime lastScouted)
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
    
    public LootingLocation(int x, int y, ResourceLevel resourceLevel, int army, string clan, string name, DateTime lastScouted)
    {
        xCoordinates = x;
        yCoordinates = y;
        ResourceLevel = resourceLevel;
        ArmyCount = army;
        ClanName = clan;
        PlayerName = name;
        DateLastScouted = lastScouted;
    }
    
    public LootingLocation(int x, int y, string playerName)
    {
        xCoordinates = x;
        yCoordinates = y;
        PlayerName = playerName;
    }
}