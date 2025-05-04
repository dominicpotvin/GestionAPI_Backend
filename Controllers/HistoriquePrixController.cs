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
    public class HistoriquePrixController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoriquePrixController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/HistoriquePrix
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoriquePrix>>> GetHistoriquePrix()
        {
            return await _context.HistoriquePrix
                                 .Include(h => h.Produit)
                                 .ToListAsync();
        }

        // GET: api/HistoriquePrix/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoriquePrix>> GetHistoriquePrix(int id)
        {
            var historique = await _context.HistoriquePrix
                                           .Include(h => h.Produit)
                                           .FirstOrDefaultAsync(h => h.Id == id);

            if (historique == null)
                return NotFound();

            return historique;
        }

        // POST: api/HistoriquePrix
        [HttpPost]
        public async Task<ActionResult<HistoriquePrix>> PostHistoriquePrix(HistoriquePrix historiquePrix)
        {
            _context.HistoriquePrix.Add(historiquePrix);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistoriquePrix), new { id = historiquePrix.Id }, historiquePrix);
        }

        // PUT: api/HistoriquePrix/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoriquePrix(int id, HistoriquePrix historiquePrix)
        {
            if (id != historiquePrix.Id)
                return BadRequest();

            _context.Entry(historiquePrix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.HistoriquePrix.Any(h => h.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/HistoriquePrix/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoriquePrix(int id)
        {
            var historique = await _context.HistoriquePrix.FindAsync(id);
            if (historique == null)
                return NotFound();

            _context.HistoriquePrix.Remove(historique);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
