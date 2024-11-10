using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Amin.Data;

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

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__99FC143B4CE8B184");

            entity.Property(e => e.CommentId).HasColumnName("Comment_ID");
            entity.Property(e => e.CommentAuthorId).HasColumnName("Comment_Author_ID");
            entity.Property(e => e.CommentContent)
                .HasColumnType("ntext")
                .HasColumnName("Comment_Content");
            entity.Property(e => e.CommentDate)
                .HasColumnType("datetime")
                .HasColumnName("Comment_Date");
            entity.Property(e => e.PostId).HasColumnName("Post_ID");

            entity.HasOne(d => d.CommentAuthor).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentAuthorId)
                .HasConstraintName("FK_Comments_AuthorID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_PostID");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificateId).HasName("PK__Notifica__14707F342513C431");

            entity.Property(e => e.NotificateId).HasColumnName("NotificateID");
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.SendDay).HasColumnName("Send_Day");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<PatientInformation>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Patient___603A0C60E46CD560");

            entity.ToTable("Patient_Informations");

            entity.Property(e => e.RecordId).HasColumnName("Record_ID");
            entity.Property(e => e.CaffeineIntake)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("Caffeine_intake");
            entity.Property(e => e.PhysicalActivityDuration).HasColumnName("Physical_Activity_Duration");
            entity.Property(e => e.SleepTime).HasColumnName("Sleep_Time");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.WakeTime).HasColumnName("Wake_Time");

            entity.HasOne(d => d.User).WithMany(p => p.PatientInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientInfo_UserID");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__5875F74D65BE3C04");

            entity.Property(e => e.PostId).HasColumnName("Post_ID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(255)
                .HasColumnName("Author_Name");
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.PostImageData).HasColumnName("Post_Image_Data");
            entity.Property(e => e.PostImageId)
                .HasMaxLength(255)
                .HasColumnName("Post_Image_ID");
            entity.Property(e => e.PostedDate)
                .HasColumnType("datetime")
                .HasColumnName("Posted_Date");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__206D91904F968F0E");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E49A7B7A13").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AlcoholicStatus).HasColumnName("Alcoholic_status");
            entity.Property(e => e.Bmi)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("BMI");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("Full_Name");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.SmokingStatus).HasColumnName("Smoking_status");
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.YearOfBirth).HasColumnName("Year_Of_Birth");

            entity.HasMany(d => d.Posts).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "TruyCap",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TruyCap_PostID"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TruyCap_UserID"),
                    j =>
                    {
                        j.HasKey("UserId", "PostId").HasName("PK__Truy_Cap__E5EACEE4E8E8D7B5");
                        j.ToTable("Truy_Cap");
                        j.IndexerProperty<int>("UserId").HasColumnName("User_ID");
                        j.IndexerProperty<int>("PostId").HasColumnName("Post_ID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
