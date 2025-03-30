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

            var equipment = order.Equipments?.FirstOrDefault();
            if (equipment != null)
            {
                if (equipment.Amount < order.OrderAmount)
                {
                    throw new Exception("nema dovoljno equipment");
                }
                equipment.Amount -= order.OrderAmount;
            }

            var motorcycle = order.Motorcycles?.FirstOrDefault();
            if (motorcycle != null)
            {
                if (motorcycle.Amount < order.OrderAmount)
                {
                    throw new Exception("nema dovoljno motor");
                }
                motorcycle.Amount -= order.OrderAmount; 
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
