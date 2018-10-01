

namespace Salon.Services
{
    using Salon.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public interface IAdministratorService
    {
        IEnumerable<UserViewModel> ListAllUser();

        Task <UserRoleModel>  UserRole(string id);

        Task SetUserRole(string id, string role);

        List<SelectListItem> SetUserRole();
    }
}
