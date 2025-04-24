using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("documents")]
    public class DocumentProjet
    {
        public int Id { get; set; }

        [Required]
        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [Column("chemin_fichier")]
        public string CheminFichier { get; set; } = string.Empty;

        [Column("type")]
        public string? Type { get; set; }

        [Column("projet_id")]
        public int? ProjetId { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        // 🔗 Propriété de navigation vers Projet
        public Projet? Projet { get; set; }

        // 🔗 Propriété de navigation inverse : un document peut avoir plusieurs dessins qui y sont liés
        public ICollection<Dessin> Dessins { get; set; } = new List<Dessin>();
    }
}