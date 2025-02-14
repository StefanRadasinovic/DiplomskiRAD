using DiplomskiRAD.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DiplomskiRAD.Models
{
    public class TaskService
    {
        public Guid Id { get; set; }

        public string TaskDescription { get; set; }

        public DateTime? EndDateTask { get; set; }

        public ServiceStatus Status { get; set; }

        public string? razlogOdbijanja { get; set; }

        [ForeignKey(nameof(Service))]
        public Guid ServiceId { get; set; }

        public Service Service { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; } //radnikID

        public User User { get; set; }

        [JsonIgnore]
        public ICollection<SparePart> SpareParts { get; set; }
    }
}
