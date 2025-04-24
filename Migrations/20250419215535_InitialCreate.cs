using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestEase.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomCategorie = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: false),
                    adresse = table.Column<string>(type: "TEXT", nullable: false),
                    ville = table.Column<string>(type: "TEXT", nullable: false),
                    code_postal = table.Column<string>(type: "TEXT", nullable: false),
                    telephone = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    date_creation = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    entreprise = table.Column<string>(type: "TEXT", nullable: false),
                    adresse = table.Column<string>(type: "TEXT", nullable: false),
                    ville = table.Column<string>(type: "TEXT", nullable: false),
                    code_postal = table.Column<string>(type: "TEXT", nullable: false),
                    contact = table.Column<string>(type: "TEXT", nullable: false),
                    telephone = table.Column<string>(type: "TEXT", nullable: false),
                    poste = table.Column<string>(type: "TEXT", nullable: false),
                    fax = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    client_id = table.Column<int>(type: "INTEGER", nullable: true),
                    date_debut = table.Column<DateTime>(type: "TEXT", nullable: true),
                    date_fin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    statut = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projets_Clients_client_id",
                        column: x => x.client_id,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    categorie_id = table.Column<int>(type: "INTEGER", nullable: true),
                    description_sommaire = table.Column<string>(type: "TEXT", nullable: false),
                    prix_liste = table.Column<double>(type: "REAL", nullable: true),
                    prix_unitaire = table.Column<double>(type: "REAL", nullable: true),
                    quantite_min = table.Column<int>(type: "INTEGER", nullable: true),
                    date_mise_a_jour = table.Column<DateTime>(type: "TEXT", nullable: true),
                    fournisseur_id = table.Column<int>(type: "INTEGER", nullable: true),
                    code_fournisseur = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_produits_Categories_categorie_id",
                        column: x => x.categorie_id,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_produits_Fournisseurs_fournisseur_id",
                        column: x => x.fournisseur_id,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    numero_commande = table.Column<string>(type: "TEXT", nullable: false),
                    date_commande = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fournisseur_id = table.Column<int>(type: "INTEGER", nullable: true),
                    projet_id = table.Column<int>(type: "INTEGER", nullable: true),
                    montant_total = table.Column<double>(type: "REAL", nullable: true),
                    statut = table.Column<string>(type: "TEXT", nullable: false),
                    date_livraison_prevue = table.Column<DateTime>(type: "TEXT", nullable: true),
                    date_livraison_reelle = table.Column<DateTime>(type: "TEXT", nullable: true),
                    notes = table.Column<string>(type: "TEXT", nullable: false),
                    date_creation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    date_modification = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commandes_Fournisseurs_fournisseur_id",
                        column: x => x.fournisseur_id,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Commandes_projets_projet_id",
                        column: x => x.projet_id,
                        principalTable: "projets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: false),
                    chemin_fichier = table.Column<string>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: true),
                    projet_id = table.Column<int>(type: "INTEGER", nullable: true),
                    date_creation = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_documents_projets_projet_id",
                        column: x => x.projet_id,
                        principalTable: "projets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "dimensions_produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    produit_id = table.Column<int>(type: "INTEGER", nullable: false),
                    dimension1 = table.Column<string>(type: "TEXT", nullable: false),
                    dimension2 = table.Column<string>(type: "TEXT", nullable: false),
                    dimension3 = table.Column<string>(type: "TEXT", nullable: false),
                    longueur = table.Column<double>(type: "REAL", nullable: true),
                    unite = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dimensions_produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dimensions_produits_produits_produit_id",
                        column: x => x.produit_id,
                        principalTable: "produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historique_prix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    produit_id = table.Column<int>(type: "INTEGER", nullable: false),
                    prix = table.Column<double>(type: "REAL", nullable: false),
                    date_changement = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historique_prix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_historique_prix_produits_produit_id",
                        column: x => x.produit_id,
                        principalTable: "produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    produit_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantite = table.Column<int>(type: "INTEGER", nullable: true),
                    localisation = table.Column<string>(type: "TEXT", nullable: false),
                    projet_id = table.Column<int>(type: "INTEGER", nullable: true),
                    valeur_inventaire = table.Column<double>(type: "REAL", nullable: true),
                    date_derniere_verification = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stock_produits_produits_produit_id",
                        column: x => x.produit_id,
                        principalTable: "produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stock_produits_projets_projet_id",
                        column: x => x.projet_id,
                        principalTable: "projets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "articles_commande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    unite = table.Column<string>(type: "TEXT", nullable: false),
                    prix_unitaire = table.Column<double>(type: "REAL", nullable: false),
                    statut = table.Column<string>(type: "TEXT", nullable: false),
                    commande_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles_commande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_articles_commande_Commandes_commande_id",
                        column: x => x.commande_id,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "factures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    numero = table.Column<string>(type: "TEXT", nullable: false),
                    date_facture = table.Column<DateTime>(type: "TEXT", nullable: false),
                    commande_id = table.Column<int>(type: "INTEGER", nullable: false),
                    client_id = table.Column<int>(type: "INTEGER", nullable: false),
                    montant_total = table.Column<double>(type: "REAL", nullable: false),
                    date_paiement = table.Column<DateTime>(type: "TEXT", nullable: true),
                    notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_factures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_factures_Clients_client_id",
                        column: x => x.client_id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_factures_Commandes_commande_id",
                        column: x => x.commande_id,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dessins",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    numero = table.Column<string>(type: "TEXT", nullable: false),
                    titre = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    projet_id = table.Column<int>(type: "INTEGER", nullable: true),
                    document_id = table.Column<int>(type: "INTEGER", nullable: true),
                    revision = table.Column<string>(type: "TEXT", nullable: false),
                    statut = table.Column<string>(type: "TEXT", nullable: false),
                    dessinateur = table.Column<string>(type: "TEXT", nullable: false),
                    date_creation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    date_modification = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DocumentProjetId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dessins", x => x.id);
                    table.ForeignKey(
                        name: "FK_Dessins_documents_DocumentProjetId",
                        column: x => x.DocumentProjetId,
                        principalTable: "documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dessins_documents_document_id",
                        column: x => x.document_id,
                        principalTable: "documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Dessins_projets_projet_id",
                        column: x => x.projet_id,
                        principalTable: "projets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articles_commande_commande_id",
                table: "articles_commande",
                column: "commande_id");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_fournisseur_id",
                table: "Commandes",
                column: "fournisseur_id");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_projet_id",
                table: "Commandes",
                column: "projet_id");

            migrationBuilder.CreateIndex(
                name: "IX_Dessins_document_id",
                table: "Dessins",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_Dessins_DocumentProjetId",
                table: "Dessins",
                column: "DocumentProjetId");

            migrationBuilder.CreateIndex(
                name: "IX_Dessins_projet_id",
                table: "Dessins",
                column: "projet_id");

            migrationBuilder.CreateIndex(
                name: "IX_dimensions_produits_produit_id",
                table: "dimensions_produits",
                column: "produit_id");

            migrationBuilder.CreateIndex(
                name: "IX_documents_projet_id",
                table: "documents",
                column: "projet_id");

            migrationBuilder.CreateIndex(
                name: "IX_factures_client_id",
                table: "factures",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_factures_commande_id",
                table: "factures",
                column: "commande_id");

            migrationBuilder.CreateIndex(
                name: "IX_historique_prix_produit_id",
                table: "historique_prix",
                column: "produit_id");

            migrationBuilder.CreateIndex(
                name: "IX_produits_categorie_id",
                table: "produits",
                column: "categorie_id");

            migrationBuilder.CreateIndex(
                name: "IX_produits_fournisseur_id",
                table: "produits",
                column: "fournisseur_id");

            migrationBuilder.CreateIndex(
                name: "IX_projets_client_id",
                table: "projets",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_produits_produit_id",
                table: "stock_produits",
                column: "produit_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_produits_projet_id",
                table: "stock_produits",
                column: "projet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles_commande");

            migrationBuilder.DropTable(
                name: "Dessins");

            migrationBuilder.DropTable(
                name: "dimensions_produits");

            migrationBuilder.DropTable(
                name: "factures");

            migrationBuilder.DropTable(
                name: "historique_prix");

            migrationBuilder.DropTable(
                name: "stock_produits");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "produits");

            migrationBuilder.DropTable(
                name: "projets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
