using GestEase.Data;
using GestEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class UtilisateursController : ControllerBase
{
    private readonly AppDbContext _context;

    public UtilisateursController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAll()
    {
        return await _context.Utilisateurs.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Utilisateur>> GetById(int id)
    {
        var user = await _context.Utilisateurs.FindAsync(id);
        return user == null ? NotFound() : user;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Initiales == login.Initiales && u.MotDePasse == login.MotDePasse && u.Actif);

        if (user == null) return Unauthorized(new { message = "Identifiants invalides." });

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<Utilisateur>> Create(Utilisateur user)
    {
        _context.Utilisateurs.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
}
