using Microsoft.AspNetCore.Mvc;
using MobileProviderAPI.Helpers;
using MobileProviderAPI.Models;

namespace MobileProviderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Here the username and password must be verified.
            if (login.Username == "admin" && login.Password == "password")
            {
                var token = JwtHelper.GenerateJwtToken(_configuration["JwtSettings:Secret"]);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}
