using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollee> Enrollees { get; set; }

    public virtual DbSet<EnrolleeAchievement> EnrolleeAchievements { get; set; }

    public virtual DbSet<EnrolleeSubject> EnrolleeSubjects { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramEnrollee> ProgramEnrollees { get; set; }

    public virtual DbSet<ProgramSubject> ProgramSubjects { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=test");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.AchievementId).HasName("achievement_pk");

            entity.ToTable("achievement", "pcs");

            entity.Property(e => e.AchievementId)
                .ValueGeneratedNever()
                .HasColumnName("achievement_id");
            entity.Property(e => e.Bonus).HasColumnName("bonus");
            entity.Property(e => e.NameAchievement)
                .HasColumnType("character varying")
                .HasColumnName("name_achievement");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pk");

            entity.ToTable("department", "pcs");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("department_id");
            entity.Property(e => e.NameDepartment)
                .HasColumnType("character varying")
                .HasColumnName("name_department");
        });

        modelBuilder.Entity<Enrollee>(entity =>
        {
            entity.HasKey(e => e.EnrolleeId).HasName("enrollee_pk");

            entity.ToTable("enrollee", "pcs");

            entity.Property(e => e.EnrolleeId)
                .ValueGeneratedNever()
                .HasColumnName("enrollee_id");
            entity.Property(e => e.NameEnrollee)
                .HasColumnType("character varying")
                .HasColumnName("name_enrollee");
        });

        modelBuilder.Entity<EnrolleeAchievement>(entity =>
        {
            entity.HasKey(e => e.EnrolleeAchivId).HasName("enrollee_achievement_pk");

            entity.ToTable("enrollee_achievement", "pcs");

            entity.Property(e => e.EnrolleeAchivId)
                .ValueGeneratedNever()
                .HasColumnName("enrollee_achiv_id");
            entity.Property(e => e.AchievementId).HasColumnName("achievement_id");
            entity.Property(e => e.EnrolleeId).HasColumnName("enrollee_id");

            entity.HasOne(d => d.Achievement).WithMany(p => p.EnrolleeAchievements)
                .HasForeignKey(d => d.AchievementId)
                .HasConstraintName("enrollee_achievement_achievement_fk");

            entity.HasOne(d => d.Enrollee).WithMany(p => p.EnrolleeAchievements)
                .HasForeignKey(d => d.EnrolleeId)
                .HasConstraintName("enrollee_achievement_enrollee_fk");
        });

        modelBuilder.Entity<EnrolleeSubject>(entity =>
        {
            entity.HasKey(e => e.EnrolleeSubjectId).HasName("enrollee_subject_pk");

            entity.ToTable("enrollee_subject", "pcs");

            entity.Property(e => e.EnrolleeSubjectId)
                .ValueGeneratedNever()
                .HasColumnName("enrollee_subject_id");
            entity.Property(e => e.EnrolleeId).HasColumnName("enrollee_id");
            entity.Property(e => e.Result).HasColumnName("result");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Enrollee).WithMany(p => p.EnrolleeSubjects)
                .HasForeignKey(d => d.EnrolleeId)
                .HasConstraintName("enrollee_subject_enrollee_fk");

            entity.HasOne(d => d.Subject).WithMany(p => p.EnrolleeSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("enrollee_subject_subject_fk");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("program_pk");

            entity.ToTable("program", "pcs");

            entity.Property(e => e.ProgramId)
                .ValueGeneratedNever()
                .HasColumnName("program_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.NameProgram)
                .HasColumnType("character varying")
                .HasColumnName("name_program");
            entity.Property(e => e.Plan).HasColumnName("plan");

            entity.HasOne(d => d.Department).WithMany(p => p.Programs)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("program_department_fk");
        });

        modelBuilder.Entity<ProgramEnrollee>(entity =>
        {
            entity.HasKey(e => e.ProgramEnrolleeId).HasName("program_enrollee_pk");

            entity.ToTable("program_enrollee", "pcs");

            entity.Property(e => e.ProgramEnrolleeId)
                .ValueGeneratedNever()
                .HasColumnName("program_enrollee_id");
            entity.Property(e => e.EnrolleeId).HasColumnName("enrollee_id");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");

            entity.HasOne(d => d.Enrollee).WithMany(p => p.ProgramEnrollees)
                .HasForeignKey(d => d.EnrolleeId)
                .HasConstraintName("program_enrollee_enrollee_fk");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramEnrollees)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("program_enrollee_program_fk");
        });

        modelBuilder.Entity<ProgramSubject>(entity =>
        {
            entity.HasKey(e => e.ProgramSubjectId).HasName("program_subject_pk");

            entity.ToTable("program_subject", "pcs");

            entity.Property(e => e.ProgramSubjectId)
                .ValueGeneratedNever()
                .HasColumnName("program_subject_id");
            entity.Property(e => e.MinResult).HasColumnName("min_result");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramSubjects)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("program_subject_program_fk");

            entity.HasOne(d => d.Subject).WithMany(p => p.ProgramSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("program_subject_subject_fk");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("subject_pk");

            entity.ToTable("subject", "pcs");

            entity.Property(e => e.SubjectId)
                .ValueGeneratedNever()
                .HasColumnName("subject_id");
            entity.Property(e => e.NameSubject)
                .HasColumnType("character varying")
                .HasColumnName("name_subject");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
