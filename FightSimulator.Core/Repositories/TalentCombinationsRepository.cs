using System.Text.Json;
using System.Text.Json.Serialization;
using FightSimulator.Core.Models;

namespace FightSimulator.Core.Repositories;

public class TalentCombinationsRepository : ITalentCombinationsRepository
{
    private readonly string _cacheFilesDirectory;
    private readonly Dictionary<string, List<List<Talent>>> _talentTreeCombinationsCache = new();
    private readonly Dictionary<string, List<List<Talent>>> _allCombinationsCache = new();
    private readonly object _cacheLock = new();
    private const int MaxCacheSize = 10;

    public TalentCombinationsRepository(string cacheFilesDirectory)
    {
        _cacheFilesDirectory = cacheFilesDirectory;
    }

    public List<List<Talent>> GetCachedCombinations(string cacheKey)
    {
        if (_allCombinationsCache.ContainsKey(cacheKey))
        {
            return _allCombinationsCache[cacheKey];
        }

        var cacheFileName = $"{_cacheFilesDirectory}/{cacheKey}Combinations.json";
        
        if (File.Exists(cacheFileName))
        {
            var cachedJson = File.ReadAllText(cacheFileName);
            var cachedCombinations = JsonSerializer.Deserialize<List<List<Talent>>>(cachedJson);
            
            if (_allCombinationsCache.Count < MaxCacheSize)
                _allCombinationsCache[cacheKey] = cachedCombinations;
            
            return cachedCombinations;
        }

        return null;
    }

    public void SaveCombinations(string cacheKey, List<List<Talent>> combinations)
    {
        var cacheFileName = $"{_cacheFilesDirectory}/{cacheKey}Combinations.json";
        
        var json = JsonSerializer.Serialize(combinations, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
        File.WriteAllText(cacheFileName, json);
        
        if (_allCombinationsCache.Count < MaxCacheSize)
            _allCombinationsCache[cacheKey] = combinations;
    }

    public List<List<Talent>> GetCachedTreeCombinations(string talentTreeName)
    {
        if (_talentTreeCombinationsCache.ContainsKey(talentTreeName))
        {
            return _talentTreeCombinationsCache[talentTreeName];
        }

        var cacheFileName = $"{_cacheFilesDirectory}/{talentTreeName}.json";
        
        if (File.Exists(cacheFileName))
        {
            var cachedJson = File.ReadAllText(cacheFileName);
            var cachedCombinations = JsonSerializer.Deserialize<List<List<Talent>>>(cachedJson);
            _talentTreeCombinationsCache[talentTreeName] = cachedCombinations;
            return cachedCombinations;
        }

        return null;
    }

    public void SaveTreeCombinations(string talentTreeName, List<List<Talent>> combinations)
    {
        _talentTreeCombinationsCache[talentTreeName] = combinations;
        
        // Remove these large links before serialising, they aren't needed anymore
        var combinationsForSerialization = combinations.Select(x => x.Select(t =>
        {
            var talentCopy = new Talent
            {
                TalentId = t.TalentId,
                TalentTreeName = t.TalentTreeName,
                Optional = t.Optional,
                Boosts = t.Boosts
            };
            return talentCopy;
        }).ToList()).ToList();
        
        lock (_cacheLock)
        {
            var cacheFileName = $"{_cacheFilesDirectory}/{talentTreeName}.json";
            var json = JsonSerializer.Serialize(combinationsForSerialization, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
            File.WriteAllText(cacheFileName, json);
        }
    }

    public bool HasCachedCombinations(string cacheKey)
    {
        if (_allCombinationsCache.ContainsKey(cacheKey))
            return true;

        var cacheFileName = $"{_cacheFilesDirectory}/{cacheKey}Combinations.json";
        return File.Exists(cacheFileName);
    }

    public bool HasCachedTreeCombinations(string talentTreeName)
    {
        if (_talentTreeCombinationsCache.ContainsKey(talentTreeName))
            return true;

        var cacheFileName = $"{_cacheFilesDirectory}/{talentTreeName}.json";
        return File.Exists(cacheFileName);
    }
}
