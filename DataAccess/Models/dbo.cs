using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class dbo : DbContext
{
    public dbo()
    {
    }

    public dbo(DbContextOptions<dbo> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Flexworker> Flexworkers { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__category__3213E83FEEE1023E");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Flexworker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__flexwork__3213E83F15FF6BAD");

            entity.ToTable("flexworkers");

            entity.HasIndex(e => e.Email, "UQ__flexwork__AB6E616430E988A5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .HasColumnName("adress");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(255)
                .HasColumnName("profile_picture_url");

            entity.HasMany(d => d.Skills).WithMany(p => p.Flexworkers)
                .UsingEntity<Dictionary<string, object>>(
                    "SkillsFlexworker",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillsId")
                        .HasConstraintName("FK__skills_fl__skill__38996AB5"),
                    l => l.HasOne<Flexworker>().WithMany()
                        .HasForeignKey("FlexworkersId")
                        .HasConstraintName("FK__skills_fl__flexw__37A5467C"),
                    j =>
                    {
                        j.HasKey("FlexworkersId", "SkillsId").HasName("PK__skills_f__DB44A7030EB3489D");
                        j.ToTable("skills_flexworkers");
                        j.IndexerProperty<int>("FlexworkersId").HasColumnName("flexworkers_id");
                        j.IndexerProperty<int>("SkillsId").HasColumnName("skills_id");
                    });
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jobs__3213E83F5D83D074");

            entity.ToTable("jobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .HasColumnName("adress");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.MaxHours).HasColumnName("max_hours");
            entity.Property(e => e.MinHours).HasColumnName("min_hours");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasMany(d => d.Flexworkers).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobsFlexworker",
                    r => r.HasOne<Flexworker>().WithMany()
                        .HasForeignKey("FlexworkersId")
                        .HasConstraintName("FK__jobs_flex__flexw__30F848ED"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobsId")
                        .HasConstraintName("FK__jobs_flex__jobs___300424B4"),
                    j =>
                    {
                        j.HasKey("JobsId", "FlexworkersId").HasName("PK__jobs_fle__214806B336373FA9");
                        j.ToTable("jobs_flexworkers");
                        j.IndexerProperty<int>("JobsId").HasColumnName("jobs_id");
                        j.IndexerProperty<int>("FlexworkersId").HasColumnName("flexworkers_id");
                    });

            entity.HasMany(d => d.Skills).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "SkillsJob",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillsId")
                        .HasConstraintName("FK__skills_jo__skill__34C8D9D1"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobsId")
                        .HasConstraintName("FK__skills_jo__jobs___33D4B598"),
                    j =>
                    {
                        j.HasKey("JobsId", "SkillsId").HasName("PK__skills_j__8032DA9FDB0FFD49");
                        j.ToTable("skills_jobs");
                        j.IndexerProperty<int>("JobsId").HasColumnName("jobs_id");
                        j.IndexerProperty<int>("SkillsId").HasColumnName("skills_id");
                    });
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skills__3213E83FEE47368D");

            entity.ToTable("skills");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Skills)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__skills__category__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FDD325EC9");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164108D35A4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}