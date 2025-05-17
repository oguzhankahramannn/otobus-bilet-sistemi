using Microsoft.EntityFrameworkCore;
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
            var user = await _context.Persons
                .Where(x => x.name == loginUser.name && x.password == loginUser.password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                //throw new ApplicationException("Kullanıcı adı Hatalı");
                   return null;
            }
            else
            {
                var token = await GenerateJwtToken(user);
                var authResponse = new AuthResponse
                {
                    Id = user.p_id,
                    Token = token,
                    Username = user.name,
                };
                return authResponse;
            }
        }


        public async Task<string> GenerateJwtToken(Person person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, person.p_id.ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
