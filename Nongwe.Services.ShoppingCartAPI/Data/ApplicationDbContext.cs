using Microsoft.EntityFrameworkCore;
using Nongwe.Services.ShoppingCartAPI.Models;

namespace Nongwe.Services.ShoppingCartAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
      
        }

        public DbSet<CartHeader> cartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

    }
}
