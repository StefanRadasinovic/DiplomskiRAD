using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;
using static DiplomskiRAD.DTOs.ProducerDTO;

namespace DiplomskiRAD.DTOs
{
    public class MotorcycleDTO
    {
        public class MotorcycleInfo
        {

            public Guid Id { get; set; }
            public string Name { get; set; }
            public string? Slika { get; set; }
            public double Kilometraza { get; set; } 
            public int YearOfProduction { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public MotorcycleState MotorcycleState { get; set; }  

            public double Amount { get; set; } 

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public MotorcycleType MotorcycleType { get; set; }

            public List<ProducerInfo> Producers { get; set; }

            public MotorcycleInfo() { }

            public MotorcycleInfo(Guid id, string name, string? slika, double kilometraza, int yearOfProduction,
                                  MotorcycleState motorcycleState, double amount, MotorcycleType motorType,
                                  List<ProducerInfo> producers)
            {
                Id = id;
                Name = name;
                Slika = slika;
                Kilometraza = kilometraza;
                YearOfProduction = yearOfProduction;
                MotorcycleState = motorcycleState;
                Amount = amount;
                MotorcycleType = motorType;
                Producers = producers;
            }

            public MotorcycleInfo(Guid id, string name, string? slika, List<ProducerInfo> producerInfos)
            {
                Id = id;
                Name = name;
                Slika = slika;
                Producers = producerInfos;
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

            public List<CreateProducerDto> Producers { get; set; }


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

            public List<UpdateProducerDto> Producers { get; set; }

        }
    }
}
