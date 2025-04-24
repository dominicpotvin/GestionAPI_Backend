using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("stock_produits")]
    public class StockProduit
    {
        public int Id { get; set; }

        [Column("produit_id")]
        public int ProduitId { get; set; }

        [Column("quantite")]
        public int? Quantite { get; set; }

        [Column("localisation")]
        public string Localisation { get; set; } = string.Empty;

        [Column("valeur_inventaire")]
        public double? ValeurInventaire { get; set; }

        [Column("date_derniere_verification")]
        public DateTime? DateDerniereVerification { get; set; }

        public Produit? Produit { get; set; }
    }
}
