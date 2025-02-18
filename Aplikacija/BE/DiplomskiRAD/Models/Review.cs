using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DiplomskiRAD.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public int Grade { get; set; }

        [ForeignKey(nameof(Service))]
        public Guid ServiceId { get; set; }

        [JsonIgnore]
        public Service Services { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User Users { get; set; }
    }
}
