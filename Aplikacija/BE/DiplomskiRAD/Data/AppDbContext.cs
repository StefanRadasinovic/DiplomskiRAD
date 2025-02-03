using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiRAD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
    }
}
