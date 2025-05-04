using GestEase.Data;
using GestEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GestEase.DTO;




[ApiController]
[Route("api/[controller]")]
public class UtilisateursController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UtilisateursController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
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
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            Console.WriteLine($"🔐 Tentative de connexion avec : {request.Initiales} / {request.MotDePasse}");

            var utilisateur = _context.Utilisateurs
                .FirstOrDefault(u =>
                    u.Initiales.ToLower().Trim() == request.Initiales.ToLower().Trim() &&
                    u.MotDePasse.Trim() == request.MotDePasse.Trim());

            if (utilisateur == null)
            {
                Console.WriteLine("❌ Identifiants invalides");
                return Unauthorized("Identifiants invalides");
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, utilisateur.Initiales),
            new Claim(ClaimTypes.Role, utilisateur.Role ?? "User")
        };

            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                return StatusCode(500, "Clé JWT manquante dans appsettings.json");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                user = new { utilisateur.Id, utilisateur.Initiales, utilisateur.Role }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Erreur interne : {ex.Message}");
            return StatusCode(500, $"Erreur interne : {ex.Message}");
        }
    }


    [HttpPost]
    public async Task<ActionResult<Utilisateur>> Create(Utilisateur user)
    {
        _context.Utilisateurs.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
}
