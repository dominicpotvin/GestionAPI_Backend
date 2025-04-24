using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;

namespace GestEase.Controllers
{
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
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeFournisseur>> GetCommandeFournisseur(int id)
        {
            var commande = await _context.CommandesFournisseurs
                .Include(c => c.Fournisseur)
                .Include(c => c.Projet)
                .FirstOrDefaultAsync(c => c.Id == id);

            return commande == null ? NotFound() : commande;
        }

        [HttpPost]
        public async Task<ActionResult<CommandeFournisseur>> PostCommandeFournisseur(CommandeFournisseur commande)
        {
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
