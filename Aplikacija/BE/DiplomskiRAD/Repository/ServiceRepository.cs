using DiplomskiRAD.Data;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DiplomskiRAD.Repository
{
    public class ServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllServicesByUserId(Guid userId)
        {
            return await _context.Services.Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetAllPendingAndInprogressServices()
        {
            return await _context.Services
                                .Where(s => s.ServiceStatus == ServiceStatus.NA_CEKANJU || 
                                       s.ServiceStatus == ServiceStatus.U_TOKU || s.ServiceStatus == ServiceStatus.NA_CEKANJU)
                                .Include(s => s.User)
                                .Include(s => s.TaskServices).ThenInclude(p => p.User)
                                .ToListAsync();
        }


        //OVO CES MORATI DA MENJAS DA IMAS DETALJE I O TASKOVIMA I RADNICIMA KOJI RADE NA NJIMA
        public async Task<Service?> GetServiceById(Guid serviceId)
        {
            return await _context.Services
                                .Include(s => s.User)
                                .Include(s => s.TaskServices).ThenInclude(p=>p.User)
                                .Include(s => s.TaskServices).ThenInclude(p => p.SpareParts)
                                .Include(s => s.Reviews)
                                .FirstOrDefaultAsync(o => o.Id == serviceId);
        }


        public async Task CreateService(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteService(Guid serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}
