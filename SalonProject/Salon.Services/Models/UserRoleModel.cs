

namespace Salon.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class UserRoleModel
    {
        [StringLength(100)]
        public string UserName { get; set; }

        public string id { get; set; }

        public IList<string> RoleUser {get;set;}
    }
}
