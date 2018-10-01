using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Salon.Data.Models
{
    
    public class User : IdentityUser
    {
        public virtual List<Salons> Salon { get; set; }

     
    }
}
