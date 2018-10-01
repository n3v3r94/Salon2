

namespace Salon.Services.Implementation
{
    using Microsoft.AspNetCore.Identity;
    using Salon.Data;
    using Salon.Data.Models;
    using Salon.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class AdministratorService : IAdministratorService
    {
        private readonly SalonDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministratorService(
            SalonDbContext db, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IEnumerable<UserViewModel> ListAllUser()
        {
            return (this.db.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }));
        }

        public async Task<UserRoleModel> UserRole( string id)
        {
            UserRoleModel userRole = new UserRoleModel();
            var user = await this.userManager.FindByIdAsync(id);

            userRole.UserName = user.UserName;
            var role = await userManager.GetRolesAsync(user);

            userRole.RoleUser = role;
            userRole.id = user.Id;
            return userRole;
        }

        public async Task SetUserRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);
            
           //his.db.Products.Include(s => s.SalonWorkers).ThenInclude(p => p.)
            await this.userManager.AddToRoleAsync(user, role);
        }

        public List<SelectListItem> SetUserRole()
        {
            var role = this.roleManager.Roles.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Name
            }).ToList();


            return role;
        }
    }
}
