using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CerealAPI.src
{


    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}