using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.DTOs
{
    public class MotorcycleDTO
    {
        public class MotorcycleInfo
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public MotorcycleType MotorcycleType { get; set; }

            public int YearOfProduction { get; set; }

            public string? Slika { get; set; }

            public MotorcycleInfo() { }
            public MotorcycleInfo(Guid id, string name, MotorcycleType motorType, int yearOfProduction, string? slika) //Konstruktor zbog paginacije
            {
                Id = id;
                Name = name;
                MotorcycleType = motorType;
                YearOfProduction = yearOfProduction;
                Slika = slika;
            }
        }

        public class CreateMotorcycleDto
        {

            public string Name { get; set; }

            public string? Slika { get; set; }

            public double Kilometraza { get; set; }

            public int YearOfProduction { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public MotorcycleState MotorcycleState { get; set; }

            public double Amount { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public MotorcycleType MotorcycleType { get; set; }


        }

        public class UpdateMotorcycleDto
        {
            public string Name { get; set; }

            public string? Slika { get; set; }

            public double Kilometraza { get; set; }

            public int YearOfProduction { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public MotorcycleState MotorcycleState { get; set; }

            public double Amount { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public MotorcycleType MotorcycleType { get; set; }

        }
    }
}
