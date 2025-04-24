using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    [Table("categories")]
    public class Categorie
    {
        public int Id { get; set; }

        [Column("nom_categorie")] // correspondance exacte avec la colonne SQL
        public string NomCategorie { get; set; } = string.Empty;
    }
}
