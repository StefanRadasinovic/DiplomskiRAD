using DiplomskiRAD.DTOs;
using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDTO.UserRegistrationDto userRegistrationDto)
        {
            try
            {
                await _userService.Register(userRegistrationDto);
                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDTO.UserLoginDto userLoginDto)
        {
            try
            {
                var token = await _userService.Login(userLoginDto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
