using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Utilisateur
{
    public int Id { get; set; }

    [Column("nom")]
    public string Nom { get; set; } = string.Empty;

    [Column("initiales")]
    public string Initiales { get; set; } = string.Empty;

    [Column("mot_de_passe")]
    public string MotDePasse { get; set; } = string.Empty;

    [Column("role")]
    public string? Role { get; set; }

    [Column("actif")]
    public bool Actif { get; set; } = true;

    [Column("date_creation")]
    public DateTime DateCreation { get; set; } = DateTime.Now;
}

