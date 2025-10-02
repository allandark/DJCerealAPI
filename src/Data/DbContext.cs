using CerealAPI.src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CerealAPI.src.Data
{

    /// <summary>
    /// Entity Frame Database Context
    /// </summary>
    public class AppDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



    }
}