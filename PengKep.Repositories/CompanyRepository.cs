using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.Entities;
using PengKep.Interfaces;

namespace PengKep.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {


        public CompanyRepository(DBContext context)
            : base(context)
        {

        }

        public List<Company> GetAccessibleCompanies(string userId)
        {
            //if (!userRepository.Get().Any(a => a.UserID.ToLower() == userId.ToLower() && a.IsActive == "Y")) return null;
            //var userRoleIDs = userRoleRepository.Get().Where(w => w.UserID == userId).Select(s => s.RoleID).ToList();
            //var accessibleCompanies = this.Get().Where(w => userRoleIDs.Contains(w.CompanyID) || userRoleIDs.Contains("ADM")).ToList();
            //if (accessibleCompanies.Count() > 1)
            //{
            //    accessibleCompanies.Insert(0, new Company { CompanyID = "", CompanyName = "- All -" });
            //}
            //return accessibleCompanies.ToList();
            return new List<Company>();
        }
    }
}
