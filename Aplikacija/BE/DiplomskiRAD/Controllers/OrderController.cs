using DiplomskiRAD.Models;
using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;
using static DiplomskiRAD.DTOs.OrderDTO;
using static DiplomskiRAD.DTOs.PriceListDTO;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }


        //GETAllPendingOrders(userID) za zahteve koji su PENDING(na cekanju) - display along orderDetails + motorName+ProducerName+item.Amount+TotalPrice
        //GETOrdersById(orderId) - display also th
        //GETAllOrdersForUser(userId) - svi poslati zahtevi - kartice sa slikom iznad pise ime+proizvodjac ispod pise status

        //CreateOrder(userId,motorId ili EquipmentId)-isto ko i za priceList Obe opcije
        //DeclineOrder(orderId) stavi mu status-ODBIJEN
        //AcceptOrder(orderId) stavi mu status - PRIHVACEN

        [HttpGet("pending")]
        public async Task<ActionResult<OrderInfo>> GetAllPendingOrders() 
        {

            var orders = await _orderService.GetAllPendingOrders();
            if (!orders.Any())
            {
                return NotFound("No current  pending orders ");
            }

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderInfo>> GetOrderById(Guid orderId)
        {
            var existingOrder = await _orderService.GetOrderById(orderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            return Ok(existingOrder);

        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderInfo>>> GetAllOrdersForUser(Guid userId)
        { 

            var existingUserOrder = await _orderService.GetAllOrdersForUser(userId);
            if (!existingUserOrder.Any())
            {
                return NotFound();
            }

            return Ok(existingUserOrder);

        }

     
        [HttpPost]
        public async Task<ActionResult> CreateOrder(Guid userId, Guid itemId, [FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
            {
                return BadRequest("createOrderDto data is required");
            }

            var order = await _orderService.CreateOrder(userId, itemId, createOrderDto);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = order.Id }, order);
        }
       

        [HttpPut("accept/{orderId}")]
        public async Task<ActionResult> AcceptOrder(Guid orderId)
        {
            await _orderService.AcceptOrder(orderId);
            return NoContent();
        }

        [HttpPut("decline/{orderId}")]
        public async Task<ActionResult> DeclineOrder(Guid orderId)
        {
            await _orderService.DeclineOrder(orderId);
            return NoContent();
        }


        
    }
}
