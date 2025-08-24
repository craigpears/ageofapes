using FightSimulator.Core.Models;

namespace FightSimulator.Core.Repositories;

public interface ITalentCombinationsRepository
{
    List<List<Talent>> GetCachedCombinations(string cacheKey);
    void SaveCombinations(string cacheKey, List<List<Talent>> combinations);
    List<List<Talent>> GetCachedTreeCombinations(string talentTreeName);
    void SaveTreeCombinations(string talentTreeName, List<List<Talent>> combinations);
    bool HasCachedCombinations(string cacheKey);
    bool HasCachedTreeCombinations(string talentTreeName);
}
