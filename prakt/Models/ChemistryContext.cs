using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prakt.Models;

public partial class ChemistryContext : DbContext
{
    public ChemistryContext()
    {
    }

    public ChemistryContext(DbContextOptions<ChemistryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=192.168.2.105;User=root;Database=Chemistry;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Client", "Chemistry");

            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("Full name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("Phone number");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Medicine", "Chemistry");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
