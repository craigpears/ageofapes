namespace BlazorApp1.Shared.FighterSimulator;

public class AttackResult
{
    public List<AttackLog> AttackLogs { get; set; } = new();
    public Army AttackingArmy { get; set; }
}