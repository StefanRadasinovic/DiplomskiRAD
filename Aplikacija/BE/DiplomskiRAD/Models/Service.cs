using System.ComponentModel.DataAnnotations.Schema;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.Models
{
    public class Service
    {
        public Guid Id { get; set; }

        public string ServiceDescription { get; set; }

        public string? Picture { get; set; }

        public DateTime StartDateService { get; set; }

        public DateTime? EndDateService { get; set; }

        public ServiceStatus ServiceStatus { get; set; }

        public string? razlogOdbijanja { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<TaskService> TaskServices { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
