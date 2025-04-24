using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("dimensions_produits")]
    public class DimensionProduit
    {
        public int Id { get; set; }

        [Column("produit_id")]
        public int ProduitId { get; set; }

        [Column("dimension1")]
        public string Dimension1 { get; set; } = string.Empty;

        [Column("dimension2")]
        public string Dimension2 { get; set; } = string.Empty;

        [Column("dimension3")]
        public string Dimension3 { get; set; } = string.Empty;

        [Column("longueur")]
        public double? Longueur { get; set; }

        [Column("unite")]
        public string Unite { get; set; } = string.Empty;

        [ForeignKey("ProduitId")]
        public Produit Produit { get; set; } = new Produit();
    }
}
