using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestEase.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionArticlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReceptionArticlesController(AppDbContext context)
        {
            _context = context;
        }

        public class ReceptionDto
        {
            public int ArticleCommandeId { get; set; }
            public int QuantiteRecue { get; set; }
            public int CommandeFournisseurId { get; set; } // 🔐 Pour validation stricte
        }

        [HttpPost("recevoir")]
        public async Task<IActionResult> RecevoirArticle([FromBody] ReceptionDto dto)
        {
            var article = await _context.ArticlesCommande
                .FirstOrDefaultAsync(a => a.Id == dto.ArticleCommandeId && a.CommandeId == dto.CommandeFournisseurId);

            if (article == null)
                return NotFound("Article introuvable ou ne correspond pas à la commande fournisseur.");

            article.Statut = "Reçu";
            article.QuantiteRecue = dto.QuantiteRecue;
            article.DateModification = DateTime.Now;

            // 🔎 Mise à jour du stock
            var stock = await _context.StockProduits
                .FirstOrDefaultAsync(s => s.ProduitId == article.ProduitId);

            if (stock == null)
            {
                stock = new StockProduit
                {
                    ProduitId = article.ProduitId,
                    Quantite = dto.QuantiteRecue,
                    ValeurInventaire = article.PrixUnitaire * dto.QuantiteRecue,
                    DateDerniereVerification = DateTime.Now,
                    Localisation = "À définir"
                };
                _context.StockProduits.Add(stock);
            }
            else
            {
                stock.Quantite = (stock.Quantite ?? 0) + dto.QuantiteRecue;
                stock.ValeurInventaire += article.PrixUnitaire * dto.QuantiteRecue;
                stock.DateDerniereVerification = DateTime.Now;
            }

            // 🔁 Vérification de tous les articles pour cette commande fournisseur
            var commande = await _context.CommandesFournisseurs.FindAsync(dto.CommandeFournisseurId);
            if (commande != null)
            {
                commande.DateModification = DateTime.Now;
                if (commande.DateLivraisonReelle == null)
                    commande.DateLivraisonReelle = DateTime.Now;

                var articlesCommande = await _context.ArticlesCommande
                    .Where(a => a.CommandeId == dto.CommandeFournisseurId)
                    .ToListAsync();

                if (articlesCommande.All(a => a.Statut == "Reçu"))
                {
                    commande.Statut = "Reçue";
                }
                else if (articlesCommande.Any(a => a.Statut == "Reçu"))
                {
                    commande.Statut = "Partielle";
                }
                else
                {
                    commande.Statut = "En attente";
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Article reçu, stock et commande fournisseur mis à jour.");
        }
    }
}
