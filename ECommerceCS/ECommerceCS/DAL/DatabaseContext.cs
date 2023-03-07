using ECommerceCS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCS.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
            
        }

        public DbSet<Country> Countries { get; set; }
    }
}
