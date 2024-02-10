using BlazorApp1.Shared.FighterSimulator;
using Newtonsoft.Json;
using ScoutingParser;

namespace Tests;

public class FighterSimulatorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldDoStuff()
    {
        /*
         *
         * Cayicep stats are +350% for his hitters, +300% for defence of hitters.  Other stats are quite small, like +45% counter to pilots and +17.5% normal damage
         */
        var repo = new FightersRepository();
        var statsService = new FighterStatsService();
        var fightSimulationService = new FightSimulationService();
        var fightOptions = new FightSimulationOptions()
        {
            Seige = true,
            UseCannons = true
        };

        var fighters = repo.GetFighters();
        var results = new List<AttackResult>();
        foreach (var fighter in fighters)
        {
            var configurations = statsService.GetConfigurationsForFighter(fighter, fightOptions);
            foreach (var configuration in configurations)
            {
                var army = new Army
                {
                    ArmyBoosts = configuration.ArmyBoosts, 
                    FighterConfiguration = configuration,
                    Troops = new List<Troop>
                    {
                        new() { Count = 100000, TroopType = TroopType.WallBreaker, GearLevel = 5, TroopLevel = 5 }
                    }
                };
                
                var defendingArmy = new Army
                {
                    ArmyBoosts = new ArmyBoosts(),
                    Troops = new List<Troop>
                    {
                        new() { TroopType = TroopType.Hitter, Count = 250000, GearLevel = 5, TroopLevel = 5 },
                        new() { TroopType = TroopType.Pilot, Count = 250000, GearLevel = 5, TroopLevel = 5 },
                        new() { TroopType = TroopType.Shooter, Count = 250000, GearLevel = 5, TroopLevel = 5 },
                    }
                };
                
                var attackResult = fightSimulationService.SimulateCityAttack(army, defendingArmy, fightOptions);
                results.Add(attackResult);
            }
            
        }


        var resultsCsv = results.Select(x =>
            $"{x.AttackingArmy.FighterConfiguration.TalentPointCount}\t{x.AttackLogs.Max(a => a.AttackerDamage)}\t{x.AttackLogs.First().DefenderLostTroops}");
        
        /*
           \t{JsonConvert.SerializeObject(x.AttackingArmy.FighterConfiguration, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            })}
         */
        var resultsFile = string.Join("\r\n", resultsCsv);

     }
}