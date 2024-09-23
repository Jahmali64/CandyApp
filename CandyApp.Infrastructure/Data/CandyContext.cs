using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CandyApp.Infrastructure.Data;

public partial class CandyContext : DbContext
{
    public CandyContext()
    {
    }

    public CandyContext(DbContextOptions<CandyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fan> Fans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Candy;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
