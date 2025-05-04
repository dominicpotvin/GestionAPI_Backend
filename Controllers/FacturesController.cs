using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestEase.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facture>>> GetFactures()
        {
            return await _context.Set<Facture>()
                .Include(f => f.Client)
                .Include(f => f.Commande)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Facture>> GetFacture(int id)
        {
            var facture = await _context.Set<Facture>()
                .Include(f => f.Client)
                .Include(f => f.Commande)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (facture == null)
                return NotFound();

            return facture;
        }

        [HttpPost]
        public async Task<ActionResult<Facture>> CreateFacture(Facture facture)
        {
            // Validation : si statut "Payée", la date_paiement ne doit pas être null
            if (facture.Statut == "Payée" && facture.DatePaiement == null)
            {
                return BadRequest("La date de paiement est requise si la facture est marquée comme payée.");
            }

            _context.Set<Facture>().Add(facture);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFacture), new { id = facture.Id }, facture);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacture(int id, Facture facture)
        {
            if (id != facture.Id)
                return BadRequest();

            if (facture.Statut == "Payée" && facture.DatePaiement == null)
            {
                return BadRequest("La date de paiement est requise si la facture est marquée comme payée.");
            }

            _context.Entry(facture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<Facture>().Any(f => f.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacture(int id)
        {
            var facture = await _context.Set<Facture>().FindAsync(id);
            if (facture == null)
                return NotFound();

            _context.Set<Facture>().Remove(facture);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
