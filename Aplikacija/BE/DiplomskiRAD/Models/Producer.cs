using System.Text.Json.Serialization;

namespace DiplomskiRAD.Models
{
    public class Producer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public List<Equipment> Equipments { get; set; }

        [JsonIgnore]
        public List<Motorcycle> Motorcycles { get; set; }
    }
}
