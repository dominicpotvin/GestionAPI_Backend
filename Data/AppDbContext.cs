using Microsoft.EntityFrameworkCore;
using GestEase.Models;

namespace GestEase.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Fournisseur> Fournisseurs { get; set; } = null!;
        public DbSet<CommandeFournisseur> CommandesFournisseurs { get; set; } = null!;
        public DbSet<CommandeClient> CommandesClients { get; set; } = null!;
        public DbSet<ArticleCommande> ArticlesCommande { get; set; } = null!;
        public DbSet<Categorie> Categories { get; set; } = null!;
        public DbSet<Dessin> Dessins { get; set; } = null!;
        public DbSet<DimensionProduit> DimensionsProduits { get; set; } = null!;
        public DbSet<DocumentProjet> Documents { get; set; } = null!;
        public DbSet<HistoriquePrix> HistoriquePrix { get; set; } = null!;
        public DbSet<Produit> Produits { get; set; } = null!;
        public DbSet<Projet> Projets { get; set; } = null!;
        public DbSet<StockProduit> StockProduits { get; set; } = null!;
        public DbSet<Facture> Factures { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<Produit>()
                    .HasOne(p => p.Fournisseur)
                    .WithMany(f => f.Produits)
                    .HasForeignKey(p => p.FournisseurId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<Produit>()
                    .HasOne<Categorie>()
                    .WithMany()
                    .HasForeignKey(p => p.CategorieId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<DimensionProduit>()
                    .HasOne(d => d.Produit)
                    .WithMany(p => p.Dimensions)
                    .HasForeignKey(d => d.ProduitId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<HistoriquePrix>()
                    .HasOne(h => h.Produit)
                    .WithMany()
                    .HasForeignKey(h => h.ProduitId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<CommandeFournisseur>()
                    .HasOne(c => c.Fournisseur)
                    .WithMany()
                    .HasForeignKey(c => c.FournisseurId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<CommandeFournisseur>()
                    .HasOne(c => c.Projet)
                    .WithMany()
                    .HasForeignKey(c => c.ProjetId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<CommandeClient>()
                    .HasOne(c => c.Client)
                    .WithMany()
                    .HasForeignKey(c => c.ClientId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<CommandeClient>()
                    .HasOne(c => c.Projet)
                    .WithMany()
                    .HasForeignKey(c => c.ProjetId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<ArticleCommande>()
                    .HasOne(a => a.Commande)
                    .WithMany()
                    .HasForeignKey(a => a.CommandeId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Dessin>()
                    .HasOne(d => d.Projet)
                    .WithMany()
                    .HasForeignKey(d => d.ProjetId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<Dessin>()
                    .HasOne(d => d.Document)
                    .WithMany()
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<StockProduit>()
                    .HasOne(s => s.Produit)
                    .WithMany()
                    .HasForeignKey(s => s.ProduitId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Facture>()
                    .HasOne(f => f.Client)
                    .WithMany()
                    .HasForeignKey(f => f.ClientId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Facture>()
                    .HasOne<CommandeFournisseur>(f => f.Commande)
                    .WithMany()
                    .HasForeignKey(f => f.CommandeId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erreur lors de la configuration du modèle EF Core : " + ex.Message);
                throw;
            }
        }
    }
}
