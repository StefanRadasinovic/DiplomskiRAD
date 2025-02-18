using DiplomskiRAD.Data;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiRAD.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByRole(string role)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Role.ToString() == role);
        }

        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllWorkers()
        {
            return await _context.Users
                .Where(u => u.Role == Role.RADNIK)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllFreeUsersForTask(Guid taskId)
        {
            var declinedUsers = await _context.Users
                .Where(u => u.TaskServices.Any(ts => ts.Id == taskId && ts.Status == ServiceStatus.ODBIJEN))
                .Select(u => u.Id)
                .ToListAsync();

            return await _context.Users
                .Where(u => u.Role == Role.RADNIK
                            && !u.TaskServices.Any(ts => ts.Id == taskId)  
                            && !declinedUsers.Contains(u.Id)) 
                .Include(u => u.TaskServices)
                .ToListAsync();
        }


        public async Task<IEnumerable<User>> GetAllDeclineUsersForTask(Guid taskId)
        {
            return await _context.Users
                .Where(u => u.TaskServices.Any(ts => ts.Id == taskId && ts.Status == ServiceStatus.ODBIJEN))
                .Include(u => u.TaskServices)
                .ToListAsync();
        }
    }
}
