using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Context;
using Models.Entities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        private readonly ApplicationDBContext _context;

        public AuthController(IConfiguration configuration, ApplicationDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel dto)
        {
            var authUser = await _context.AuthUsers
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Email == dto.Email && a.Active);

            if (authUser == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            var token = GenerateToken(authUser);

            return Ok(new { token });
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel dto)
        {
            if (await _context.AuthUsers.AnyAsync(a => a.Email == dto.Email))
                return BadRequest("El email ya está registrado.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Active = true,
                MemberSince = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var authUser = new AuthUser
            {
                UserId = user.Id,
                Email = dto.Email,
                Password = dto.Password,
                Active = true
            };

            _context.AuthUsers.Add(authUser);
            await _context.SaveChangesAsync();

            var token = GenerateToken(authUser);

            return Ok(new { token });
        }
        
        private string GenerateToken(AuthUser authUser)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").
                Get<JwtSettings>();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, authUser.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, authUser.Email),
                new Claim("userId", authUser.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
    
    public class RegisterModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

}
