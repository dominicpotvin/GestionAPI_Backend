using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("projets")]
    public class Projet
    {
        public int Id { get; set; }

        [Required]
        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("client_id")]
        public int? ClientId { get; set; }

        [Column("date_debut")]
        public DateTime? DateDebut { get; set; }

        [Column("date_fin")]
        public DateTime? DateFin { get; set; }

        [Column("statut")]
        public string Statut { get; set; } = string.Empty;

        // Relation avec Client (optionnel)
        public Client? Client { get; set; }
    }
}
