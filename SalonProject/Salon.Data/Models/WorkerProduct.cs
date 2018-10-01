using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Data.Models
{
    public class WorkerProduct
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int WorkerId { get; set; }

        public Worker Worker { get; set; }
    }
}
