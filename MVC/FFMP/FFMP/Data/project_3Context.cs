using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FFMP.Data
{
    public partial class project_3Context : DbContext
    {
        public project_3Context()
        {
        }

        public project_3Context(DbContextOptions<project_3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditingForm> AuditingForms { get; set; } = null!;
        public virtual DbSet<AuditingLog> AuditingLogs { get; set; } = null!;
        public virtual DbSet<Inspection> Inspections { get; set; } = null!;
        public virtual DbSet<ObjectToCheck> Objects { get; set; } = null!;
        public virtual DbSet<Requirement> Requirements { get; set; } = null!;
        public virtual DbSet<RequirementResult> RequirementResults { get; set; } = null!;
        public virtual DbSet<TargetGroup> TargetGroups { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=project-3-mysql-server.mysql.database.azure.com;user id=stremberg_s;password=1Database1;database=project_3", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.39-mysql"), x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<AuditingForm>(entity =>
            {
                entity.HasKey(e => e.AuditingId)
                    .HasName("PRIMARY");

                entity.ToTable("auditing_form");

                entity.HasIndex(e => e.TargetGroupId, "fk_Auditing_Target_group1_idx");

                entity.HasIndex(e => e.UserLogin, "fk_Auditing_User1_idx");

                entity.Property(e => e.AuditingId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("auditing_id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(45)
                    .HasColumnName("user_login");

                entity.Property(e => e.TargetGroupId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("target_group_id");

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.AuditingForms)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Auditing_User1");

                entity.HasOne(d => d.TargetGroupIdNavigation)
                    .WithMany(p => p.AuditingForms)
                    .HasForeignKey(d => d.TargetGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Auditing_Target_group1");
            });

            modelBuilder.Entity<AuditingLog>(entity =>
            {
                entity.ToTable("auditing_logs");

                entity.HasIndex(e => e.ObjectId, "fk_Auditing_logs_Object1_idx");

                entity.HasIndex(e => e.UserLogin, "fk_Auditing_logs_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(45)
                    .HasColumnName("user_login");

                entity.Property(e => e.ObjectId)
                    .HasMaxLength(45)
                    .HasColumnName("object_id");

                entity.Property(e => e.Result)
                    .HasMaxLength(45)
                    .HasColumnName("result");

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.AuditingLogs)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Auditing_logs_User1");

                entity.HasOne(d => d.ObjectIdNavigation)
                    .WithMany(p => p.AuditingLogs)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Auditing_logs_Object1");
            });

            modelBuilder.Entity<Inspection>(entity =>
            {
                entity.HasKey(e => e.Timestamp)
                    .HasName("PRIMARY");

                entity.ToTable("inspection");

                entity.HasIndex(e => e.ObjectId, "fk_Inspection_Object1_idx");

                entity.HasIndex(e => e.UserLogin, "fk_Inspection_User1_idx");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasColumnName("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ChangeOfState).HasColumnName("change_of_state");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(45)
                    .HasColumnName("user_login");

                entity.Property(e => e.ObjectId)
                    .HasMaxLength(45)
                    .HasColumnName("Object_id");

                entity.Property(e => e.Observations)
                    .HasMaxLength(400)
                    .HasColumnName("observations");

                entity.Property(e => e.Reason)
                    .HasMaxLength(100)
                    .HasColumnName("reason");

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.Inspections)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Inspection_User1");

                entity.HasOne(d => d.ObjectIdNavigation)
                    .WithMany(p => p.Inspections)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Inspection_Object1");
            });

            modelBuilder.Entity<ObjectToCheck>(entity =>
            {
                entity.ToTable("object");

                entity.HasIndex(e => e.TargetGroupId, "fk_Object_Target_group1_idx");

                entity.HasIndex(e => e.UserLogin, "fk_Object_User_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(45)
                    .HasColumnName("user_login");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Location)
                    .HasMaxLength(45)
                    .HasColumnName("location");

                entity.Property(e => e.Model)
                    .HasMaxLength(45)
                    .HasColumnName("model");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.TargetGroupId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("Target_group_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .HasColumnName("type");

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.Objects)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Object_User");

                entity.HasOne(d => d.TargetGroupNavigation)
                    .WithMany(p => p.Objects)
                    .HasForeignKey(d => d.TargetGroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Object_Target_group1");
            });

            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.HasKey(e => e.ReqId)
                    .HasName("PRIMARY");

                entity.ToTable("requirement");

                entity.HasIndex(e => e.AuditingAuditingId, "fk_Requirement_Auditing1_idx");

                entity.Property(e => e.ReqId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("req_id");

                entity.Property(e => e.AuditingAuditingId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("Auditing_auditing_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Must).HasColumnName("must");

                entity.HasOne(d => d.AuditingAuditingIdNavigation)
                    .WithMany(p => p.Requirements)
                    .HasForeignKey(d => d.AuditingAuditingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Requirement_Auditing1");
            });

            modelBuilder.Entity<RequirementResult>(entity =>
            {
                entity.HasKey(e => e.RequirementId)
                    .HasName("PRIMARY");

                entity.ToTable("requirement_result");

                entity.HasIndex(e => e.AuditingLogsId, "fk_Requirement_result_Auditing_logs1_idx");

                entity.Property(e => e.RequirementId)
                    .HasMaxLength(45)
                    .HasColumnName("requirement_id");

                entity.Property(e => e.AuditingLogsId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("Auditing_logs_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.Must).HasColumnName("must");

                entity.Property(e => e.Result).HasColumnName("result");

                entity.HasOne(d => d.AuditingLogsNavigation)
                    .WithMany(p => p.RequirementResults)
                    .HasForeignKey(d => d.AuditingLogsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Requirement_result_Auditing_logs1");
            });

            modelBuilder.Entity<TargetGroup>(entity =>
            {
                entity.ToTable("target_group");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.Property(e => e.Login)
                    .HasMaxLength(45)
                    .HasColumnName("login");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Admin)
                    .HasColumnName("admin")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("timestamp")
                    .HasColumnName("last_login");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
