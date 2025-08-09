namespace FightSimulator.Core.FighterSimulator;

public class AttackLog
{
    public RoundLogData YourRoundLogData { get; set; }
    public RoundLogData EnemyRoundLogData { get; set; }

    public double YourNormalDamage => YourRoundLogData.NormalDamage;
    public double YourSkillDamage => YourRoundLogData.SkillDamage;
    public int EnemyLostTroops => EnemyRoundLogData.LostTroops;
    public int YourLostTroops => YourRoundLogData.LostTroops;
    
}