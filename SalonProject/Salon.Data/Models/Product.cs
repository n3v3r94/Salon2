using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Salon.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [Range(0, 100)]
        public double Discount { get; set; }

        public int Duration { get; set; } 

        public int SalonId { get; set; }

        public Salons Salon { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public List<WorkerProduct> Workers { get; set; } = new List<WorkerProduct>();
    }
}
