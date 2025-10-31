using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _authService.RegisterAsync(data);
            return NoContent(); // vraća 204
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _authService.LoginAsync(data);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var profile = await _authService.GetProfile(User);
            return Ok(profile);
        }
    }
}