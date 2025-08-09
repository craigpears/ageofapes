using FightSimulator.Core.Services;

namespace FightSimulator.Core;

public class AttackResult
{
    public List<AttackLog> AttackLogs { get; set; } = new();
    public Army YourArmy { get; set; }
    public Army EnemyArmy { get; set; }
    public FightSimulationOptions FightOptions { get; set; }

    public int NumberOfRounds => AttackLogs.Count;
    public int HighestYourDamage => (int)AttackLogs.Max(a => a.YourNormalDamage);
    public int HighestYourSkillDamage => (int)AttackLogs.Max(a => a.YourSkillDamage);
    public int TotalEnemyLostTroops => AttackLogs.Sum(x => x.EnemyLostTroops);
    public int TotalYourLostTroops => AttackLogs.Sum(x => x.YourLostTroops);
    public int YourRemainingTroops => YourArmy.TotalTroopsCount;
    public int EnemyRemainingTroops => EnemyArmy.TotalTroopsCount;
    public double KillRatio => TotalEnemyLostTroops / (double)TotalYourLostTroops;
}
