
namespace Salon.Services.Models
{
    using Salon.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchByUser
    {
        public int productId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Range(0,double.MaxValue)]
        public double Price { get; set; }


        [Range(0, double.MaxValue)]
        public double Discount { get; set; }

        public IList<string> Roles { get; set; } 

        public List<string> Workers { get; set; } = new List<string>();

        public User User { get; set; }
    }
}
