using System.ComponentModel.DataAnnotations.Schema;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.Models
{
    public class SparePart
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public IsSpartPartUsed IsSpartPartUsed { get; set; }

        [ForeignKey(nameof(TaskService))]
        public Guid TaskServiceId { get; set; }

        public TaskService TaskService { get; set; }
    }
}
