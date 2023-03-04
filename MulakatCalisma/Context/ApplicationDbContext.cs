using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Entity;
using MulakatCalisma.Entity.Model;

namespace MulakatCalisma.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }    
        public DbSet<Order> Orders { get; set; }    
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Basket> Baskets { get; set; }

    }
}
