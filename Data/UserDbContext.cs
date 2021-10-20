using Microsoft.EntityFrameworkCore;
using UserService.Models;
using System.Collections.Concurrent;
using System;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace UserService.Data {
    public class UserDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public UserDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public UserDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}