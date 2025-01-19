using Microsoft.EntityFrameworkCore;
using OnlineShoppingPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.Context
{
    public class OnlineShoppingPlatformDbContext : DbContext
    {
        //db context sınıfımı oluşturup tabloları ekliyorum.

        public OnlineShoppingPlatformDbContext(DbContextOptions<OnlineShoppingPlatformDbContext> options) : base(options) 
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<Setting>().HasData(new Setting
            {
                Id = 1,
                MaintenenceMode= false
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Setting> Settings { get; set; }
        
        
        
        
    }
}
