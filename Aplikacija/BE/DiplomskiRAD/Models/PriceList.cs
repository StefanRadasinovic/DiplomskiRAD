

using System.Text.Json.Serialization;

namespace DiplomskiRAD.Models
{
    public class PriceList
    {
        public Guid Id { get; set; }

        public double Price { get; set; }   

        public DateTime StartingDate { get; set; }

        public DateTime  EndingDate { get; set; }

        [JsonIgnore]
        public List<Motorcycle> Motorcycles { get; set; }

        [JsonIgnore]
        public List<Equipment> Equipments { get; set; }
    }
}
