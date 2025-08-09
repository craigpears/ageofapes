using FightSimulator.Core.FighterSimulator.Fighters.Gatherers;
using FightSimulator.Core.FighterSimulator.Fighters.Leaders;
using FightSimulator.Core.FighterSimulator.Fighters.Pilots;
using FightSimulator.Core.FighterSimulator.Fighters.Shooters;

namespace FightSimulator.Core.FighterSimulator.Fighters;

public class FightersRepository
{
    public List<Fighter> GetFighters(RunOptions options)
    {
        var fighters = new List<Fighter>();
        
        // TODO: Add todos for fighters not yet unlocked

        // Leaders
        if (options.IncludeLeaders)
        {
            fighters.Add(Derrick.GetFighter());
            fighters.Add(Maximus.GetFighter());
            fighters.Add(Rodruigez.GetFighter());
            // TODO: Mastodonte
            // TODO: Ken
            // TODO: Joey Jester
            // TODO: Tassia
            // TODO: Sergeant Buzz
            // TODO: Baby Cosmo
            // TODO: Greg
            // TODO: Fuscata
            // TODO: Korutopi
            // TODO: Rams
        }
        
        // Gatherers
        if (options.IncludeGatherers)
        {
            fighters.Add(Laurent.GetFighter());
            fighters.Add(Remy.GetFighter());
            fighters.Add(Dempsey.GetFighter());
            fighters.Add(Vance.GetFighter());
        }
        
        // Pilots
        if (options.IncludePilots)
        {
            fighters.Add(Elmo.GetFighter());
            fighters.Add(Dave.GetFighter());
            fighters.Add(Cal.GetFighter());
            // TODO: Tiffany
            fighters.Add(Oscar.GetFighter());
            // TODO: Tny
            // TODO: Maverick
            fighters.Add(Darcy.GetFighter());
        }
        
        // Hitters
        if (options.IncludeHitters)
        {
            // TODO: Fiona
            // TODO: Bazell
            // TODO: Electro Jack
            // TODO: Hardy
            // TODO: Aldrich
            // TODO: Bruce
            // TODO: Alexis
            // TODO: Scott
            // TODO: Rony Jaa
        }

        // Shooters
        if (options.IncludeShooters)
        {
            fighters.Add(BonoBoom.GetFighter());
            fighters.Add(Carina.GetFighter());
            fighters.Add(ClarkBrothers.GetFighter());
            fighters.Add(Thor.GetFighter());
            fighters.Add(Raymond.GetFighter());
            // TODO: Duke Sean
            // TODO: Louise Armstrong
            // TODO: Mike
            // TODO: Genesis
            // TODO: Len
        }

        return fighters;
    }
}