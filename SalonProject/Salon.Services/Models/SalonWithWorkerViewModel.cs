
namespace Salon.Services.Models
{
    using Salon.Data.Models;
    using System.Collections.Generic;

    public class SalonWithWorkerViewModel
    {
        public Salons Salons { get; set; }
        
        public List<User> WorkerUsers { get; set; }
    }
}
