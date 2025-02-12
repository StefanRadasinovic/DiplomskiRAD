using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomskiRAD.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public int Grade { get; set; }

        [ForeignKey(nameof(Service))]
        public Guid ServiceId { get; set; }

        public Service Services { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User Users { get; set; }
    }
}
