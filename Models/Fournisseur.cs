using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    public class Fournisseur
    {
        public int Id { get; set; }

        [Required]
        [Column("entreprise")]
        public string Entreprise { get; set; } = string.Empty;

        [Column("adresse")]
        public string Adresse { get; set; } = string.Empty;

        [Column("ville")]
        public string Ville { get; set; } = string.Empty;

        [Column("code_postal")]
        public string CodePostal { get; set; } = string.Empty;

        [Column("contact")]
        public string Contact { get; set; } = string.Empty;

        [Column("telephone")]
        public string Telephone { get; set; } = string.Empty;

        [Column("poste")]
        public string Poste { get; set; } = string.Empty;

        [Column("fax")]
        public string Fax { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        public ICollection<Produit> Produits { get; set; } = new List<Produit>();
    }
}
