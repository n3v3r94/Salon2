using Salon.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Services.Models
{
    public class SalonModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
