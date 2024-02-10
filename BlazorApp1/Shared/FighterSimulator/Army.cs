namespace BlazorApp1.Shared.FighterSimulator;

public class Army
{
    public ArmyBoosts ArmyBoosts { get; set; }
    public List<Troop> Troops { get; set; }
    public FighterConfiguration FighterConfiguration { get; set; }
}

public class Troop
{
    
    public void CalculateStats(ArmyBoosts armyBoosts)
    {
        var boosts = armyBoosts.UnitBoosts.SingleOrDefault(x => x.TroopType == TroopType);
        CalculatedAttack = Attack * (boosts?.Attack ?? 1);
        CalculatedDefence = Defence * (boosts?.Defence ?? 1);
        CalculatedHealth = Health * (boosts?.Health ?? 1);
    }
    
    public double CalculatedAttack { get; set; }
    public double CalculatedDefence { get; set; }
    public double CalculatedHealth { get; set; }
    
    public TroopType TroopType { get; set; }
    public int TroopLevel { get; set; }
    public int GearLevel { get; set; }
    public int Count { get; set; }
    public int Attack {
        get
        {
            // TODO: Add the percentage boosts for each type of gear to this
            // For simplicity this assumes that 
            var result = (TroopType, TroopLevel) switch
            {
                (TroopType.Hitter, 5) => 194,
                (TroopType.Pilot, 5) => 198,
                (TroopType.Shooter, 5) => 205,
                (TroopType.WallBreaker, 5) => 201,
            };

            result += GearLevel * 3;

            return result;
        }
    }

    public int Defence
    {
        get
        {
            // TODO: Add the percentage boosts for each type of gear to this
            // For simplicity this assumes that 
            var result = (TroopType, TroopLevel) switch
            {
                (TroopType.Hitter, 5) => 195,
                (TroopType.Pilot, 5) => 184,
                (TroopType.Shooter, 5) => 188,
                (TroopType.WallBreaker, 5) => 186,
            };

            result += GearLevel * 3;

            return result;
        }
    }

    public int Health { 
        get
        {
            // For simplicity this assumes that all weapons have to be the same level
            var result = (TroopType, TroopLevel) switch
            {
                (TroopType.Hitter, 5) => 178,
                (TroopType.Pilot, 5) => 185,
                (TroopType.Shooter, 5) => 175,
                (TroopType.WallBreaker, 5) => 180,
            };

            result += GearLevel;

            return result;
        }
    }

    public int GearAttackBoost
    {
        get
        {
            var result = (TroopType, TroopLevel) switch
            {
                (TroopType.Hitter, 5) => 10 + 18 + 12,
                (TroopType.Pilot, 5) => 16 + 10 + 12,
                (TroopType.Shooter, 5) => 17 + 12 + 11, 
                (TroopType.WallBreaker, 5) => 29 + 16 + 39,
            };

            return result;
        }
    }

    public int GearDefenceBoost
    {
        get
        {
            var result = (TroopType, TroopLevel) switch
            {
                (TroopType.Hitter, 5) => 13 + 23 + 11,
                (TroopType.Pilot, 5) => 16 + 13 + 11,
                (TroopType.Shooter, 5) => 12 + 13 + 15, // All shooter stats go up 1% per level
                (TroopType.WallBreaker, 5) => 13 + 26 + 39,
            };

            return result;
        }
    }

}