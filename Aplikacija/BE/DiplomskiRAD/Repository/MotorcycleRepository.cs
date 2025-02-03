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
            return await _context.Motorcycles.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Motorcycle> GetMotorById(Guid id)
        {
            return await _context.Motorcycles.FindAsync(id);
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

    }
}
