﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class AdvertContext : DbContext
    {
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<User> Users { get; set; }

        public AdvertContext()
        {
        }

        public AdvertContext(DbContextOptions<AdvertContext> options)
            : base(options)
        {
        }
    }
}
