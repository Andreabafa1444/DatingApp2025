using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Extensions;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // 📩 Registrar usuario
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
        {
            if (await EmailExists(request.Email))
                return BadRequest("Email is already taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Devuelve DTO con el token incluido
            return user.ToDto(_tokenService);
        }

       // 🔐 Iniciar sesión
[HttpPost("login")]
public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
{
    var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
    if (user == null) return Unauthorized("Invalid email or password");

    if (user.PasswordSalt == null || user.PasswordHash == null)
        return Unauthorized("Invalid email or password");

    using var hmac = new HMACSHA512(user.PasswordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

    // Comparación segura usando SequenceEqual
    if (!computedHash.SequenceEqual(user.PasswordHash))
        return Unauthorized("Invalid email or password");

    return user.ToDto(_tokenService);
}

        // 🔎 Validar si el correo ya existe
        private async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
