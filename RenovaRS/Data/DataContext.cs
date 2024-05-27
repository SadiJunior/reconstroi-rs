using Microsoft.EntityFrameworkCore;
using RenovaRS.Models;

namespace RenovaRS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Service> Services { get; set; }
    }
}
