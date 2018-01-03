using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using PengKep.Models;

namespace PengKep.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserManagementController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public UserManagementController(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: UserManagement
        public ActionResult Index()
        {
            var model = userManager.Users;
            return View(model);
        }
    }
}