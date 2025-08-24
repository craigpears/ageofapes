namespace FightSimulator.Core.Models;

public class AttackLog
{
    public RoundLogData YourRoundLogData { get; set; } = new();
    public RoundLogData EnemyRoundLogData { get; set; } = new();

    public double YourNormalDamage => YourRoundLogData.NormalDamage;
    public double YourSkillDamage => YourRoundLogData.SkillDamage;
    public int EnemyLostTroops => EnemyRoundLogData.LostTroops;
    public int YourLostTroops => YourRoundLogData.LostTroops;
    
}