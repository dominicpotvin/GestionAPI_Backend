using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;
using GestEase.Dtos;

namespace GestEase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesCommandeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArticlesCommandeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleCommande>>> GetAll()
        {
            return await _context.ArticlesCommande.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleCommande>> Get(int id)
        {
            var item = await _context.ArticlesCommande.FindAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<ArticleCommande>> Post(ArticleCommande item)
        {
            _context.ArticlesCommande.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ArticleCommande item)
        {
            if (id != item.Id) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ArticlesCommande.FindAsync(id);
            if (item == null) return NotFound();

            _context.ArticlesCommande.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("by-commande/{commandeId}")]
        public async Task<IActionResult> DeleteByCommandeId(int commandeId)
        {
            var articles = await _context.ArticlesCommande
                .Where(a => a.CommandeId == commandeId)
                .ToListAsync();

            if (!articles.Any())
                return NotFound();

            _context.ArticlesCommande.RemoveRange(articles);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // ✅ POST /api/ArticlesCommande/reception
        [HttpPost("reception")]
        public async Task<IActionResult> ReceptionArticle([FromBody] ReceptionArticleDto dto)
        {
            var article = await _context.ArticlesCommande.FindAsync(dto.ArticleId);
            if (article == null)
                return NotFound();

            // Mise à jour article
            article.QuantiteRecue += dto.QuantiteRecue;
            article.Statut = "Reçu";
            article.DateModification = DateTime.Now;

            // Mise à jour ou création de stock
            var stock = await _context.StockProduits.FirstOrDefaultAsync(s => s.ProduitId == article.ProduitId);
            if (stock != null)
            {
                stock.Quantite = (stock.Quantite ?? 0) + dto.QuantiteRecue;
                stock.DateDerniereVerification = DateTime.Now;
            }
            else
            {
                stock = new StockProduit
                {
                    ProduitId = article.ProduitId,
                    Quantite = dto.QuantiteRecue,
                    Localisation = "À définir",
                    ValeurInventaire = article.PrixUnitaire * dto.QuantiteRecue,
                    DateDerniereVerification = DateTime.Now
                };
                _context.StockProduits.Add(stock);
            }

            // Vérification : tous les articles de la commande sont-ils reçus ?
            var commandeId = article.CommandeId;
            var tousRecus = await _context.ArticlesCommande
                .Where(a => a.CommandeId == commandeId)
                .AllAsync(a => a.Statut == "Reçu");

            if (tousRecus)
            {
                var commande = await _context.CommandesFournisseurs
                    .FirstOrDefaultAsync(c => c.Id == commandeId);

                if (commande != null)
                {
                    commande.Statut = "Reçue";
                    commande.DateLivraisonReelle = DateTime.Now;
                    commande.DateModification = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Article reçu, stock et statut commande mis à jour." });
        }
    }
}
