

namespace Salon.Services.Models
{
    using Salon.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SalonViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
