using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.DTOs
{
    public class UserDTO
    {
        public class UserLoginDto
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class UserRegistrationDto
        {
            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class UserInfo
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public Role Role { get; set; }
        }

        public class DisplayWorkerDto
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Role Role { get; set; }

            public double Salary { get; set; } //only for director and worker

            public int numOfTasks { get; set; } //only for worker

        }

        public class DisplayClientDto
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Role Role { get; set; }

            public int numOfPurchases { get; set; }
        }

        public class DisplayDirectorDto
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Role Role { get; set; }

            public double Salary { get; set; } //only for director and worker
        }

        public class CreateWorkerDtO
        {
           
            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }
            public string Password { get; set; }

            public double Salary { get; set; } //only for director and worker

        }

        public class UpdateWorkerDto
        {
            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            public double Salary { get; set; } //only for director and worker

        }

        public class UpdateClientDto
        {
            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }


        }

        public class UpdateDirectorDto
        {
            public string Name { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            public double Salary { get; set; } //only for director and worker

        }

    }
}
