using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalRPractice.DTO;
using SignalRPractice.Service;

namespace SignalRPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly TokenService _tokenService;

        public AuthController(UserManager<IdentityUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var data = await _userManager.FindByEmailAsync(user.email);

            if (data is not null)
            {
                bool valid = await _userManager.CheckPasswordAsync(data, user.password);

                if (!valid)
                {
                    return BadRequest("Invalid Password!");
                }

                string token = _tokenService.GenerateToken(data, await _userManager.GetRolesAsync(data));

                return Ok(new { Token = token });
            }

            return NotFound("User Not Found!");
        }

    }
}
