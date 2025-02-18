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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<TaskService> TaskServices { get; set; }

        public DbSet<Review> Reviews { get; set; }



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


            modelBuilder.Entity<Order>()
                .HasMany(e => e.Equipments)
                .WithMany(e => e.Orders);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Motorcycles)
                .WithMany(e => e.Orders);

            modelBuilder.Entity<Order>()
                .HasOne(r => r.User)  
                .WithMany(u => u.Orders)  
                .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<Service>()
                .HasOne(r => r.User)
                .WithMany(u => u.Services)
                .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<TaskService>()
                .HasOne(r => r.Service)
                .WithMany(u => u.TaskServices)
                .HasForeignKey(r => r.ServiceId);

            modelBuilder.Entity<TaskService>()
                .HasOne(r => r.User)
                .WithMany(u => u.TaskServices)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<SparePart>()
                .HasOne(r => r.TaskService)
                .WithMany(u => u.SpareParts)
                .HasForeignKey(r => r.TaskServiceId);


            modelBuilder.Entity<Review>()
                .HasOne(r => r.Services)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ServiceId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Users)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);


        }

    }
}
