using DiplomskiRAD.Data;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiRAD.Repository
{
    public class SparePartsRepository
    {
        private readonly AppDbContext _context;

        public SparePartsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SparePart>> GetUsedSparePartsByTaskId(Guid taskId)
        {
            return await _context.SpareParts.Where(t => t.TaskServiceId == taskId).ToListAsync();
        }

        public async Task CreateSparePart(SparePart sparePart)
        {
            await _context.SpareParts.AddAsync(sparePart);
            await _context.SaveChangesAsync();
        }
    }
}
