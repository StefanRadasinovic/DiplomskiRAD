using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }

        public int numOfPurchases { get; set; } //only for client

        public double Salary { get; set; } //only for director and worker

        public int numOfTasks {  get; set; } //only for worker

    }
}
