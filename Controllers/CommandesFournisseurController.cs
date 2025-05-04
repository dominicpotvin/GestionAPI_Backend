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
    public class CommandesFournisseurController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommandesFournisseurController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandeFournisseur>>> GetCommandesFournisseur()
        {
            return await _context.CommandesFournisseurs
                .Include(c => c.Fournisseur)
                .Include(c => c.Projet)
                .Include(c => c.Utilisateur)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeFournisseur>> GetCommandeFournisseur(int id)
        {
            var commande = await _context.CommandesFournisseurs
                .Include(c => c.Fournisseur)
                .Include(c => c.Projet)
                .Include(c => c.Utilisateur)
                .FirstOrDefaultAsync(c => c.Id == id);

            return commande == null ? NotFound() : commande;
        }

        [HttpPost]
        public async Task<ActionResult<CommandeFournisseur>> PostCommandeFournisseur(CommandeFournisseur commande)
        {
            // Simuler un utilisateur connecté pour l’instant
            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(); // Remplacer plus tard par l’utilisateur authentifié

            if (utilisateur == null)
                return BadRequest("Aucun utilisateur disponible pour générer la commande.");

            // Générer un numéro unique
            var dernierId = await _context.CommandesFournisseurs.MaxAsync(c => (int?)c.Id) ?? 0;
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var numero = $"{utilisateur.Initiales}-{date}-{(dernierId + 1).ToString("D3")}";

            // Affectation
            commande.NumeroCommande = numero;
            commande.UtilisateurId = utilisateur.Id;
            commande.DateCreation = DateTime.UtcNow;
            commande.DateModification = DateTime.UtcNow;

            _context.CommandesFournisseurs.Add(commande);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommandeFournisseur), new { id = commande.Id }, commande);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandeFournisseur(int id, CommandeFournisseur commande)
        {
            if (id != commande.Id)
                return BadRequest();

            _context.Entry(commande).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("generer-numero")]
        [Authorize]
        public async Task<IActionResult> GenererNumeroCommande()
        {
            // Récupère les initiales à partir du token JWT
            var initiales = User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(initiales))
                return Unauthorized("Impossible de récupérer les initiales de l'utilisateur connecté.");

            // Utilise les 3 premières lettres seulement si elles existent
            var prefix = initiales.Length >= 3 ? initiales.Substring(0, 3).ToUpper() : initiales.ToUpper();

            var dernierId = await _context.CommandesFournisseurs.MaxAsync(c => (int?)c.Id) ?? 0;
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var numero = $"{prefix}-{date}-{(dernierId + 1).ToString("D3")}";

            return Ok(new { numero });
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommandeFournisseur(int id)
        {
            var commande = await _context.CommandesFournisseurs.FindAsync(id);
            if (commande == null) return NotFound();

            _context.CommandesFournisseurs.Remove(commande);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
