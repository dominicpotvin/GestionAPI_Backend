using GestEase.Data;
using GestEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace GestEase.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DessinsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DessinsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Dessins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dessin>>> GetDessins()
        {
            return await _context.Dessins.ToListAsync();
        }

        // GET: api/Dessins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dessin>> GetDessin(int id)
        {
            var dessin = await _context.Dessins.FindAsync(id);
            if (dessin == null)
                return NotFound();
            return dessin;
        }

        // POST: api/Dessins
        [HttpPost]
        public async Task<ActionResult<Dessin>> PostDessin(Dessin dessin)
        {
            _context.Dessins.Add(dessin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDessin), new { id = dessin.Id }, dessin);
        }

        // PUT: api/Dessins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDessin(int id, Dessin dessin)
        {
            if (id != dessin.Id)
                return BadRequest();

            _context.Entry(dessin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dessins.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Dessins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDessin(int id)
        {
            var dessin = await _context.Dessins.FindAsync(id);
            if (dessin == null)
                return NotFound();

            _context.Dessins.Remove(dessin);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
