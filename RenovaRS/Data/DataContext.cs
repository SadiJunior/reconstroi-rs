using Microsoft.EntityFrameworkCore;
using ReconstroiRS.Models;

namespace ReconstroiRS.Data
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
