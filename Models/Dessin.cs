using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestEase.Models
{
    public class Dessin
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("numero")]
        public string Numero { get; set; } = string.Empty;

        [Column("titre")]
        public string Titre { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("projet_id")]
        public int? ProjetId { get; set; }

        [Column("document_id")]
        public int? DocumentId { get; set; }

        [Column("revision")]
        public string Revision { get; set; } = string.Empty;

        [Column("statut")]
        public string Statut { get; set; } = string.Empty;

        [Column("dessinateur")]
        public string Dessinateur { get; set; } = string.Empty;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("date_modification")]
        public DateTime DateModification { get; set; }

        public Projet Projet { get; set; } = new Projet();
        public DocumentProjet Document { get; set; } = new DocumentProjet();
    }
}
