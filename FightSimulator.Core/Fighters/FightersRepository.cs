using FightSimulator.Core.Fighters.Gatherers;
using FightSimulator.Core.Fighters.Leaders;
using FightSimulator.Core.Fighters.Pilots;
using FightSimulator.Core.Fighters.Shooters;
using FightSimulator.Core.Models;

namespace FightSimulator.Core.Fighters;

public class FightersRepository
{
    public List<Fighter> GetFighters(RunOptions options)
    {
        var fighters = new List<Fighter>();
        
        // TODO: Add todos for fighters not yet unlocked

        // Leaders
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
        
        // Gatherers
        fighters.Add(Laurent.GetFighter());
        fighters.Add(Remy.GetFighter());
        fighters.Add(Dempsey.GetFighter());
        fighters.Add(Vance.GetFighter());
        
        // Pilots
        fighters.Add(Elmo.GetFighter());
        fighters.Add(Dave.GetFighter());
        fighters.Add(Cal.GetFighter());
        // TODO: Tiffany
        fighters.Add(Oscar.GetFighter());
        fighters.Add(TNY.GetFighter());
        // TODO: Maverick
        fighters.Add(Darcy.GetFighter());
        
        // Hitters
        // TODO: Fiona
        // TODO: Bazell
        // TODO: Electro Jack
        // TODO: Hardy
        // TODO: Aldrich
        // TODO: Bruce
        // TODO: Alexis
        // TODO: Scott
        // TODO: Rony Jaa

        // Shooters
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

        var includedFighters = fighters.Where(f =>
            f.TalentSkills.Any(x => x.TalentTree.TalentTreeName == "Leader" && options.IncludeLeaders) ||
            f.TalentSkills.Any(x => x.TalentTree.TalentTreeName == "Gatherer" && options.IncludeGatherers) ||
            f.TalentSkills.Any(x => x.TalentTree.TalentTreeName == "Hunter" && options.IncludeHunters) ||
            f.TalentSkills.Any(x => x.TalentTree.TalentTreeName == "Pilot" && options.IncludePilots) ||
            f.TalentSkills.Any(x => x.TalentTree.TalentTreeName == "Hitter" && options.IncludeHitters) ||
            f.TalentSkills.Any(x => x.TalentTree.TalentTreeName == "Shooter" && options.IncludeShooters)
        ).ToList();

        return includedFighters;
    }
}