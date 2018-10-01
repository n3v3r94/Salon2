using System.Collections.Generic;

namespace Salon.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductDetails
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Range(0,double.MaxValue)]
        public double Price { get; set; }

        [Range(0, double.MaxValue)]
        public double Discount { get; set; }

        public List<string> Workers { get; set; } = new List<string>();
    }
}
