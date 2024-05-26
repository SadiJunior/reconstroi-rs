﻿using Microsoft.EntityFrameworkCore;
using RenovaRS.Models;

namespace RenovaRS.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
    }
}
