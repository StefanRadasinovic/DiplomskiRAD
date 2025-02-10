using DiplomskiRAD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace DiplomskiRAD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public DbSet<PriceList> PriceLists { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Equipments)
                .WithMany(e => e.Producers);

            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Motorcycles)
                .WithMany(e => e.Producers);

            modelBuilder.Entity<PriceList>()
               .HasMany(e => e.Equipments)
               .WithMany(e => e.PriceLists);

            modelBuilder.Entity<PriceList>()
                .HasMany(e => e.Motorcycles)
                .WithMany(e => e.PriceLists);
        }

    }
}
