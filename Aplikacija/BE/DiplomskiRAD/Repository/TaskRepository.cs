using DiplomskiRAD.Data;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiRAD.Repository
{
    public class TaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<TaskService?> GetTaskById(Guid taskId)
        {
            return await _context.TaskServices
                .Include(t => t.User)          
                .Include(t => t.SpareParts)    
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<List<TaskService>> GetTasksByUserId(Guid userId)
        {
            return await _context.TaskServices
                .Include(t => t.User)
                .Include(t => t.SpareParts)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task CreateTask(TaskService task)
        {
            await _context.TaskServices.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTask(TaskService task)
        {
            _context.TaskServices.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskService>> GetAllInProgressDeclinedTasks()
        {
            return await _context.TaskServices
                 .Include(t => t.User)
                 .Include(t => t.SpareParts)
                 .Where(t => t.Status == ServiceStatus.ODBIJEN || t.Status == ServiceStatus.U_TOKU).ToListAsync();
        }

        public async Task<List<TaskService>> GetAllTasksForService(Guid serviceId)
        {
            return await _context.TaskServices
                .Include(t => t.User)
                .Include(t => t.SpareParts)
                .Where(t => t.ServiceId == serviceId).ToListAsync();
        }

        /*
        public async Task<List<TaskService>> GetTasksByStatus(ServiceStatus status)
        {
            return await _context.TaskServices.Where(t => t.Status == status).ToListAsync();
        }
        */
    }
}
