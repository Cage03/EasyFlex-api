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

        modelBuilder.Entity<JobModel>()
            .HasMany(j => j.Skills)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "JobSkill",
                j => j.HasOne<SkillModel>().WithMany().HasForeignKey("SkillsId"),
                j => j.HasOne<JobModel>().WithMany().HasForeignKey("JobsId"));

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}