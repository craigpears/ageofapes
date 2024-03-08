namespace BlazorApp1.Shared.FighterSimulator;

public class AttackLog
{
    public double YourDamageFactor { get; set; }
    public double EnemyDamageFactor { get; set; }
    public double YourDamage { get; set; }
    public double EnemyDamage { get; set; }
    public double YourTotalHealth { get; set; }
    public double EnemyTotalHealth { get; set; }
    public int EnemyLostTroops { get; set; }
    public int YourLostTroops { get; set; }
    public double YourSkillDamage { get; set; }
}