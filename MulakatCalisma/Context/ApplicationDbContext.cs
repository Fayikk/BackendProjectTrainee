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
        public DbSet<Star> Stars { get; set; }  
        public DbSet<UserMoney> UserMoneys { get; set; }
        public DbSet<Teacher> Teachers { get; set; }  
    }
}
