using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("factures")]
    public class Facture
    {
        public int Id { get; set; }

        [Required]
        [Column("numero")]
        public string NumeroFacture { get; set; } = string.Empty;


        [Column("date_facture")]
        public DateTime DateEmission { get; set; } = DateTime.Now;

        [Column("commande_id")]
        public int CommandeId { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        [Column("montant_total")]
        public double MontantTotal { get; set; }

        [Column("date_paiement")]
        public DateTime? DatePaiement { get; set; }

        [Column("notes")]
        public string Notes { get; set; } = string.Empty;

        [Column("statut")]
        public string Statut { get; set; } = string.Empty;



        public CommandeFournisseur Commande { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }
}
