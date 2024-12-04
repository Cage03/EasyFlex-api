using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class EasyFlexContext : DbContext
{
    public EasyFlexContext()
    {
    }

    public EasyFlexContext(DbContextOptions<EasyFlexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryModel> Categories { get; set; }

    public virtual DbSet<FlexworkerModel> Flexworkers { get; set; }

    public virtual DbSet<JobModel> Jobs { get; set; }

    public virtual DbSet<SkillModel> Skills { get; set; }

    public virtual DbSet<PreferenceModel> Preferences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FlexworkerModel>()
            .HasMany(f => f.Skills)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "FlexworkerSkill",
                j => j.HasOne<SkillModel>().WithMany().HasForeignKey("SkillsId"),
                j => j.HasOne<FlexworkerModel>().WithMany().HasForeignKey("FlexworkersId"));
        
        modelBuilder.Entity<PreferenceModel>()
            .HasOne(p => p.Job) 
            .WithMany(j => j.Preferences) 
            .HasForeignKey(p => p.JobId); 
        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}