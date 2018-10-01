

namespace Salon.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    public class AddProductView
    {
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, double.MaxValue)]
        public double Discount { get; set; }

        public int SalonId { get; set; }
    }
}
