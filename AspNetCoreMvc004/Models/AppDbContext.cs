using Microsoft.EntityFrameworkCore;
using AspNetCoreMvc004.Models;

namespace AspNetCoreMvc004.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<AspNetCoreMvc004.Models.Category> Category { get; set; }
    }
}
