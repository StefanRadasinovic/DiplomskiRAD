using System.Linq.Expressions;
using DiplomskiRAD.Data;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiRAD.Repository
{
    public class EquipmentRepository
    {
        private readonly AppDbContext _context;

        public EquipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipment>> GetWithOffsetPagination(int pageNumber, int pageSize)
        {
            return await _context.Equipments.AsNoTracking()
                .Include(e=>e.Producers)
                .Include(m => m.PriceLists)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Equipment> GetEquipmentById(Guid id)
        {
            return await _context.Equipments.Include(e=>e.Producers).Include(m => m.PriceLists).FirstAsync(m => m.Id == id);
        }

        public async Task CreateEquipment(Equipment newEquipment)
        {
            await _context.Equipments.AddAsync(newEquipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEquipment(Equipment updateEquipment)
        {
            _context.Equipments.Update(updateEquipment);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteEquipment(Guid id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment != null)
            {
                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Equipment>> GetFilter(Expression<Func<Equipment, bool>> filter)
        {
            return await _context.Equipments.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await GetFilter(x => true);
        }


        public async Task<IEnumerable<Equipment>> GetEquipmentByName(string name)
        {
            return await _context.Set<Equipment>()
                .Include(m => m.Producers) 
                .Where(m => m.Name.ToLower().Contains(name.ToLower())) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentByProducerName(string producerName)
        {
            return await _context.Set<Equipment>()
                .Include(m => m.Producers)
                .Where(m => m.Producers.Any(p => p.Name.ToLower().Contains(producerName.ToLower()))) 
                .ToListAsync();
        }

        //kombinacija name+producerName
        public async Task<IEnumerable<Equipment>> GetEquipmentByNameAndProducerName(string name, string producerName) 
        {
            return await _context.Set<Equipment>()
                .Include(m => m.Producers)
                .Where(m => (string.IsNullOrEmpty(name) || m.Name.ToLower().Contains(name.ToLower())) &&
                            (string.IsNullOrEmpty(producerName) || m.Producers.Any(p => p.Name.ToLower().Contains(producerName.ToLower()))))
                .ToListAsync();
        }
    }
}
