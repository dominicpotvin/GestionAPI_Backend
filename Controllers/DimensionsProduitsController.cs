using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;

namespace GestEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimensionsProduitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DimensionsProduitsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimensionProduit>>> GetDimensionsProduits()
        {
            return await _context.DimensionsProduits.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DimensionProduit>> GetDimensionProduit(int id)
        {
            var dimension = await _context.DimensionsProduits.FindAsync(id);
            if (dimension == null)
                return NotFound();

            return dimension;
        }

        [HttpPost]
        public async Task<ActionResult<DimensionProduit>> PostDimensionProduit(DimensionProduit dimension)
        {
            _context.DimensionsProduits.Add(dimension);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDimensionProduit), new { id = dimension.Id }, dimension);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimensionProduit(int id, DimensionProduit dimension)
        {
            if (id != dimension.Id)
                return BadRequest();

            _context.Entry(dimension).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DimensionsProduits.Any(d => d.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimensionProduit(int id)
        {
            var dimension = await _context.DimensionsProduits.FindAsync(id);
            if (dimension == null)
                return NotFound();

            _context.DimensionsProduits.Remove(dimension);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
