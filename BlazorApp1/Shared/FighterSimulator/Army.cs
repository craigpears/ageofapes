namespace BlazorApp1.Shared.FighterSimulator;

public class Army
{
    public ArmyBoosts ArmyBoosts { get; set; }
    public List<Troop> Troops { get; set; }
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
}