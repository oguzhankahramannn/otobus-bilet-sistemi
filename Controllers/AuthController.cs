using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Dtos;
using OtobusBiletiApp.Services;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _authService.Login(loginUser);
            if (result.Id != 0)
                return Ok(result);

            return Unauthorized("Kullanıcı adı veya şifre yanlış.");
        }
    }
}
