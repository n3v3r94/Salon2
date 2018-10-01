using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Salon.Data.Models;
using Salon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salon.Web.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IAdministratorService administratorSvc;


        public AdministratorController(IAdministratorService administratorSvc )
        {
            this.administratorSvc = administratorSvc;
           
        }

        public IActionResult AllUsers()
        {
           return View( administratorSvc.ListAllUser());

        }

        public async Task<IActionResult> Roles(string id)
        {
            return View( await administratorSvc.UserRole(id));
        }

       public IActionResult SetUserRole()
        {
            ViewBag.UserRoles = administratorSvc.SetUserRole();

            return View(administratorSvc.SetUserRole());
        }

        [HttpPost]
        public async Task<IActionResult> SetUserRole(string id , string role)
        {
            if (id==null)
            {
                return NotFound();
            }

             await administratorSvc.SetUserRole(id, role);

            return RedirectToAction(nameof(AllUsers));
        }

    }
}
