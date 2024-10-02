using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PraxStock.Model.DBModels;

public partial class PraxixSkladContext : DbContext
{
    public PraxixSkladContext()
    {
    }

    public PraxixSkladContext(DbContextOptions<PraxixSkladContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataStock> DataStocks { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<MoveInPost> MoveInPosts { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<WriteOff> WriteOffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=ASUTP-RADKEVICH\\MSSQL_RADKEVICH; Database=Praxix_Sklad; Integrated Security=True; TrustServerCertificate=True");
        => optionsBuilder.UseSqlServer("Server=LANDROMM-ROG; Database=Praxix_Sklad; Integrated Security=True; TrustServerCertificate=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataStock>(entity =>
        {
            entity.HasKey(e => e.IdItemStock);

            entity.ToTable("DataStock");

            entity.Property(e => e.IdItemStock)
                .ValueGeneratedNever()
                .HasColumnName("idItemStock");
            entity.Property(e => e.IdItem).HasColumnName("idItem");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.DataStocks)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DataStock_Items");

            entity.HasOne(d => d.IdItemStockNavigation).WithOne(p => p.DataStock)
                .HasForeignKey<DataStock>(d => d.IdItemStock)
                .HasConstraintName("FK_DataStock_Receipt");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IdItem);

            entity.Property(e => e.IdItem).HasColumnName("idItem");
            entity.Property(e => e.NameItem).HasMaxLength(50);
            entity.Property(e => e.UnitMeasure).HasMaxLength(50);
        });

        modelBuilder.Entity<MoveInPost>(entity =>
        {
            entity.HasKey(e => e.IdMove);

            entity.ToTable("MoveInPost");

            entity.Property(e => e.IdMove).HasColumnName("idMove");
            entity.Property(e => e.IdItem).HasColumnName("idItem");
            entity.Property(e => e.NamePost).HasMaxLength(50);

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.MoveInPosts)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoveInPost_Items");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.IdReceipt);

            entity.ToTable("Receipt");

            entity.Property(e => e.IdReceipt).HasColumnName("idReceipt");
            entity.Property(e => e.IdItem).HasColumnName("idItem");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipt_Items1");
        });

        modelBuilder.Entity<WriteOff>(entity =>
        {
            entity.HasKey(e => e.IdWriteOff);

            entity.ToTable("WriteOff");

            entity.Property(e => e.IdWriteOff)
                .ValueGeneratedNever()
                .HasColumnName("idWriteOff");
            entity.Property(e => e.DateWriteOff)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.IdItem).HasColumnName("idItem");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.WriteOffs)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WriteOff_Items");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
