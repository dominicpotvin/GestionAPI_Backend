using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;

namespace GestEase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockProduitController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockProduitController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockProduit>>> GetStockProduits()
        {
            return await _context.StockProduits
                .Include(s => s.Produit)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockProduit>> GetStockProduit(int id)
        {
            var stock = await _context.StockProduits
                .Include(s => s.Produit)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null)
                return NotFound();

            return stock;
        }

        [HttpPost]
        public async Task<ActionResult<StockProduit>> CreateStockProduit(StockProduit stock)
        {
            _context.StockProduits.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStockProduit), new { id = stock.Id }, stock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStockProduit(int id, StockProduit stock)
        {
            if (id != stock.Id)
                return BadRequest();

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.StockProduits.Any(s => s.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockProduit(int id)
        {
            var stock = await _context.StockProduits.FindAsync(id);
            if (stock == null)
                return NotFound();

            _context.StockProduits.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}