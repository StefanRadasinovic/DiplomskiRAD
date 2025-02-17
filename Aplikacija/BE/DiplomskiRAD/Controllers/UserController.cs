using System.Text.Json;
using DiplomskiRAD.DTOs;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserRepository _userRepository;

        public UserController(UserService service, UserRepository userRepository)
        {
            _userService = service;
            _userRepository = userRepository;
        }


        [Authorize(Roles = "DIREKTOR")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO.UserInfo>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetUserById(Guid id) //object handles multiple DTOs
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize(Roles = "DIREKTOR")]
        [HttpPost]
        public async Task<ActionResult> CreateRadnik([FromBody] CreateWorkerDtO createWorkerDtO)
        {
            if (createWorkerDtO == null)
            {
                return BadRequest("User data is required");
            }

            var user = new User
            {
                Name = createWorkerDtO.Name,
                Surname = createWorkerDtO.Surname,
                Username = createWorkerDtO.Username,
                Password = createWorkerDtO.Password,
                Salary = createWorkerDtO.Salary,
            };

            await _userService.CreateRadnik(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] JsonElement updateDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUser(id, updateDto);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "DIREKTOR")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [Authorize(Roles = "DIREKTOR")]
        [HttpGet("workers")]
        public async Task<ActionResult<IEnumerable<UserDTO.UserInfo>>> GetAllWorkers()
        {
            var users = await _userService.GetAllWorkers();
            return Ok(users);
        }

        
        [HttpGet("free-workers/{taskId}")]
        public async Task<ActionResult<IEnumerable<UserDTO.UserInfo>>> GetAllFreeUsersForTask(Guid taskId)
        {
            var user = await _userService.GetAllFreeUsersForTask(taskId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

    }
}
