

namespace Salon.Services.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductWithWorkers
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public int productId { get; set; }

        public List<string> Workers { get; set; } = new List<string>();

        public List<SelectListItem> selectWorker = new List<SelectListItem>();
    }
}
