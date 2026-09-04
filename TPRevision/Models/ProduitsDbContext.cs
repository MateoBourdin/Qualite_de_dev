
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

public partial class ProduitsDbContext : DbContext
{
    public ProduitsDbContext() { }
    public ProduitsDbContext(DbContextOptions<ProduitsDbContext> options) : base(options) { }
    public virtual DbSet<Avi> Avis { get; set; }
    public virtual DbSet<Marque> Categories { get; set; }
    public virtual DbSet<Produit> Films { get; set; }
    public virtual DbSet<TypeProduit> Utilisateurs { get; set; }


    
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 =>optionsBuilder.UseNpgsql("Server=localhost:port-5432;Database=Films00; uid-postgres; password-postgres;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avi>(entity =>
        {
            entity.HasKey(e => new { e.Idfilm, e.Idutilisateur }).HasName("pk_avis");

            entity.HasOne(d => d.IdfilmNavigation).WithMany(p => p.Avis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_avis_film");

            entity.HasOne(d => d.IdutilisateurNavigation).WithMany(p => p.Avis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_avis_utilisateur");
        });

        modelBuilder.Entity<Marque>(entity =>
        {
            entity.HasKey(e => e.Idcategorie).HasName("pk_categorie");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.Idfilm).HasName("pk_film");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Films)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_categorie");

        });

        modelBuilder.Entity<TypeProduit>(entity =>
        {
            entity.HasKey(e => e.Idutilisateur).HasName("pk_utilisateur");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}