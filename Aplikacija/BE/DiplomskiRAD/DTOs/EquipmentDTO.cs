using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.DTOs
{
    public class EquipmentDTO
    {
        public class EquipmentInfo
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string? Slika { get; set; }

            public EquipmentInfo() { }
            public EquipmentInfo(Guid id, string name, string? slika) //Konstruktor zbog paginacije
            {
                Id = id;
                Name = name;
                Slika = slika;
            }
        }

        public class CreateEquipmentDTO
        {
              public string Name { get; set; }

              public string? Slika { get; set; }

              [JsonConverter(typeof(JsonStringEnumConverter))]
              public EquipmentState EquipmentState { get; set; }

              public double Amount { get; set; }
        }

        public class UpdateEquipmentDTO
        {
            public string Name { get; set; }

            public string? Slika { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public EquipmentState EquipmentState { get; set; }

            public double Amount { get; set; }
        }
    }
}
