using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using verbum_service_domain.Models;

namespace verbum_service_infrastructure.DataContext
{
    public partial class verbumContext : DbContext
    {
        public verbumContext()
        {
        }

        public verbumContext(DbContextOptions<verbumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Domain> Domains { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectQa> ProjectQas { get; set; } = null!;
        public virtual DbSet<ProjectSetting> ProjectSettings { get; set; } = null!;
        public virtual DbSet<ProjectTargetLanguage> ProjectTargetLanguages { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Revelancy> Revelancies { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SubDomain> SubDomains { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserCompany> UserCompanies { get; set; } = null!;
        public virtual DbSet<UserJob> UserJobs { get; set; } = null!;
        public virtual DbSet<Workflow> Workflows { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.CompanyId)
                    .ValueGeneratedNever()
                    .HasColumnName("company_id");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Domain>(entity =>
            {
                entity.ToTable("domain");

                entity.Property(e => e.DomainId)
                    .ValueGeneratedNever()
                    .HasColumnName("domain_id");

                entity.Property(e => e.DomainName)
                    .HasColumnType("character varying")
                    .HasColumnName("domain_name");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

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

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("job");

                entity.HasIndex(e => e.TargetLanguageId, "job_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("The id of the job");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("created_at")
                    .HasComment("The date time that the jobs was created");

                entity.Property(e => e.DocumentUrl)
                    .HasColumnName("document_url")
                    .HasComment("The URL of the job's document got uploaded to online storage");

                entity.Property(e => e.DueDate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("due_date")
                    .HasComment("Job have to finish before this date");

                entity.Property(e => e.FileExtension)
                    .HasColumnName("file_extension")
                    .HasComment("The file extension of the job's document");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasComment("The name of the job's document");

                entity.Property(e => e.Progress)
                    .HasColumnName("progress")
                    .HasComment("The number of completed segment out of all segment");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasComment("The project the this job is under");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("The status of the job");

                entity.Property(e => e.TargetLanguageId)
                    .HasColumnType("character varying")
                    .HasColumnName("target_language_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("updated_at")
                    .HasComment("The date time that the jobs was updated");

                entity.Property(e => e.WordCount)
                    .HasColumnName("word_count")
                    .HasComment("The number of words in the job's document");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_project_id_foreign");

                entity.HasOne(d => d.TargetLanguage)
                    .WithOne(p => p.Job)
                    .HasForeignKey<Job>(d => d.TargetLanguageId)
                    .HasConstraintName("job_language_fk");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("language");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("character varying")
                    .HasColumnName("language_id");

                entity.Property(e => e.LanguageName).HasColumnName("language_name");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.Entity).HasColumnName("entity");

                entity.Property(e => e.PermissionName).HasColumnName("permission_name");

                entity.HasMany(d => d.UserCompanies)
                    .WithMany(p => p.PermissionNames)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserPermission",
                        l => l.HasOne<UserCompany>().WithMany().HasForeignKey("UserCompanyId").HasConstraintName("user_permission_user_company_fk"),
                        r => r.HasOne<Permission>().WithMany().HasForeignKey("PermissionNameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("role_permission_permission_name_id_foreign"),
                        j =>
                        {
                            j.HasKey("PermissionNameId", "UserCompanyId").HasName("user_permission_pk");

                            j.ToTable("user_permission");

                            j.HasIndex(new[] { "PermissionNameId" }, "user_permission_permission_name_id_idx");

                            j.HasIndex(new[] { "UserCompanyId" }, "user_permission_user_company_id_idx");

                            j.IndexerProperty<int>("PermissionNameId").HasColumnName("permission_name_id");

                            j.IndexerProperty<int>("UserCompanyId").HasColumnName("user_company_id");
                        });
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CilentName)
                    .HasColumnName("cilent_name")
                    .HasComment("Name of the client that all jobs in this project belongs to");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("created_date");

                entity.Property(e => e.Domain)
                    .HasColumnName("domain")
                    .HasComment("The domain that this project this about");

                entity.Property(e => e.DueDate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("due_date")
                    .HasComment("All jobs in this projects must be earlier than this date");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasComment("Name of a project");

                entity.Property(e => e.OwnerName)
                    .HasColumnName("owner_name")
                    .HasComment("PMs or Admin who will manage this project");

                entity.Property(e => e.ProjectQaId)
                    .HasColumnName("project_qa_id")
                    .HasComment("The QA options that all jobs in this project have to follow");

                entity.Property(e => e.ProjectSettingId)
                    .HasColumnName("project_setting_id")
                    .HasComment("The settings that all jobs in this project have to follow");

                entity.Property(e => e.SourceLanguageId)
                    .HasColumnType("character varying")
                    .HasColumnName("source_language_id")
                    .HasComment("The original language all jobs in this project");

                entity.Property(e => e.SubDomain)
                    .HasColumnName("sub_domain")
                    .HasComment("The sub-domain of project's domain");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("project_company_fk");

                entity.HasOne(d => d.ProjectQa)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectQaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("project_project_qa_id_foreign");

                entity.HasOne(d => d.ProjectSetting)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectSettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("project_project_setting_id_foreign");

                entity.HasOne(d => d.SourceLanguage)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.SourceLanguageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("project_language_fk");
            });

            modelBuilder.Entity<ProjectQa>(entity =>
            {
                entity.ToTable("project_qa");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.QaOptions).HasColumnName("qa_options");
            });

            modelBuilder.Entity<ProjectSetting>(entity =>
            {
                entity.ToTable("project_settings");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FileNameFormat).HasColumnName("file_name_format");

                entity.Property(e => e.MarkedCanceledOn).HasColumnName("marked_canceled_on");

                entity.Property(e => e.MarkedCompletedOn).HasColumnName("marked_completed_on");

                entity.Property(e => e.PreTranslate).HasColumnName("pre_translate");

                entity.Property(e => e.Workflow).HasColumnName("workflow");
            });

            modelBuilder.Entity<ProjectTargetLanguage>(entity =>
            {
                entity.ToTable("project_target_language");

                entity.HasIndex(e => e.LanguageId, "project_target_language_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.LanguageId)
                    .HasColumnType("character varying")
                    .HasColumnName("language_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.HasOne(d => d.Language)
                    .WithOne(p => p.ProjectTargetLanguage)
                    .HasForeignKey<ProjectTargetLanguage>(d => d.LanguageId)
                    .HasConstraintName("project_target_language_language_fk");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTargetLanguages)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("project_target_language_project_fk");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("refreshtoken_pk");

                entity.ToTable("refresh_token");

                entity.Property(e => e.TokenId)
                    .HasColumnName("token_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ExpireAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("expire_at");

                entity.Property(e => e.IssuedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("issued_at");

                entity.Property(e => e.TokenContent)
                    .HasColumnType("character varying")
                    .HasColumnName("token_content");
            });

            modelBuilder.Entity<Revelancy>(entity =>
            {
                entity.ToTable("revelancy");

                entity.HasIndex(e => e.SourceLanguageId, "revelancy_unique")
                    .IsUnique();

                entity.HasIndex(e => e.TargetLanguageId, "revelancy_unique_1")
                    .IsUnique();

                entity.Property(e => e.RevelancyId)
                    .ValueGeneratedNever()
                    .HasColumnName("revelancy_id");

                entity.Property(e => e.SourceLanguageId)
                    .HasColumnType("character varying")
                    .HasColumnName("source_language_id");

                entity.Property(e => e.SubDomainId).HasColumnName("sub_domain_id");

                entity.Property(e => e.TargetLanguageId)
                    .HasColumnType("character varying")
                    .HasColumnName("target_language_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.SourceLanguage)
                    .WithOne(p => p.RevelancySourceLanguage)
                    .HasForeignKey<Revelancy>(d => d.SourceLanguageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("revelancy_language_fk");

                entity.HasOne(d => d.SubDomain)
                    .WithMany(p => p.Revelancies)
                    .HasForeignKey(d => d.SubDomainId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("revelancy_sub_domain_fk");

                entity.HasOne(d => d.TargetLanguage)
                    .WithOne(p => p.RevelancyTargetLanguage)
                    .HasForeignKey<Revelancy>(d => d.TargetLanguageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("target_revelancy_language_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Revelancies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("revelancy_user_fk");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("Role_pkey");

                entity.ToTable("role");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<SubDomain>(entity =>
            {
                entity.ToTable("sub_domain");

                entity.HasIndex(e => e.DomainId, "sub_domain_domain_id_idx");

                entity.Property(e => e.SubDomainId)
                    .ValueGeneratedNever()
                    .HasColumnName("sub_domain_id");

                entity.Property(e => e.DomainId).HasColumnName("domain_id");

                entity.Property(e => e.SubDomainName)
                    .HasColumnType("character varying")
                    .HasColumnName("sub_domain_name");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.SubDomains)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sub_domain_domain_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "User_email_key")
                    .IsUnique();

                entity.HasIndex(e => new { e.Email, e.Password }, "user_login_idx");

                entity.HasIndex(e => e.TokenId, "user_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.EmailVerified)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("email_verified");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'ACTIVE'::text");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_image_fk");

                entity.HasOne(d => d.Token)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.TokenId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_refreshtoken_fk");
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.ToTable("user_company");

                entity.HasIndex(e => e.CompanyId, "user_company_company_id_idx");

                entity.HasIndex(e => e.Role, "user_company_unique")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_company_user_id_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_company_company_id_foreign");

                entity.HasOne(d => d.RoleNavigation)
                    .WithOne(p => p.UserCompany)
                    .HasForeignKey<UserCompany>(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_company_role_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCompanies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_company_user_id_foreign");
            });

            modelBuilder.Entity<UserJob>(entity =>
            {
                entity.ToTable("user_job");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.WorkflowId).HasColumnName("workflow_id");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.UserJobs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_job_job_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserJobs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_job_user_id_foreign");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.UserJobs)
                    .HasForeignKey(d => d.WorkflowId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_job_workflow_fk");
            });

            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.ToTable("workflow");

                entity.Property(e => e.WorkflowId)
                    .ValueGeneratedNever()
                    .HasColumnName("workflow_id");

                entity.Property(e => e.WorkflowName)
                    .HasColumnType("character varying")
                    .HasColumnName("workflow_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
