using DiplomskiRAD.Data;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiRAD.Repository
{
    public class ProducerRepository
    {

        private readonly AppDbContext _context;

        public ProducerRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Producer> GetProducerById(Guid id)
        {
            return await _context.Producers.FindAsync(id);
        }

        public async Task<Producer> GetProducerByName(string name)
        {
            return await _context.Producers.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }

        public async Task CreateProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();
        }

    }
}
