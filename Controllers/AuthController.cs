using NOROFF_ASPNET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace NOROFF_ASPNET.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request) //change this to use DTO instead
        {
            if (await _authService.ValidateUserAsync(request.Username, request.Password))
            {
                var user = await _authService.GetUserByUsernameAsync(request.Username);
                var token = _authService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid username or password");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)   //change this to use DTO instead
        {
            if (await _authService.RegisterUserAsync(request.Username, request.Password))
            {
                return Ok("Registration successful");
            }
            return BadRequest("Username already exists");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }




}






