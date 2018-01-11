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
    public class UserManagementController : Controller
    {

        private IUnitOfWork unitOfWork;

        private ICompanyRepository companyRepository;
        private IOrganizationUnitRepository organizationUnitRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public UserManagementController(
            ICompanyRepository companyRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitOfWork unitOfWork,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.companyRepository = companyRepository;
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
        public ActionResult Create([Bind(Exclude = "CreatedBy, CreatedDate, UserRoles.Role, LastAccess, LastUpdatedDate")]User model)
        {
            bool process = true;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = userManager.CreateAsync(user, model.Password);
                if (result.)
                {
                    signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await userManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
            return Json(new { result = result });
        }

        ////
        //// POST: /User/Edit/5

        //[HttpPost]
        //[ValidateCustomAntiForgeryToken()]
        //public ActionResult Edit([Bind(Exclude = "LastAccess")]User model)
        //{
        //    String result = "";
        //    bool process = true;
        //    if (ModelState.IsValid)
        //    {
        //        if (String.IsNullOrEmpty(model.LastUpdateRemark))
        //        {
        //            result = "Update reason cannot be blank"; process = false;
        //        }
        //        String _userid =
        //            (from q in userRepository.Get(includeProperties: "UserRoles")
        //             where q.UserID == User.Identity.Name && q.UserRoles.Any(a => a.RoleID == "ADM")
        //             select model.UserID).FirstOrDefault();

        //        if (_userid == null)
        //        {
        //            result = "You are not authorized to perform this action!"; process = false;
        //        }
        //        if (process)
        //        {
        //            List<string> arrRoles = new List<string>();
        //            if (model.UserRoles != null) { model.UserRoles.ToList().ForEach(x => arrRoles.Add(x.RoleID)); }
        //            List<UserRole> userRoles = userRoleRepository.Get().Where(w => w.UserID == _userid).ToList();

        //            var existingRoleIDs = roleRepository.Get().Select(s => s.RoleID).ToList();
        //            var notExistsRoleIDs = arrRoles.Where(w => !existingRoleIDs.Contains(w)).Select(s => s);
        //            foreach (var item in notExistsRoleIDs)
        //            {
        //                roleRepository.Insert(new Role() { RoleID = item });
        //            }
        //            unitOfWork.Commit();

        //            foreach (String role in arrRoles)
        //            {
        //                if (!userRoles.Any(a => a.RoleID == role) && !String.IsNullOrEmpty(role))
        //                {
        //                    userRoleRepository.Insert(new UserRole { UserID = _userid, RoleID = role });
        //                }
        //            }
        //            foreach (UserRole userRole in userRoles)
        //            {
        //                bool delete = true;
        //                foreach (String role in arrRoles)
        //                {
        //                    if (userRole.RoleID == role) { delete = false; break; }
        //                }
        //                if (delete == true)
        //                {
        //                    userRoleRepository.Delete(userRole);
        //                }
        //            }

        //            model.UserID = _userid.Replace("\\\\", "\\");
        //            var editedmodel = userRepository.GetByID(model.UserID);
        //            editedmodel.UserName = model.UserName;
        //            editedmodel.EmailAddress = model.EmailAddress;
        //            editedmodel.IsActive = model.IsActive;
        //            editedmodel.LastUpdatedBy = User.Identity.Name;
        //            editedmodel.LastUpdatedDate = DateTime.Now;
        //            editedmodel.LastUpdateRemark = model.LastUpdateRemark;
        //            userRepository.Update(editedmodel);
        //            unitOfWork.Commit();

        //            editedmodel.UserRoles =
        //                (from q in userRoleRepository.Get(includeProperties: "Role")
        //                 where q.UserID == model.UserID
        //                 orderby q.Role.RoleName
        //                 select q).ToList();

        //            var returnmodel = AutoMapper.Mapper.Map<UserViewModel>(editedmodel);
        //            foreach (var item in returnmodel.UserRoles.Where(w => w.Role?.RoleName == null))
        //            {
        //                var company = companyRepository.GetByID(item.RoleID);
        //                if (company != null)
        //                {
        //                    item.Role.RoleName = company.CompanyName;
        //                }
        //                else
        //                {
        //                    var organizationUnit = organizationUnitRepository.GetByID(item.RoleID);
        //                    if (organizationUnit != null) item.Role.RoleName = organizationUnit.OrganizationUnitName;
        //                }
        //            }
        //            return Json(new { result = "OK", model = returnmodel });
        //        }
        //    }
        //    else
        //    {
        //        foreach (var key in ModelState.Keys)
        //        {
        //            foreach (var err in ModelState[key].Errors)
        //            {
        //                result = err.ErrorMessage + "\n";
        //            }
        //        }

        //    }
        //    return Json(new { result = result });
        //}

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