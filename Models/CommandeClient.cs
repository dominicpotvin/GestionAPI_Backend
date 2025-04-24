using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("commandes_clients")]
    public class CommandeClient
    {
        public int Id { get; set; }

        [Required]
        [Column("numero_commande")]
        public string NumeroCommande { get; set; } = string.Empty;

        [Required]
        [Column("date_commande")]
        public DateTime DateCommande { get; set; } = DateTime.Now;

        [Column("client_id")]
        public int? ClientId { get; set; }

        [Column("projet_id")]
        public int? ProjetId { get; set; }

        [Column("montant_total")]
        public double? MontantTotal { get; set; }

        [Column("statut")]
        public string Statut { get; set; } = "En attente";

        [Column("date_livraison_prevue")]
        public DateTime? DateLivraisonPrevue { get; set; }

        [Column("date_livraison_reelle")]
        public DateTime? DateLivraisonReelle { get; set; }

        [Column("notes")]
        public string Notes { get; set; } = string.Empty;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Column("date_modification")]
        public DateTime DateModification { get; set; } = DateTime.Now;

        // 🔗 Navigation
        public Client? Client { get; set; }
        public Projet? Projet { get; set; }
    }
}
