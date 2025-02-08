using DiplomskiRAD.Data;
<<<<<<< Updated upstream
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> Stashed changes

namespace DiplomskiRAD.Repository
{
    public class PriceListRepository
    {
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        private readonly AppDbContext _context;

        public PriceListRepository(AppDbContext context)
        {
            _context = context;
        }
<<<<<<< Updated upstream


        public async Task<PriceList?> GetPriceListById(Guid id)
        {
            return await _context.PriceLists
                                .Include(p => p.Motorcycles)
                                .ThenInclude(m => m.Producers) 
                                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PriceList>> GetAllPricesListByMotorId(Guid motorcycleId)
        {
            return await _context.PriceLists
                                    .Where(p => p.Motorcycles.Any(m => m.Id == motorcycleId))
                                    .OrderBy(p => p.StartingDate)
                                    .ToListAsync();
        }

        public async Task<PriceList?> GetCurrentPriceListByMotorId(Guid motorcycleId)
        {
            return await _context.PriceLists
                            .Where(p => p.Motorcycles.Any(m => m.Id == motorcycleId) &&
                            p.StartingDate <= DateTime.UtcNow && p.EndingDate >= DateTime.UtcNow)
                            .OrderByDescending(p => p.StartingDate)
                            .FirstOrDefaultAsync();
        }

        public async Task CreatePriceList(PriceList priceList)
        {
            await _context.PriceLists.AddAsync(priceList);
            await _context.SaveChangesAsync();
        }

        /*
        public async Task UpdatePriceList(PriceList priceList)
        {
            _context.PriceLists.Update(priceList);
            await _context.SaveChangesAsync();
        }
        */

=======
>>>>>>> Stashed changes
    }
}
