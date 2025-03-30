using DiplomskiRAD.Enums;
using System.Text.Json.Serialization;
using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.MotorcycleDTO;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.DTOs
{
    public class OrderDTO
    {


        public class OrderInfo
        {
            public Guid Id { get; set; }

            public double OrderAmount { get; set; }

            public double TotalPrice { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public OrderStatus OrderStatus { get; set; }

            public List<EquipmentInfo>? EquipmentInfo { get; set; }
            public List<MotorcycleInfo>? MotorcycleInfo { get; set; }

            public UserInfo UserInfo { get; set; }
        }



        public class CreateOrderDto
        {
            public CreateOrderDto(double orderAmount)
            {
                OrderAmount = orderAmount;
            }

            public double OrderAmount { get; set; }


        }
    }
}
