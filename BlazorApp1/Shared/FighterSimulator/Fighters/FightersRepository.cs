using BlazorApp1.Shared.FighterSimulator.Fighters;

namespace BlazorApp1.Shared.FighterSimulator;

public class FightersRepository
{
    public List<Fighter> GetFighters()
    {
        var fighters = new List<Fighter>();

        fighters.Add(Derrick.GetFighter());
        fighters.Add(Laurent.GetFighter());

        return fighters;
    }
}