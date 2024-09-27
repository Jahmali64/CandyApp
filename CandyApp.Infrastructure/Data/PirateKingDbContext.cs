using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CandyApp.Infrastructure.Data;

public partial class PirateKingDbContext : DbContext
{
    public PirateKingDbContext()
    {
    }

    public PirateKingDbContext(DbContextOptions<PirateKingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BuildVersion> BuildVersions { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Fan> Fans { get; set; }

    public virtual DbSet<UserAdministrator> UserAdministrators { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=pirate-king.database.windows.net;Database=PirateKingDB;User ID=sql-admin;Password=Animefreak369@;Trusted_Connection=False;Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity.HasKey(e => e.SystemInformationId).HasName("PK__BuildVer__35E58ECA3A8AE4EB");

            entity.ToTable("BuildVersion");

            entity.Property(e => e.SystemInformationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SystemInformationID");
            entity.Property(e => e.DatabaseVersion)
                .HasMaxLength(25)
                .HasColumnName("Database Version");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VersionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorLogId).HasName("PK_ErrorLog_ErrorLogID");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");
            entity.Property(e => e.ErrorMessage).HasMaxLength(4000);
            entity.Property(e => e.ErrorProcedure).HasMaxLength(126);
            entity.Property(e => e.ErrorTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(128);
        });

        modelBuilder.Entity<Fan>(entity =>
        {
            entity.ToTable("Fan");

            entity.Property(e => e.FanId).HasColumnName("FanID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.FirstName).HasMaxLength(35);
            entity.Property(e => e.LastName).HasMaxLength(35);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserAdministrator>(entity =>
        {
            entity.ToTable("UserAdministrator");

            entity.Property(e => e.UserAdministratorId).HasColumnName("UserAdministratorID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });
        modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
