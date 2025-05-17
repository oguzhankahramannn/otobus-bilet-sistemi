using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Dtos;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Services;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly AppDbContext _context;

        public AuthController(AuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        // Kullanıcı Girişi
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _authService.Login(loginUser);

            if (result == null)
                return BadRequest("Kullanıcı adı veya şifre yanlış");

            return Ok(result); // JWT veya mock token döner
        }

        // Admin Girişi
        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin([FromBody] LoginUser loginUser)
        {
            var person = _context.Persons.FirstOrDefault(p =>
                p.email == loginUser.name && p.password == loginUser.password);

            if (person == null)
                return Unauthorized(new { message = "Kullanıcı adı veya şifre yanlış" });

            var isAdmin = _context.Admins.Any(a => a.p_id == person.p_id);
            if (!isAdmin)
                return Unauthorized(new { message = "Bu kişi admin değil" });

            return Ok(new AuthResponse
            {
                Username = person.name + " " + person.surname,
                Token = "admin_" + person.p_id
            });
        }
    }
}
