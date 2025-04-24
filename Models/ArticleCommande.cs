using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("articles_commande")]
    public class ArticleCommande
    {
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("quantite")]
        public int Quantite { get; set; }

        [Column("unite")]
        public string Unite { get; set; } = string.Empty;

        [Column("prix_unitaire")]
        public double PrixUnitaire { get; set; }

        [Column("sous_total")]
        public double? SousTotal { get; set; }

        [Column("statut")]
        public string Statut { get; set; } = "En attente";

        [Column("quantite_recue")]
        public int QuantiteRecue { get; set; } = 0;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("date_modification")]
        public DateTime DateModification { get; set; }

        [Column("commande_id")]
        public int CommandeId { get; set; }

        [Column("produit_id")]
        public int ProduitId { get; set; }

        public CommandeFournisseur? Commande { get; set; }
    }
}
