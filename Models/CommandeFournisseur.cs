using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("commandes_fournisseurs")]
    public class CommandeFournisseur
    {
        public int Id { get; set; }

        [Required]
        [Column("numero_commande")]
        public string NumeroCommande { get; set; } = string.Empty;

        [Required]
        [Column("date_commande")]
        public DateTime DateCommande { get; set; }

        [Column("fournisseur_id")]
        public int? FournisseurId { get; set; }

        [Column("projet_id")]
        public int? ProjetId { get; set; }

        [Column("montant_total")]
        public double? MontantTotal { get; set; }

        [Column("statut")]
        public string Statut { get; set; } = string.Empty;

        [Column("date_livraison_prevue")]
        public DateTime? DateLivraisonPrevue { get; set; }

        [Column("date_livraison_reelle")]
        public DateTime? DateLivraisonReelle { get; set; }

        [Column("notes")]
        public string Notes { get; set; } = string.Empty;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("date_modification")]
        public DateTime DateModification { get; set; }

        public Fournisseur? Fournisseur { get; set; }
        public Projet? Projet { get; set; }
    }
}
