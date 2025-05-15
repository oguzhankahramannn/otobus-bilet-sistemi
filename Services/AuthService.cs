using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OtobusBiletiApp.Dtos;
using OtobusBiletiApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OtobusBiletiApp.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Login(LoginUser loginUser)
        {
            var user = _context.Persons.FirstOrDefault(x => x.name == loginUser.name && x.password == loginUser.passwordHash);
            if (user == null)
                return new AuthResponse();

            var token = GenerateJwtToken(user);
            return new AuthResponse
            {
                Id = user.p_id,
                Username = user.name,
                Token = token
            };
        }

        private string GenerateJwtToken(Person user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.p_id.ToString()),
                new Claim(ClaimTypes.Name, user.name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
