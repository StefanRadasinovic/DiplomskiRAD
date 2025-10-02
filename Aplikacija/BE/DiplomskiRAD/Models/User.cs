using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;
using Microsoft.Extensions.Hosting;

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

        public int numOfPurchases { get; set; } // client

        public double? Salary { get; set; } // director i radnik

        public int numOfTasks {  get; set; } //radnik


        public ICollection<Order> Orders { get; set; }

        public ICollection<Service> Services { get; set; }

        public ICollection<TaskService> TaskServices { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
