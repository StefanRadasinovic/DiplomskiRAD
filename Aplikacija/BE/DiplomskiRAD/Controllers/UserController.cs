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
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] object userUpdateDto)
        {
            if (userUpdateDto == null)
            {
                return BadRequest("User data is required");
            }

            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            switch (user.Role)
            {
                case Role.KLIJENT:
                    var updateUserDto = JsonSerializer.Deserialize<UpdateClientDto>(userUpdateDto.ToString());
                    user.Name = updateUserDto.Name ?? user.Name;
                    user.Surname = updateUserDto.Surname ?? user.Surname;
                    user.Username = updateUserDto.Username ?? user.Username;
                    user.Password = updateUserDto.Password ?? user.Password;
                    break;

                case Role.RADNIK:
                    var updateWorkerDto = JsonSerializer.Deserialize<UpdateWorkerDto>(userUpdateDto.ToString());
                    user.Name = updateWorkerDto.Name ?? user.Name;
                    user.Surname = updateWorkerDto.Surname ?? user.Surname;
                    user.Username = updateWorkerDto.Username ?? user.Username;
                    user.Password = updateWorkerDto.Password ?? user.Password;
                    user.Salary = updateWorkerDto.Salary != default ? updateWorkerDto.Salary : user.Salary;
                    break;

                case Role.DIREKTOR:
                    var updateDirectorDto = JsonSerializer.Deserialize<UpdateDirectorDto>(userUpdateDto.ToString());
                    user.Name = updateDirectorDto.Name ?? user.Name;
                    user.Surname = updateDirectorDto.Surname ?? user.Surname;
                    user.Username = updateDirectorDto.Username ?? user.Username;
                    user.Password = updateDirectorDto.Password ?? user.Password;
                    user.Salary = updateDirectorDto.Salary != default ? updateDirectorDto.Salary : user.Salary;
                    break;

                default:
                    return BadRequest("Invalid role");
            }

            await _userService.UpdateUser(user);

            return NoContent();
        }

        [Authorize(Roles = "DIREKTOR")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

    }
}
