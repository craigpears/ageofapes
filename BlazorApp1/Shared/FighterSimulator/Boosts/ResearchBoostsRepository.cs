using System.Runtime.InteropServices.ComTypes;

namespace BlazorApp1.Shared.FighterSimulator.Boosts;

public class ResearchBoostsRepository
{
    public List<Boost> GetBoosts()
    {
        var boosts = new List<Boost>();
        
        // TODO: Add research boosts
        
        #region military
        
        
        #endregion
        
        #region batteries
        
        #endregion
        
        #region gang
        
        #endregion

        boosts.ForEach(x => x.Source = "Research");
        return boosts;
    }
}