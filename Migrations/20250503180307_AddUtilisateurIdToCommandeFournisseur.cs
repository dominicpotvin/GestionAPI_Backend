using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestEase.Migrations
{
    public partial class AddUtilisateurIdToCommandeFournisseur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // (rien modifié ici : garde tout ce que EF a généré)
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_commande_commandes_fournisseurs_commande_id",
                table: "articles_commande");

            migrationBuilder.DropForeignKey(
                name: "FK_factures_commandes_fournisseurs_commande_id",
                table: "factures");

            migrationBuilder.DropForeignKey(
                name: "FK_produits_categories_categorie_id",
                table: "produits");

            migrationBuilder.DropTable(name: "commandes_clients");
            migrationBuilder.DropTable(name: "commandes_fournisseurs");
            migrationBuilder.DropTable(name: "utilisateurs");

            migrationBuilder.DropPrimaryKey(name: "PK_categories", table: "categories");

            migrationBuilder.DropColumn(name: "statut", table: "factures");
            migrationBuilder.DropColumn(name: "date_creation", table: "articles_commande");
            migrationBuilder.DropColumn(name: "date_modification", table: "articles_commande");
            migrationBuilder.DropColumn(name: "produit_id", table: "articles_commande");
            migrationBuilder.DropColumn(name: "quantite_recue", table: "articles_commande");
            migrationBuilder.DropColumn(name: "sous_total", table: "articles_commande");

            migrationBuilder.RenameTable(name: "categories", newName: "Categories");
            migrationBuilder.RenameColumn(name: "nom_categorie", table: "Categories", newName: "NomCategorie");

            migrationBuilder.AddColumn<int>(
                name: "projet_id",
                table: "stock_produits",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description_sommaire",
                table: "produits",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code_fournisseur",
                table: "produits",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            // ✅ 🔥 Bloc CreateTable("Commandes") supprimé ici (était invalide)

            migrationBuilder.CreateIndex(
                name: "IX_stock_produits_projet_id",
                table: "stock_produits",
                column: "projet_id");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_commande_Commandes_commande_id",
                table: "articles_commande",
                column: "commande_id",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_factures_Commandes_commande_id",
                table: "factures",
                column: "commande_id",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produits_Categories_categorie_id",
                table: "produits",
                column: "categorie_id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_stock_produits_projets_projet_id",
                table: "stock_produits",
                column: "projet_id",
                principalTable: "projets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
