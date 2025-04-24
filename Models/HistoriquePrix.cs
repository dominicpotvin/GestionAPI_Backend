using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("historique_prix")]
    public class HistoriquePrix
    {
        public int Id { get; set; }

        [Column("produit_id")]
        public int ProduitId { get; set; }

        [Column("prix")]
        public double Prix { get; set; }

        [Column("date_changement")]
        public DateTime DateChangement { get; set; }

        // 🔗 Relation vers le produit
        public Produit? Produit { get; set; }
    }
}
