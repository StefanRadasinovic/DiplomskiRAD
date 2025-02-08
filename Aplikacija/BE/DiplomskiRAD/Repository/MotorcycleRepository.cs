using DiplomskiRAD.Data;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DiplomskiRAD.Repository
{
    public class MotorcycleRepository
    {
        private readonly AppDbContext _context;

        public MotorcycleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Motorcycle>> GetWithOffsetPagination(int pageNumber, int pageSize)
        {
            return await _context.Motorcycles
            .Include(m => m.Producers)
            .Include(m => m.PriceLists)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }


        public async Task<Motorcycle> GetMotorById(Guid id)
        {
            return await _context.Motorcycles.Include(m => m.Producers).Include(m => m.PriceLists).FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task CreateMotor(Motorcycle newMotor)
        {
            await _context.Motorcycles.AddAsync(newMotor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMotor(Motorcycle updateMotor)
        {
            _context.Motorcycles.Update(updateMotor);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteMotor(Guid id)
        {
            var motor = await _context.Motorcycles.FindAsync(id);
            if (motor != null)
            {
                _context.Motorcycles.Remove(motor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Motorcycle>> GetFilter(Expression<Func<Motorcycle, bool>> filter)
        {
            return await _context.Motorcycles.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<Motorcycle>> GetAllAsync()
        {
            return await GetFilter(x => true);
        }

        public async Task<List<Motorcycle>> GetMotorByType(string motorType)
        {
            if (!Enum.TryParse<MotorcycleType>(motorType, out var type))
            {
                throw new ArgumentException("Invalid motorcycle type.");
            }

            return await _context.Motorcycles.Where(m => m.MotorcycleType == type).ToListAsync();
        }

        public async Task<IEnumerable<Motorcycle>> GetMotorsByName(string name)
        {
            return await _context.Set<Motorcycle>()
                .Include(m => m.Producers) 
                .Where(m => m.Name.ToLower().Contains(name.ToLower())) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Motorcycle>> GetMotorsByProducerName(string producerName)
        {
            return await _context.Set<Motorcycle>()
                .Include(m => m.Producers)
                .Where(m => m.Producers.Any(p => p.Name.ToLower().Contains(producerName.ToLower()))) 
                .ToListAsync();
        }

    }
}
