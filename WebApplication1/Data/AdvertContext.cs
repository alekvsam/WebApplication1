using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Entities;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Data
{
    public class AdvertContext : DbContext
    {
        public DbSet<AdvertModel> Adverts { get; set; }
        public DbSet<User> Users { get; set; }

        public AdvertContext()
        {
        }

        public AdvertContext(DbContextOptions<AdvertContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<Program>()
                    .Build();
                var connectionString = config["WebApplication1:ConnectionString"];
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
