namespace BlazorApp1.Shared.FighterSimulator;

public class FightSimulationService
{
    public static int RageFromNormalAttack = 90;
    public static int RageFromCounterAttack = 45;
    
    
    public void SimulateFight(Army armyOne, Army armyTwo) // TODO: Change this into a set of parameters to support fighters that do better in 3v3 fights etc.
    {
        // TODO: Can start with attacking a city, simply dealing damage as the first simulation.  Would effectively be like Attack on Dr. Hogg
        // TODO: Simulate options with/without unit skills active
    }

    public AttackResult SimulateCityAttack(Army attackingArmy, Army defendingArmy, FightSimulationOptions options)
    {
        // TODO: Simulate sentry tower attack, health and protection, city walls etc.
        // TODO: Support simulating skills
        
        // TODO: Need to improve this code so it doesn't modify the original models if doing anything more complicated
        attackingArmy.ArmyBoosts.AddGearBoosts(attackingArmy.Troops);
        defendingArmy.ArmyBoosts.AddGearBoosts(defendingArmy.Troops);

        attackingArmy.Troops.ForEach(x => x.CalculateStats(attackingArmy.ArmyBoosts));
        defendingArmy.Troops.ForEach(x => x.CalculateStats(defendingArmy.ArmyBoosts));

        var cannonAttack = attackingArmy.Troops.All(x => x.TroopType == TroopType.WallBreaker) && options.UseCannons;

        var attackResult = new AttackResult
        {
            AttackingArmy = attackingArmy,
        };
        
        while (attackingArmy.Troops.Any(x => x.Count > 0) && defendingArmy.Troops.Any(x => x.Count > 0))
        {
            // TODO: Not worrying about counter unit type damage yet
            // TODO: No idea how things work if there are a mix of troops - evidence seems to show it's spread evenly by unit count since units rarely get wiped out
            // TODO: Not got troop level damage yet
            // TODO: Not thinking about normal vs counter attacks yet
            var log = new AttackLog
            {
                AttackerDamageFactor = attackingArmy.Troops.Average(x => x.CalculatedAttack) / defendingArmy.Troops.Average(x => x.CalculatedDefence),
                DefenderDamageFactor = defendingArmy.Troops.Average(x => x.CalculatedAttack) / attackingArmy.Troops.Average(x => x.CalculatedDefence)
            };

            log.AttackerDamage = log.AttackerDamageFactor * attackingArmy.Troops.Sum(x => x.Count);
            log.DefenderDamage = log.DefenderDamageFactor * defendingArmy.Troops.Sum(x => x.Count);

            log.AttackerTotalHealth = attackingArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            log.DefenderTotalHealth = defendingArmy.Troops.Average(x => x.CalculatedHealth * x.Count);
            
            // TODO: Not worrying about wounded yet
            log.DefenderLostTroops = ProcessArmyLosses(defendingArmy, log.AttackerDamage);
            if (!cannonAttack)
            {
                log.AttackerLostTroops = ProcessArmyLosses(attackingArmy, log.DefenderDamage);
            }
            
            attackResult.AttackLogs.Add(log);
        }

        return attackResult;
    }

    private static int ProcessArmyLosses(Army army, double damage)
    {
        // TODO: From brief tests in game it seems damage is evenly spread per unit, so need to research and improve this
        int totalLosses = 0;
        var damageRemaining = damage;
        foreach (var troop in army.Troops)
        {
            if (damageRemaining > 0)
            {
                var troopTotalHealth = troop.CalculatedHealth * troop.Count;

                var losses = (int)(damageRemaining / troop.CalculatedHealth);
                totalLosses += losses;
                troop.Count -= (int)losses;

                damageRemaining -= troopTotalHealth;
            }
        }

        return totalLosses;
    }
}

public class FightSimulationOptions
{
    public bool MapBattle { get; set; }
    public bool Seige { get; set; }
    public bool UseCannons { get; set; }
}