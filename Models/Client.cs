using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Column("adresse")]
        public string Adresse { get; set; } = string.Empty;

        [Column("ville")]
        public string Ville { get; set; } = string.Empty;

        [Column("code_postal")]
        public string CodePostal { get; set; } = string.Empty;

        [Column("telephone")]
        public string Telephone { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}
