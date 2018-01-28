using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;
using PengKep.Helpers;
using PengKep.ViewModels;

namespace PengKep.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserManagementController : Controller
    {

        private IUnitOfWork unitOfWork;

        private IOrganizationUnitRepository organizationUnitRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public UserManagementController(
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitOfWork unitOfWork,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.organizationUnitRepository = organizationUnitRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            var users =
                (from q in userManager.Users
                 orderby q.UserName
                 select q).ToList();

            var model = AutoMapper.Mapper.Map<IEnumerable<ApplicationUserViewModel>>(users);

            var organizationUnits =
                (from q in organizationUnitRepository.Get()
                 select q).ToList();

            organizationUnitRepository.ReorderByHierarchy(ref organizationUnits, true);

            var roles =
                (from q in roleManager.Roles
                 select new RoleViewModel()
                 {
                     Id = q.Id,
                     Name = q.Name,
                     Description = q.Description,
                     RoleGroup = "basic"
                 }).ToList();

            roles = roles.Concat(
               (from q in organizationUnits
                select new RoleViewModel()
                {
                    Id = q.OrganizationUnitID,
                    Name = q.OrganizationUnitName,
                    Description = "Access to " + q.OrganizationUnitName.Trim() + " and organization units below",
                    RoleGroup = "orgunit"
                }).ToList()).ToList();

            var roleGroups = new List<dynamic> {
                new { RoleGroup = "basic", RoleGroupName =  "" },
                new { RoleGroup = "orgunit", RoleGroupName =  "Organization Unit" }
            };

            ViewBag.Roles = roles;
            ViewBag.RoleGroups = roleGroups;
            return View(model);
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateCustomAntiForgeryToken()]
        public ActionResult Create(RegisterViewModel model)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var createResult = userManager.CreateAsync(user, model.Password);
                if (createResult.Result.Succeeded)
                {
                    result = "OK";
                }
                else
                {
                    result = String.Join("\n", createResult.Result.Errors);
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var err in ModelState[key].Errors)
                    {
                        result = err.ErrorMessage + "\n";
                    }
                }

            }

            return Json(new { result = result });
        }

        ////
        //// POST: /User/Edit/5

        [HttpPost]
        [ValidateCustomAntiForgeryToken()]
        public ActionResult Edit(ApplicationUser model)
        {
            String result = "";
            bool process = true;
            if (ModelState.IsValid)
            {
                if (process)
                {

                    var roles =
                        (from q in roleManager.Roles
                         select q).ToList();

                    var user =
                        (from q in userManager.Users
                         where q.Email == model.Email
                         select q).FirstOrDefault();

                    var existingRoles =
                        (from q in userManager.Users
                         where q.Email == model.Email
                         select q.Roles).FirstOrDefault();

                    var deletedRoles =
                        (from q in existingRoles
                         join r in roles on q.RoleId equals r.Id
                         where !model.Roles.Any(a => a.RoleId == q.RoleId)
                         select r.Name).ToArray();

                    var newRoles =
                        (from q in model.Roles
                         join r in roles on q.RoleId equals r.Id
                         where !existingRoles.Any(a => a.RoleId == q.RoleId)
                         select r.Name).ToArray();

                    var updateResult = userManager.RemoveFromRolesAsync(user.Id, deletedRoles);

                    if (updateResult.Result.Succeeded)
                    {

                        updateResult = userManager.AddToRolesAsync(user.Id, newRoles);

                        if (updateResult.Result != null && updateResult.Result.Succeeded)
                        {
                            return Json(new { result = "OK", model = model });
                        }
                        else
                        {
                            result = String.Join("\n", updateResult.Result.Errors);
                        }
                    }
                    else
                    {
                        result = String.Join("\n", updateResult.Result.Errors);
                    }
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var err in ModelState[key].Errors)
                    {
                        result = err.ErrorMessage + "\n";
                    }
                }
            }
            return Json(new { result = result });
        }

        ////
        //// GET: /User/Delete/5

        //public ActionResult Delete(String id)
        //{
        //    var model = userRepository.GetByID(id);
        //    ViewBag.ActiveOption = new SelectList(new string[] { "Y", "N" });
        //    return PartialView("_Delete", model);
        //}

        ////
        //// POST: /User/Delete/5

        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult Delete([Bind(Include = "UserID")]User model)
        //{
        //    String ret = "";
        //    bool process = true;
        //    if (userRoleRepository.Get().Any(a => a.UserID == model.UserID))
        //    {
        //        ret = "Cannot delete user with role assigned!";
        //        process = false;
        //    }
        //    if (process)
        //    {
        //        userRepository.Delete(model.UserID);
        //        unitOfWork.Commit();
        //        return Json(new
        //        {
        //            result = "OK",
        //            id = model.UserID.Replace(" ", "").Replace("\\", "\\\\").Replace("!", "exclamation")
        //        });
        //    }
        //    return Json(new { result = ret });
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //    unitOfWork.Dispose();
        //}

        //private string EscapeLdapSearchFilter(string searchFilter)
        //{
        //    StringBuilder escape = new StringBuilder();
        //    for (int i = 0; i < searchFilter.Length; ++i)
        //    {
        //        char current = searchFilter[i];
        //        switch (current)
        //        {
        //            case '\\':
        //                escape.Append(@"\5c");
        //                break;
        //            case '*':
        //                escape.Append(@"\2a");
        //                break;
        //            case '(':
        //                escape.Append(@"\28");
        //                break;
        //            case ')':
        //                escape.Append(@"\29");
        //                break;
        //            case '\u0000':
        //                escape.Append(@"\00");
        //                break;
        //            case '/':
        //                escape.Append(@"\2f");
        //                break;
        //            default:
        //                escape.Append(current);
        //                break;
        //        }
        //    }

        //    return escape.ToString();
        //}
    }
}