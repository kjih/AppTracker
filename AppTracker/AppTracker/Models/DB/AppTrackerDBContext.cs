using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppTracker.Models.DB
{
    public partial class AppTrackerDBContext : DbContext
    {
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationContactXref> ApplicationContactXref { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }

        public AppTrackerDBContext(DbContextOptions<AppTrackerDBContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicationDate)
                    .HasColumnName("applicationDate")
                    .HasColumnType("date");

                entity.Property(e => e.CompanyId).HasColumnName("companyId");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(64);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Applicati__compa__15502E78");
            });

            modelBuilder.Entity<ApplicationContactXref>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicationId).HasColumnName("applicationId");

                entity.Property(e => e.ContactId).HasColumnName("contactId");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationContactXref)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK__Applicati__appli__1ED998B2");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ApplicationContactXref)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK__Applicati__conta__1FCDBCEB");
            });

            modelBuilder.Entity<ApplicationStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.ApplicationId).HasColumnName("applicationId");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(32);

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationStatus)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK__Applicati__appli__182C9B23");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address1)
                    .HasColumnName("address1")
                    .HasMaxLength(64);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(64);

                entity.Property(e => e.Address3)
                    .HasColumnName("address3")
                    .HasMaxLength(64);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(64);

                entity.Property(e => e.Notes).HasColumnName("notes");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyId");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(32);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(32);

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(32);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(64);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Contact__company__1BFD2C07");
            });
        }
    }
}
