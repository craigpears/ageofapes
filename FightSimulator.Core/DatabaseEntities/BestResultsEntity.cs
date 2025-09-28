using System.ComponentModel.DataAnnotations;

namespace FightSimulator.Core.DatabaseEntities;

public class BestResultsEntity
{
    [Key]
    public string FileName { get; set; } = string.Empty;
    
    [Key]
    public string OutputPath { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastModifiedDate { get; set; }
}
