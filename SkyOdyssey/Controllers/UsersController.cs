using Microsoft.AspNetCore.Mvc;
using SkyOdyssey.DTOs;
using SkyOdyssey.Services;
using System.Threading.Tasks;

namespace SkyOdyssey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userService.Register(registerUserDto);
            if (user == null)
                return BadRequest("Username is already taken.");

            return Ok(user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var user = await _userService.Authenticate(loginDto.Username, loginDto.Password);
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
    }
}
