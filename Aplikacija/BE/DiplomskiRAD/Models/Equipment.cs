using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.Models
{
    public class Equipment
    {
        public Guid Id { get; set; }    

        public string Name { get; set; }

        public string? Slika { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquipmentState EquipmentState { get; set; }

        public double Amount { get; set; }  
    }
}
