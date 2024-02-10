namespace BlazorApp1.Shared.FighterSimulator;

public class AttackLog
{
    public double AttackerDamageFactor { get; set; }
    public double DefenderDamageFactor { get; set; }
    public double AttackerDamage { get; set; }
    public double DefenderDamage { get; set; }
    public double AttackerTotalHealth { get; set; }
    public double DefenderTotalHealth { get; set; }
    public int DefenderLostTroops { get; set; }
    public int AttackerLostTroops { get; set; }
}