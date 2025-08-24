namespace FightSimulator.Core.Models;

public class Army
{
    public ArmyBoosts ArmyBoosts { get; set; } = new ArmyBoosts();
    public List<Troop> Troops { get; set; } = new List<Troop>();
    public FighterConfiguration FighterConfiguration { get; set; }
    public int RageLevel { get; set; }
    public int HealingResourceCost { get; set; }
    public int AlliesHealingResourceCost { get; set; }
    public int TroopReserveRemaining { get; set; }
    public int TotalTroopsCount => Troops.Sum(x => x.Count);
    public List<Troop> GarrisonTroops => Troops.Where(x => x.PlayerNumber == 0).ToList();
    public List<Troop> ReinforcingTroops => Troops.Where(x => x.PlayerNumber > 0).ToList();
    public int HospitalMax { get; set; }
    public int NumberOfPlayers => Troops.Select(x => x.PlayerNumber).Distinct().Count();
    public bool MainPlayerIsAlive => GarrisonTroops.Any(x => x.Count > 0 || x.RefreshRoundsLeft != null);
    public bool AnyPlayerIsAlive => Troops.Any(x => x.Count > 0 || x.RefreshRoundsLeft != null);
}