

namespace Salon.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SearchByProductViewModel
    {

        public int Id { get; set; }

        [StringLength(100)]
        public string SalonName { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }
    }

    
}
