using System.ComponentModel.DataAnnotations;

namespace FightSimulator.Core.DatabaseEntities;

public class FightResultEntity
{
    [Key]
    public int Id { get; set; }
    
    public string FighterName { get; set; } = string.Empty;
    
    public string? DeputyName { get; set; }
    
    public int? DeputySelectedTalent { get; set; }
    
    public string TalentBreakdown { get; set; } = string.Empty;
    
    public int NumberOfRounds { get; set; }
    
    public int HighestYourDamage { get; set; }
    
    public int HighestYourSkillDamage { get; set; }
    
    public int TotalNormalDamage { get; set; }
    
    public int TotalSkillDamage { get; set; }
    
    public int EnemyLosses { get; set; }
    
    public int YourLosses { get; set; }
    
    public int YourRemainingTroops { get; set; }
    
    public int EnemyRemainingTroops { get; set; }
    
    public double KillRatio { get; set; }
    
    public string OutputPath { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastModifiedDate { get; set; }
    
    // JSON content for detailed configuration data
    public string? ConfigurationJson { get; set; }
}
