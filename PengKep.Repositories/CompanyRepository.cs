using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.BusinessEntities;
using PengKep.Common.Constants;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {

        private UserManager<ApplicationUser> userManager;

        public CompanyRepository(
            DBContext context,
            UserManager<ApplicationUser> userManager)
            : base(context)
        {
            this.userManager = userManager;
        }

        public List<Company> GetAccessibleCompanies(string userId)
        {
            List<Company> accessibleCompanies = new List<Company>();
            var user = userManager.FindByEmail(userId);
            if (user != null && (user.LockoutEnabled == false || user.LockoutEndDateUtc < DateTime.UtcNow))
            {
                var roleIDs = user.Roles.Select(s => s.RoleId);
                accessibleCompanies = this.Get().Where(w => roleIDs.Contains(w.CompanyID) || roleIDs.Contains(ApplicationConstants.roleIDAdministrator)).ToList();
                if (accessibleCompanies.Count() > 1)
                {
                    accessibleCompanies.Insert(0, new Company { CompanyID = "", CompanyName = "- All -" });
                }
                return accessibleCompanies.ToList();
            }
            return accessibleCompanies.ToList();
        }
    }
}
