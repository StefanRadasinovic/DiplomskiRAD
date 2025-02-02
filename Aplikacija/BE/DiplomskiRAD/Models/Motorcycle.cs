using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.Models
{
    public class Motorcycle
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Slika { get; set; }

        public double Kilometraza { get; set; }

        public int YearOfProduction {  get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MotorcycleState MotorcycleState { get; set; }

        public double Amount {  get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MotorcycleType MotorcycleType { get; set; }
    }
}
