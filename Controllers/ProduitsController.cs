using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestEase.Data;
using GestEase.Models;

namespace GestEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProduitsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            try
            {
                return await _context.Produits
                    .Include(p => p.Fournisseur) // si Fournisseur existe
                    .Include(p => p.Dimensions)  // si Dimensions existe
                    .Where(p => p.Description != null && p.Description.Trim() != "") // filtre minimal
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur serveur: {ex.Message}");
                return StatusCode(500, $"Erreur lors de la récupération des produits : {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Produit>> GetProduit(int id)
        {
            try
            {
                var produit = await _context.Produits.FindAsync(id);
                if (produit == null)
                    return NotFound();

                return produit;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur lors de GetProduit: {ex.Message}");
                return StatusCode(500, "Erreur lors de la récupération du produit.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            try
            {
                _context.Produits.Add(produit);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduit), new { id = produit.Id }, produit);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur lors de la création du produit: {ex.Message}");
                return StatusCode(500, "Erreur lors de la création du produit.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.Id)
                return BadRequest();

            _context.Entry(produit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Produits.Any(p => p.Id == id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur lors de la mise à jour du produit: {ex.Message}");
                return StatusCode(500, "Erreur lors de la mise à jour du produit.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            try
            {
                var produit = await _context.Produits.FindAsync(id);
                if (produit == null)
                    return NotFound();

                _context.Produits.Remove(produit);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur lors de la suppression du produit: {ex.Message}");
                return StatusCode(500, "Erreur lors de la suppression du produit.");
            }
        }
    }
}
