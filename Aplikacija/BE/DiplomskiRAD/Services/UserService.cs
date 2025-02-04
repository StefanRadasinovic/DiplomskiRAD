using System.Text.Json.Serialization;
using DiplomskiRAD.DTOs;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UserService(UserRepository repository, TokenService tokenService)
        {
            _userRepository = repository;
            _tokenService = tokenService;
        }

        public async Task Register(UserDTO.UserRegistrationDto userDto)
        {
            if (await _userRepository.GetUserByUsername(userDto.Username) != null)
            {
                throw new Exception("Username already exists.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                Name = userDto.Name,
                Surname = userDto.Surname,
                Password = _tokenService.HashPassword(userDto.Password),
                Role = Role.KLIJENT,
                numOfPurchases = 0
            };

            await _userRepository.CreateUser(user);
        }

        public async Task<string> Login(UserDTO.UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByUsername(userLoginDto.Username);
            if (user == null || !_tokenService.VerifyPassword(userLoginDto.Password, user.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            return _tokenService.GenerateToken(user);
        }

        public async Task<IEnumerable<UserDTO.UserInfo>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return users.Select(user => new UserInfo
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            });
        }

        public async Task<object> GetUserById(Guid id) //object handles multiple DTOs
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("User doesn't exist.");
            }

            return user.Role switch
            {
                Role.KLIJENT => new DisplayClientDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.Username,
                    Role = user.Role,
                    numOfPurchases = user.numOfPurchases
                },
                Role.RADNIK => new DisplayWorkerDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.Username,
                    Role = user.Role,
                    Salary = user.Salary,
                    numOfTasks = user.numOfTasks,
                },
                Role.DIREKTOR => new DisplayDirectorDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.Username,
                    Role = user.Role,
                    Salary = user.Salary
                },
                _ => throw new Exception("Invalid role.") 
            };
        }

        public async Task CreateRadnik(User user)
        {
            var existingUser = await _userRepository.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }
            user.Id = Guid.NewGuid();
            user.Name = user.Username;
            user.Surname = user.Surname;
            user.Password = _tokenService.HashPassword(user.Password);
            user.Role = Role.RADNIK;
            user.Salary = user.Salary;
            user.numOfTasks = 0;

            await _userRepository.CreateUser(user);
        }

        public async Task UpdateUser(User user)
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
               user.Password = _tokenService.HashPassword(user.Password);
            }

            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(Guid id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
