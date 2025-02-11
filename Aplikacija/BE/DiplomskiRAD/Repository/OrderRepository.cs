using DiplomskiRAD.Data;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiRAD.Repository
{
    public class OrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllPendingOrders()
        {
            return await _context.Orders
                .Include(o => o.Motorcycles).ThenInclude(m => m.Producers)
                .Include(o => o.Equipments).ThenInclude(e => e.Producers)

                .Include(o => o.Motorcycles).ThenInclude(m => m.PriceLists)
                .Include(o => o.Equipments).ThenInclude(e => e.PriceLists)

                .Include(o => o.User)
                .Where(o => o.OrderStatus == OrderStatus.NA_CEKANJU)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            return await _context.Orders

                .Include(o => o.Motorcycles).ThenInclude(m => m.Producers)
                .Include(o => o.Equipments).ThenInclude(e => e.Producers)

                .Include(o => o.Motorcycles).ThenInclude(m => m.PriceLists)
                .Include(o => o.Equipments).ThenInclude(e => e.PriceLists)

                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetAllOrdersForUser(Guid userId)
        {
            return await _context.Orders
                .Include(o => o.Motorcycles).ThenInclude(m => m.Producers)
                .Include(o => o.Equipments).ThenInclude(e => e.Producers)

                .Include(o => o.Motorcycles).ThenInclude(m => m.PriceLists)
                .Include(o => o.Equipments).ThenInclude(e => e.PriceLists)

                .Include(o => o.User)
                .Where(o => o.User.Id == userId)
                .ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
