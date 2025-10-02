using CerealAPI.src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CerealAPI.src.Data
{


    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



    }
}