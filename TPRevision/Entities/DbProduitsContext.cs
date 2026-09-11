using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TPRevision.Entities;

public partial class DbProduitsContext : DbContext
{
    public DbProduitsContext()
    {
    }

    public DbProduitsContext(DbContextOptions<DbProduitsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Marque> Marques { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<TypeProduit> TypeProduits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Produits;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Marque>(entity =>
        {
            entity.HasKey(e => e.IdMarque).HasName("t_e_marque_pro_pkey");
            entity.ToTable("t_e_marque_pro");
            entity.Property(e => e.IdMarque).HasColumnName("idMarque");
            entity.Property(e => e.NomMarque)
                .HasMaxLength(50)
                .HasColumnName("nomMarque");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.IdProduit).HasName("t_e_produit_pro_pkey");
            entity.ToTable("t_e_produit_pro");
            entity.Property(e => e.IdProduit).HasColumnName("idProduit");
            entity.Property(e => e.nomProduit)
                .HasMaxLength(50)
                .HasColumnName("nomProduit");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.NomPhoto)
                .HasMaxLength(50)
                .HasColumnName("nomPhoto");
            entity.Property(e => e.UriPhoto)
                .HasMaxLength(50)
                .HasColumnName("uriPhoto");
            entity.Property(e => e.stockReel).HasColumnName("StockReel");
            entity.Property(e => e.stockMin).HasColumnName("StockMin");
            entity.Property(e => e.stockMax).HasColumnName("StockMax");
            entity.HasOne(d => d.TypeProduitNavigation).WithMany()
                .HasForeignKey(d => d.IdTypeProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_typeproduit");
            entity.HasOne(d => d.MarqueNavigation).WithMany()
                .HasForeignKey(d => d.IdMarque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_marque");
        });

        modelBuilder.Entity<TypeProduit>(entity =>
        {
            entity.HasKey(e => e.IdTypeProduit).HasName("t_e_typeproduit_pro_pkey");
            entity.ToTable("t_e_typeproduit_pro");
            entity.Property(e => e.IdTypeProduit).HasColumnName("idTypeProduit");
            entity.Property(e => e.NomTypeProduit)
                .HasMaxLength(50)
                .HasColumnName("nomTypeProduit");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
