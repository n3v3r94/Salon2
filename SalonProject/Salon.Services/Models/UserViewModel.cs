

namespace Salon.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserViewModel
    {
        public  string Id { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }


        public string Email { get; set; }

    }
}
