using DiplomskiRAD.DTOs;
using DiplomskiRAD.Models;
using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.MotorcycleDTO;
using static DiplomskiRAD.DTOs.OrderDTO;
using static DiplomskiRAD.DTOs.ProducerDTO;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.ZaMapiranje
{
    public static class OrderExtensions
    {
        public static OrderInfo ToOrderInfo(this Order order)
        {
            return new OrderInfo
            {
                Id = order.Id,
                OrderAmount = order.OrderAmount,
                TotalPrice = ((order.Equipments?.Sum(e => order.OrderAmount * (e.PriceLists?.FirstOrDefault()?.Price ?? 0)) ?? 0) +
                              (order.Motorcycles?.Sum(m => order.OrderAmount * (m.PriceLists?.FirstOrDefault()?.Price ?? 0)) ?? 0)),
                OrderStatus = order.OrderStatus,


                EquipmentInfo = (order.Equipments ?? new List<Equipment>())
                                .Select(e => new EquipmentInfo(
                                    e.Id,
                                    e.Name,
                                    e.Slika,
                                    e.EquipmentState,
                                    e.Amount,
                                    (e.Producers ?? new List<Producer>()).Select(p => new ProducerInfo(p.Name, p.Description)).ToList(),
                                    e.PriceLists.Select(p => new PriceListDTO.DisplayPriceOnly(p.Price)).ToList()
                                )).ToList(),


                MotorcycleInfo = (order.Motorcycles ?? new List<Motorcycle>())
                                 .Select(m => new MotorcycleInfo(
                                     m.Id,
                                     m.Name,
                                     m.Slika,
                                     m.Kilometraza,
                                     m.YearOfProduction,
                                     m.MotorcycleState,
                                     m.Amount,
                                     m.MotorcycleType,
                                     (m.Producers ?? new List<Producer>()).Select(p => new ProducerInfo(p.Name, p.Description)).ToList(),
                                     (m.PriceLists ?? new List<PriceList>()).Select(p => new PriceListDTO.DisplayPriceOnly(p.Price)).ToList()
                                 )).ToList(),

                UserInfo = new UserInfo
                {
                    Id = order.User.Id,
                    Name = order.User.Name,
                    Surname = order.User.Surname,
                    Username = order.User.Username,
                    Role = order.User.Role
                }
            };
        }
    }
}
