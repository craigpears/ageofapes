using FightSimulator.Core.Fighters;
using FightSimulator.Core.Services;
using FightSimulator.Core.Repositories;

namespace FightSimulator.Core.Scenarios;

public abstract class FightScenario
{
    public string outputFolder;
    protected double EnemyBoostMultiplier = 1.0;
    protected readonly IFightResultsRepository _fightResultsRepository;
        
    public FightSimulationOptions FightSimulationOptions { get; set; }

    public FightScenario(string outputFolder, FightSimulationOptions options, IFightResultsRepository fightResultsRepository)
    {
        this.outputFolder = outputFolder;
        this.FightSimulationOptions = options;
        this._fightResultsRepository = fightResultsRepository;
    }
    
    protected new List<UnitBoosts> DefaultEnemyBoosts => GetBoosts(EnemyBoostMultiplier);

    public RunOptions RunOptions { get; set; } = new RunOptions
    {
        IncludePilots = true,
        IncludeHitters = true,
        IncludeShooters = true,
        IncludeLeaders = true,
        IncludeGatherers = true
    };

    protected new List<UnitBoosts> GetBoosts(double multiplier) => new List<UnitBoosts>
    {
        new UnitBoosts 
            { AttackBoostPercent = 60 * multiplier, DefenceBoostPercent = 40 * multiplier, TroopType = TroopType.Pilot },
        new UnitBoosts
            { AttackBoostPercent = 40 * multiplier, DefenceBoostPercent = 60 * multiplier, TroopType = TroopType.Hitter },
        new UnitBoosts
            { AttackBoostPercent = 60 * multiplier, DefenceBoostPercent = 40 * multiplier, TroopType = TroopType.Shooter }
    };

    public DateTime? GetLastRanDate(string? prefix = null) => 
        _fightResultsRepository.GetLastRanDate(outputFolder, prefix);
    
    public virtual Func<Army, Army, Army> EnemyArmyFunc(FighterConfiguration configuration)=>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            // TODO: Defending army should have a fighter so reduced damage from skills gives a benefit
            ArmyBoosts = new ArmyBoosts
            {
                UnitBoosts = DefaultEnemyBoosts
            },
            Troops = new List<Troop>
            {
                new() { TroopType = TroopType.Hitter, Count = 25000, GearLevel = 5, TroopLevel = 5 },
                new() { TroopType = TroopType.Pilot, Count = 25000, GearLevel = 5, TroopLevel = 5 },
                new() { TroopType = TroopType.Shooter, Count = 25000, GearLevel = 5, TroopLevel = 5 },
            }
        };
    
    public virtual Func<Army, Army, Army> YourArmyFunc(FighterConfiguration configuration) =>
        (Army currentArmy, Army enemyArmy) => new Army
        {
            ArmyBoosts = configuration.ArmyBoosts,
            FighterConfiguration = configuration,
            Troops = configuration.PreferredTroopType == TroopType.ThreeUnitTypes
                ? new List<Troop>
                {
                    new()
                    {
                        Count = (int)(140000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = TroopType.Pilot, GearLevel = 5, TroopLevel = 5
                    },
                    new()
                    {
                        Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = TroopType.Hitter, GearLevel = 5, TroopLevel = 5
                    },
                    new()
                    {
                        Count = (int)(5000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = TroopType.Shooter, GearLevel = 5, TroopLevel = 5
                    }
                }
                : new List<Troop>
                {
                    new Troop()
                    {
                        Count = (int)(150000 * configuration.ArmyBoosts.MaxTroopsMultiplier),
                        TroopType = configuration.PreferredTroopType, GearLevel = 5, TroopLevel = 5
                    }
                }
        };

    public void FlushResults()
    {
        _fightResultsRepository.FlushResults(outputFolder);
    }
    
    public void SaveResults(List<AttackResult> results)
    {
        _fightResultsRepository.SaveResults(results, outputFolder);
    }
}