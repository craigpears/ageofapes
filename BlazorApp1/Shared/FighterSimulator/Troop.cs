namespace BlazorApp1.Shared.FighterSimulator;

public class Troop
{
    
    public void CalculateStats(ArmyBoosts armyBoosts, bool addCounterDamage)
    {
        var boosts = armyBoosts.UnitBoosts.SingleOrDefault(x => x.TroopType == TroopType);
        
        CalculatedAttack = Attack * ((boosts?.AttackBoostPercent ?? 1) / 100);
        CalculatedDefence = Defence * ((boosts?.DefenceBoostPercent ?? 1) / 100);
        CalculatedHealth = Health * ((boosts?.HealthBoostPercent ?? 1) / 100);
        CalculatedDamageBoost = addCounterDamage ? boosts?.Counter ?? 1 : 1;
    }
    
    public double CalculatedAttack { get; set; }
    public double CalculatedDefence { get; set; }
    public double CalculatedHealth { get; set; }
    public double CalculatedDamageBoost { get; set; }
    
    public TroopType TroopType { get; set; }
    public int TroopLevel { get; set; }
    public int GearLevel { get; set; }
    public int Count { get; set; }
    // How long until a retreating army returns with fresh numbers
    public int? RefreshRoundsLeft { get; set; }
    // So functions can treat defenders and reinforcements differently
    public int PlayerNumber { get; set; }
    // So upon refresh functions can work out how many troops would return from light injuries or can be healed
    public int LossesSinceLastRefresh { get; set; }
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