using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("produits")]
    public class Produit
    {
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("categorie_id")]
        public int? CategorieId { get; set; }

        [Column("description_sommaire")]
        public string? DescriptionSommaire { get; set; }  // ✅ peut être NULL

        [Column("prix_liste")]
        public double? PrixListe { get; set; }

        [Column("prix_unitaire")]
        public double? PrixUnitaire { get; set; }

        [Column("quantite_min")]
        public int? QuantiteMin { get; set; }

        [Column("date_mise_a_jour")]
        public DateTime? DateMiseAJour { get; set; }

        [Column("fournisseur_id")]
        public int? FournisseurId { get; set; }

        [Column("code_fournisseur")]
        public string? CodeFournisseur { get; set; }  // ✅ peut être NULL

        [ForeignKey("FournisseurId")]
        public Fournisseur? Fournisseur { get; set; }

        public ICollection<DimensionProduit> Dimensions { get; set; } = new List<DimensionProduit>();
    }
}
