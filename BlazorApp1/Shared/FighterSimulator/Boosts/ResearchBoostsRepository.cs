using System.Runtime.InteropServices.ComTypes;

namespace BlazorApp1.Shared.FighterSimulator.Boosts;

public class ResearchBoostsRepository
{
    public List<Boost> GetBoosts()
    {
        var boosts = new List<Boost>();
        
        // TODO: Add research boosts
        // TODO: Create a fluent API for this
        // TODO: Add variables for max speed boosts at various levels like with or without battery research, title from overlord, planet boost etc

        var timeIncreaseBetweenLevels = 1.19;
        var resourcesRatio = 2.0;
        
        #region civic

        /*
         Technology A
         Lvl 1 = 5m 100/100
         
         Technology B
         Lvl 1 = 09m16s 5k/5k
         Lvl 2 = 13m50s 20k/10k
         Lvl 3 = 30m44s 40k/20k
         Lvl 4 = 1h23m 93.3k/40k
         
         Construction I
         Lvl 1 = 1h 40k/20k
         Lvl 2 = 1h30m 60k/30k
         Lvl 3 = 02h 100k/50k
         
         Technology C
         Lvl 1 = 15m 40k/20k
         Lvl 2 = 30m 80k/40k
         Lvl 3 = 01h 160k/80k
         Lvl 4 = 02h 320k/160k
         Lvl 5 = 03h34m 640k/320k
         */

        #endregion
        
        #region military
        
        /*
         Technology A
         Lvl 1 = 03s 5k/5k
         
        Technology B
        Lvl 1 = 55m34s 10k/10k
        Lvl 2 = 1h06m 20k/20k
        Lvl 3 = 1h19m 80k/40k
        Lvl 4 = 1h44m 160k/80k
        Lvl 5 = 2h04m 320k/160k

        Technology C - Level II units
        Lvl 1 = 10h 400k/200k

        Technology D
        Lvl 1 = 02h 30k/15k
        Lvl 2 = 02h48m 60k/30k
        Lvl 3 = 3h55m 120k/60kz
        Lvl 5 = 0740m 480k/240k
        
        Technology E
        Lvl 1 = 6h 80k/106.6k
        Lvl 2 = 08h24m 100k/213,3k

        Technology G
        Lvl 7 = 24d  11.3m/17.0m
        Lvl 9 = 192d 38.5m/57.7m

        Technology H
        Lvl 8 = 48d

        Technology K
        Lvl 9 = 51d05h 20.4m/30.7m

        Technology L
        Lvl 8 = 38d10h original time

        Technology N
        Lvl 8 = 57d14h original time
        */
        
        #endregion
        
        #region batteries
        /*
         
         Technology A
         Lvl 1 = 02m 100b
         Lvl 2 = 20m 200b
         Lvl 3 = 30m 300b
         
         */
        
        // Great Shape
        // Lvl 26 = 3.6k batteries 8h50m
        
        // Electric Baton
        // Lvl 21 = 1.7k batteries 4h10m
        
        // Antibiotics
        // Lvl 25 = 2.3k batteries 5h40m
        
        // Explosive shells same as antibiotics
        
        // Weak Point II 
        // Lvl 5 = 1.1k batteries and 2h50m
        
        // Brave and Fierce 
        // Lvl 21 = 2.2k 5h30m
        
        // Impenetrable Defence
        // Lvl 23 = 2.8k batteries and 07h original time
        
        // Nano Reinforced
        // Lvl 21 = 3.1k batteries and 7h40m
        
        // Beserk
        // Lvl 20 = 2.0k 7h20m
        
        #endregion
        
        #region gang
        
        #endregion

        boosts.ForEach(x => x.Source = "Research");
        return boosts;
    }
}