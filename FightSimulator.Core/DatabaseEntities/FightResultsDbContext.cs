using Microsoft.EntityFrameworkCore;

namespace FightSimulator.Core.DatabaseEntities;

public class FightResultsDbContext : DbContext
{
    public FightResultsDbContext(DbContextOptions<FightResultsDbContext> options) : base(options)
    {
    }

    public DbSet<BestResultsEntity> BestResults { get; set; }
    public DbSet<FighterResultsEntity> FighterResults { get; set; }
    public DbSet<BestConfigsEntity> BestConfigs { get; set; }
    public DbSet<FightResultEntity> FightResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure composite primary keys
        modelBuilder.Entity<BestResultsEntity>()
            .HasKey(e => new { e.FileName, e.OutputPath });

        modelBuilder.Entity<FighterResultsEntity>()
            .HasKey(e => new { e.FileName, e.OutputPath });

        modelBuilder.Entity<BestConfigsEntity>()
            .HasKey(e => new { e.FileName, e.OutputPath });

        // Configure indexes for better query performance
        modelBuilder.Entity<BestResultsEntity>()
            .HasIndex(e => e.CreatedDate);

        modelBuilder.Entity<FighterResultsEntity>()
            .HasIndex(e => e.CreatedDate);

        modelBuilder.Entity<BestConfigsEntity>()
            .HasIndex(e => e.CreatedDate);

        modelBuilder.Entity<FightResultEntity>()
            .HasIndex(e => e.FighterName);
        
        modelBuilder.Entity<FightResultEntity>()
            .HasIndex(e => e.OutputPath);
        
        modelBuilder.Entity<FightResultEntity>()
            .HasIndex(e => e.CreatedDate);
        
        modelBuilder.Entity<FightResultEntity>()
            .HasIndex(e => new { e.FighterName, e.OutputPath });
    }
}
