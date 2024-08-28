using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using verbum_service_domain.Common;
using verbum_service_domain.Models;

namespace verbum_service_infrastructure.DataContext
{
    public partial class verbum_dbContext : DbContext
    {
        public verbum_dbContext()
        {
        }

        public verbum_dbContext(DbContextOptions<verbum_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Refreshtoken> Refreshtokens { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(SystemConfig.CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.HasIndex(e => e.ImageLink, "image_unique")
                    .IsUnique();

                entity.Property(e => e.ImageId)
                    .HasColumnName("image_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ImageLink)
                    .HasColumnType("character varying")
                    .HasColumnName("image_link")
                    .HasComment("save link, image will be save on thirdparty (cloudinary)");
            });

            modelBuilder.Entity<Refreshtoken>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("refreshtoken_pk");

                entity.ToTable("refreshtoken");

                entity.Property(e => e.TokenId)
                    .HasColumnName("token_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ExpireAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("expire_at");

                entity.Property(e => e.IssueAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("issue_at");

                entity.Property(e => e.TokenContent)
                    .HasColumnType("character varying")
                    .HasColumnName("token_content");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("Role_pkey");

                entity.ToTable("Role");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "User_email_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.EmailVerified)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("emailVerified");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.RoleName).HasColumnName("roleName");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_image_fk");

                entity.HasOne(d => d.NameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Name)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("user_role_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
