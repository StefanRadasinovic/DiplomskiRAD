using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.Models
{
    public class Service
    {
        public Guid Id { get; set; }

        public string FailureDescription { get; set; }

        public string? Picture { get; set; }

        public DateTime StartDate { get; set; } //datum pospeca zahteva 

        public DateTime? EndDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ServiceStatus ServiceStatus { get; set; }

        public string? razlogOdbijanja { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public ICollection<TaskService> TaskServices { get; set; }

        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; }
    }
}
