using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;

namespace GestEase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandesClientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommandesClientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandeClient>>> GetCommandesClient()
        {
            return await _context.CommandesClients
                .Include(c => c.Client)
                .Include(c => c.Projet)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeClient>> GetCommandeClient(int id)
        {
            var commande = await _context.CommandesClients
                .Include(c => c.Client)
                .Include(c => c.Projet)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (commande == null)
                return NotFound();

            return commande;
        }

        [HttpPost]
        public async Task<ActionResult<CommandeClient>> PostCommandeClient(CommandeClient commande)
        {
            _context.CommandesClients.Add(commande);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCommandeClient), new { id = commande.Id }, commande);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandeClient(int id, CommandeClient commande)
        {
            if (id != commande.Id)
                return BadRequest();

            _context.Entry(commande).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommandeClient(int id)
        {
            var commande = await _context.CommandesClients.FindAsync(id);
            if (commande == null)
                return NotFound();

            _context.CommandesClients.Remove(commande);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
