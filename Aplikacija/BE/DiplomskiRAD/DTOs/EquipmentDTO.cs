using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;
using static DiplomskiRAD.DTOs.ProducerDTO;

namespace DiplomskiRAD.DTOs
{
    public class EquipmentDTO
    {
        public class EquipmentInfo
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string? Slika { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public EquipmentState EquipmentState { get; set; }

            public double Amount { get; set; }

            public List<ProducerInfo> Producers { get; set; }

            public List<PriceListDTO.DisplayPriceOnly> DisplayPriceOnly { get; set; } //samo za cenu

            public EquipmentInfo() { }

            public EquipmentInfo(Guid id, string name, string? slika,EquipmentState equipmentState, double amount,List<ProducerInfo> producers,
                                List<PriceListDTO.DisplayPriceOnly> displayPriceOnly) 
            {
                Id = id;
                Name = name;
                Slika = slika;
                EquipmentState = equipmentState;
                Amount = amount;
                Producers = producers;
                DisplayPriceOnly = displayPriceOnly;
            }

            public EquipmentInfo(Guid id, string name, string? slika, EquipmentState equipmentState, double amount, List<ProducerInfo> producers)
            {
                Id = id;
                Name = name;
                Slika = slika;
                EquipmentState = equipmentState;
                Amount = amount;
                Producers = producers;
            }

            public EquipmentInfo(Guid id, string name, string? slika, List<ProducerInfo> producers) //Konstruktor zbog paginacije
            {
                Id = id;
                Name = name;
                Slika = slika;
                Producers = producers;
            }
        }

        public class CreateEquipmentDTO
        {
              public string Name { get; set; }

              public string? Slika { get; set; }

              [JsonConverter(typeof(JsonStringEnumConverter))]
              public EquipmentState EquipmentState { get; set; }

              public double Amount { get; set; }

              public List<CreateProducerDto> Producers { get; set; }


        }

        public class UpdateEquipmentDTO
        {
            public string Name { get; set; }

            public string? Slika { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public EquipmentState EquipmentState { get; set; }

            public double Amount { get; set; }

            public List<UpdateProducerDto> Producers { get; set; }
        }
    }
}
