using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using DiplomskiRAD.ZaMapiranje;
using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.OrderDTO;
using static DiplomskiRAD.DTOs.PriceListDTO;
using static DiplomskiRAD.DTOs.ProducerDTO;

namespace DiplomskiRAD.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly UserRepository _userRepository;
        private readonly MotorcycleRepository _motorcycleRepository;
        private readonly EquipmentRepository _equipmentRepository;

        public OrderService(OrderRepository repository, UserRepository userRepository, MotorcycleRepository motorcycleRepository, EquipmentRepository equipmentRepository)
        {
            _orderRepository = repository;
            _userRepository = userRepository;
            _motorcycleRepository = motorcycleRepository;
            _equipmentRepository = equipmentRepository;
        }


        public async Task<IEnumerable<OrderInfo>> GetAllPendingOrders()
        {
            var orders = await _orderRepository.GetAllPendingOrders();
            return orders.Select(o => o.ToOrderInfo());
        }

        public async Task<OrderInfo?> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            return order?.ToOrderInfo();
        }

        public async Task<IEnumerable<OrderInfo>> GetAllOrdersForUser(Guid userId)
        {
            var orders = await _orderRepository.GetAllOrdersForUser(userId);
            return orders.Select(o => o.ToOrderInfo());
        }


        public async Task<OrderInfo?> CreateOrder(Guid userId, Guid itemId, CreateOrderDto dto)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist.");
            }

            Order order;

            var motorcycle = await _motorcycleRepository.GetMotorById(itemId);
            if (motorcycle != null)
            {
                order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderAmount = dto.OrderAmount,
                    TotalPrice = dto.OrderAmount * (motorcycle.PriceLists?.FirstOrDefault()?.Price ?? 0),
                    OrderStatus = OrderStatus.NA_CEKANJU,
                    Motorcycles = new List<Motorcycle> { motorcycle },
                    User = user
                };
            }
            else
            {
                var equipment = await _equipmentRepository.GetEquipmentById(itemId);
                if (equipment == null)
                {
                    return null;
                }

                order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderAmount = dto.OrderAmount,
                    TotalPrice = (dto.OrderAmount * (equipment.PriceLists?.FirstOrDefault()?.Price ?? 0)),
                    OrderStatus = OrderStatus.NA_CEKANJU,
                    Equipments = new List<Equipment> { equipment },
                    User = user
                };
            }

            await _orderRepository.CreateOrder(order);
            return order.ToOrderInfo();
        }


        public async Task AcceptOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new Exception("OrderId not found");
            }

            var orderedEquipmentAmount = order.Equipments?.FirstOrDefault()?.Amount;
            if (orderedEquipmentAmount != null && orderedEquipmentAmount < order.OrderAmount) 
            {
                throw new Exception("There is not enought available equipment for the order");
            }

  


            var orderedMotorAmount = order.Motorcycles?.FirstOrDefault()?.Amount;
            if (orderedMotorAmount != null && orderedMotorAmount < order.OrderAmount)
            {
                throw new Exception("There is not enought available motors for the order");
            }

            order.OrderStatus = OrderStatus.PRIHVACEN;
            order.User.numOfPurchases++;

            await _orderRepository.UpdateOrder(order);
        }

        public async Task DeclineOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new Exception("OrderId not found");
            }

            order.OrderStatus = OrderStatus.ODBIJEN;
            await _orderRepository.UpdateOrder(order);
        }

    }
}
