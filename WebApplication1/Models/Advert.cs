using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    class Advert
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }
        public float Price { get; set; }
    }
}
