using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Advert
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }
        public float Price { get; set; }
    }

    public class AdvertContext : DbContext
    {
        public DbSet<Advert> Adverts { get; set; }

        public AdvertContext()
        {
        }

        public AdvertContext(DbContextOptions<AdvertContext> options) 
            : base(options)
        {
        }           


    }
}
