using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Salon.Data.Models
{
    public class Salons
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

    

    }
}
