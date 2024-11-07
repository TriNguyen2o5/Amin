using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Amin.Models;

public partial class PatientManagementContext : DbContext
{
    public PatientManagementContext()
    {
    }

    public PatientManagementContext(DbContextOptions<PatientManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PatientInformation> PatientInformations { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__99FC143BFF5063F1");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("Comment_ID");
            entity.Property(e => e.CommentAuthorId).HasColumnName("Comment_Author_ID");
            entity.Property(e => e.CommentContent).HasColumnName("Comment_Content");
            entity.Property(e => e.CommentDate).HasColumnName("Comment_Date");
            entity.Property(e => e.PostId).HasColumnName("Post_ID");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificateId).HasName("PK__Notifica__14707F346D5DC94B");

            entity.Property(e => e.NotificateId).HasColumnName("NotificateID");
            entity.Property(e => e.SendDay).HasColumnName("Send_Day");
        });

        modelBuilder.Entity<PatientInformation>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Patient___603A0C60C046FA76");

            entity.ToTable("Patient_Informations");

            entity.Property(e => e.RecordId).HasColumnName("Record_ID");
            entity.Property(e => e.CaffeineIntake).HasColumnName("Caffeine_intake");
            entity.Property(e => e.PhysicalActivityDuration).HasColumnName("Physical_Activity_Duration");
            entity.Property(e => e.SleepTime).HasColumnName("Sleep_Time");
            entity.Property(e => e.WakeTime).HasColumnName("Wake_Time");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__5875F74DC1FA7744");

            entity.Property(e => e.PostId).HasColumnName("Post_ID");
            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
            entity.Property(e => e.PostedDate).HasColumnName("Posted_Date");

            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__206D9190B887EE65");

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.AlcoholicStatus).HasColumnName("Alcoholic_status");
            entity.Property(e => e.Bmi).HasColumnName("BMI");
            entity.Property(e => e.FullName).HasColumnName("Full_Name");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SmokingStatus).HasColumnName("Smoking_status");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.YearOfBirth).HasColumnName("Year_Of_Birth");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
