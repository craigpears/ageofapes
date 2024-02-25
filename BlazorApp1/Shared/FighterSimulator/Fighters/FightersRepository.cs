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
        
        // Shooters
        fighters.Add(BonoBoom.GetFighter());
        fighters.Add(Carina.GetFighter());
        //fighters.Add(Genesis.GetFighter());
        //fighters.Add(Len.GetFighter());
        //fighters.Add(Mike.GetFighter());
        

        return fighters;
    }
}