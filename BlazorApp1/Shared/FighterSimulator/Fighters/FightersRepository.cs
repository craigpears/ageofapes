using BlazorApp1.Shared.FighterSimulator.Fighters;
using BlazorApp1.Shared.FighterSimulator.Fighters.Shooters;

namespace BlazorApp1.Shared.FighterSimulator;

public class FightersRepository
{
    public List<Fighter> GetFighters()
    {
        var fighters = new List<Fighter>();

        // Leaders
        fighters.Add(Derrick.GetFighter());
        fighters.Add(Maximus.GetFighter());
        
        // Gatherers
        fighters.Add(Laurent.GetFighter());
        fighters.Add(Remy.GetFighter());
        
        // Pilots
        fighters.Add(Elmo.GetFighter());
        fighters.Add(Dave.GetFighter());
        
        // Shooters
        fighters.Add(BonoBoom.GetFighter());
        fighters.Add(Carina.GetFighter());

        return fighters;
    }
}