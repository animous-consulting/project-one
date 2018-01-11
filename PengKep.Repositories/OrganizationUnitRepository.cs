using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;
using PengKep.Common.Constants;

namespace PengKep.Repositories
{
    public class OrganizationUnitRepository : GenericRepository<OrganizationUnit>, IOrganizationUnitRepository
    {

        private ICompanyRepository companyRepository;
        private UserManager<ApplicationUser> userManager;

        public OrganizationUnitRepository(DBContext context,
            ICompanyRepository companyRepository,
            UserManager<ApplicationUser> userManager)
            : base(context)
        {
            this.companyRepository = companyRepository;
            this.userManager = userManager;
        }

        public List<OrganizationUnit> GetAccessibleOrganizationUnit(string userId)
        {
            List<OrganizationUnit> accessibleOrganizationUnits = new List<OrganizationUnit>();
            var user = userManager.FindByEmail(userId);
            if (user != null && (user.LockoutEnabled == false || user.LockoutEndDateUtc < DateTime.UtcNow))
            {

                var userRoleIDs = user.Roles.Select(s => s.RoleId).ToList();
                var allOrganizationUnits = this.Get().Where(w => w.IsActive == "Y").ToList();

                var accessibleOrganizationUnitIDs = this.Get().Where(w => userRoleIDs.Contains(w.OrganizationUnitID) || userRoleIDs.Contains(ApplicationConstants.roleIDAdministrator) && w.IsActive == "Y").Select(s => s.OrganizationUnitID).ToList();
                var accessibleCompanyIDs = companyRepository.GetAccessibleCompanies(userId).Select(s => s.CompanyID).ToList();

                char[] spliter = new char[] { ';' };

                // switch to below code for faster performance
                while (
                    (from q in allOrganizationUnits
                     where accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                     !accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitID) &&
                     accessibleCompanyIDs.Intersect(q.CompanyTag.Split(spliter)).Any()
                     select q.OrganizationUnitID).Any())
                {
                    accessibleOrganizationUnitIDs = accessibleOrganizationUnitIDs.Union(
                            (from q in allOrganizationUnits
                             where accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                             !accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitID) &&
                             accessibleCompanyIDs.Intersect(q.CompanyTag.Split(spliter)).Any()
                             select q.OrganizationUnitID).ToList()
                        ).ToList();
                }

                accessibleOrganizationUnits =
                    (from q in allOrganizationUnits
                     where accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitID)
                     select q).ToList();
            }

            return accessibleOrganizationUnits.ToList();
        }

        public List<OrganizationUnit> GetOrganizationUnitChildren(string organizationUnitId, List<string> companyIDs)
        {
            var children =
                (from q in this.Get()
                 where q.OrganizationUnitParentID == organizationUnitId && q.IsActive == "Y"
                 select q).ToList();

            var spliter = new char[] { ';' };

            children =
                (from q in children
                 where companyIDs != null && companyIDs.Intersect(q.CompanyTag.Split(spliter)).Any()
                 select q).ToList();

            return children;
        }

        public List<OrganizationUnit> GetAllOrganizationUnitChildren(string organizationUnitId, List<string> companyIDs, int? maxLevel = null)
        {
            var organizationUnits = this.Get().ToList();

            var allOrganizationUnitIDs =
                (from q in this.Get()
                 where q.IsActive == "Y"
                 select q).ToList();

            if (companyIDs == null) companyIDs = new List<string>();

            //List<OrganizationUnit> children = new List<OrganizationUnit>();
            //PopulateChildren(organizationUnits, organizationUnitId, ref children, companyId);

            var spliter = new char[] { ';' };

            // switch to below code for faster performance
            List<string> organizationUnitIDs = new List<string>() { organizationUnitId };
            int level = 1;
            while (
                (from q in allOrganizationUnitIDs
                 where organizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                 !organizationUnitIDs.Contains(q.OrganizationUnitID) &&
                 companyIDs.Intersect(q.CompanyTag.Split(spliter)).Any()
                 select q.OrganizationUnitID).Any())
            {
                organizationUnitIDs = organizationUnitIDs.Union(
                        (from q in allOrganizationUnitIDs
                         where organizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                         !organizationUnitIDs.Contains(q.OrganizationUnitID) &&
                         companyIDs.Intersect(q.CompanyTag.Split(spliter)).Any()
                         select q.OrganizationUnitID).ToList()
                    ).ToList();
                if (maxLevel != null && level >= maxLevel) break;
                level += 1;
            }

            return
                (from q in allOrganizationUnitIDs
                 where organizationUnitIDs.Contains(q.OrganizationUnitID) && q.OrganizationUnitID != organizationUnitId
                 select q).ToList();
        }

        public bool IsLeaf(string organizationUnitId)
        {
            return !this.Get().Any(a => a.OrganizationUnitParentID == organizationUnitId);
        }

        public void ReorderByHierarchy(ref List<OrganizationUnit> organizationUnits, bool indent = true)
        {
            var displayOrder = 0;
            var displayLevel = 1;
            var orderedOrganizationUnits = organizationUnits;
            var toporganizationUnits = orderedOrganizationUnits.Where(w => orderedOrganizationUnits.Any(a => a.OrganizationUnitID == w.OrganizationUnitParentID) == false)
                .OrderByDescending(o => orderedOrganizationUnits.Any(a => a.OrganizationUnitParentID == o.OrganizationUnitID))
                .ThenByDescending(o => o.CompanyTag != null ? o.CompanyTag.Length : 100)
                .ThenBy(t => t.CompanyTag)
                .ThenBy(o => o.OrganizationUnitName)
                .ToList();
            foreach (var item in toporganizationUnits)
            {
                displayOrder += 1;
                item.OrganizationUnitDisplayOrder = displayOrder;
                PopulateChildrenDisplayOrder(orderedOrganizationUnits, item, displayLevel + 1, ref displayOrder, indent);
            }
            organizationUnits = orderedOrganizationUnits.OrderBy(o => o.OrganizationUnitDisplayOrder).ToList();
        }

        private void PopulateChildrenDisplayOrder(List<OrganizationUnit> organizationUnits, OrganizationUnit organizationUnit, int displayLevel, ref int displayOrder, bool indent = true)
        {
            var childrenOrganizationUnit = organizationUnits.Where(w => w.OrganizationUnitParentID == organizationUnit.OrganizationUnitID)
                .OrderByDescending(o => organizationUnits.Any(a => a.OrganizationUnitParentID == o.OrganizationUnitID))
                .ThenByDescending(o => o.CompanyTag != null ? o.CompanyTag.Length : 100)
                .ThenBy(t => t.CompanyTag)
                .ThenBy(o => o.OrganizationUnitName)
                .ToList();
            if (childrenOrganizationUnit != null)
            {
                foreach (var item in childrenOrganizationUnit)
                {
                    displayOrder += 1;
                    if (indent) item.OrganizationUnitName = String.Join("", Enumerable.Repeat("\u00A0\u00A0", displayLevel - 1)) + item.OrganizationUnitName;
                    item.OrganizationUnitDisplayOrder = displayOrder;
                    PopulateChildrenDisplayOrder(organizationUnits, item, displayLevel + 1, ref displayOrder, indent);
                }
            }
        }

        private void PopulateChildren(List<OrganizationUnit> organizationUnits, string organizationUnitId, ref List<OrganizationUnit> children, string companyId)
        {
            if (organizationUnits.Any(a => a.OrganizationUnitParentID == organizationUnitId &&
            (String.IsNullOrEmpty(companyId) || (!String.IsNullOrEmpty(companyId) && a.CompanyTag.Contains(companyId)))
            && a.IsActive == "Y"))
            {
                foreach (var item in organizationUnits.Where(w => w.OrganizationUnitParentID == organizationUnitId &&
                (String.IsNullOrEmpty(companyId) || (!String.IsNullOrEmpty(companyId) && w.CompanyTag.Contains(companyId)))
                && w.IsActive == "Y").ToList())
                {
                    if (!children.Any(a => a.OrganizationUnitID == item.OrganizationUnitID))
                    {
                        children.Add(item);
                    }
                    PopulateChildren(organizationUnits, item.OrganizationUnitID, ref children, companyId);
                }
            }
        }

        private void PopulateLeafChildren(List<OrganizationUnit> organizationUnits, string organizationUnitId, ref List<OrganizationUnit> children, string companyId)
        {
            if (organizationUnits.Any(a => a.OrganizationUnitParentID == organizationUnitId &&
            (String.IsNullOrEmpty(companyId) || (!String.IsNullOrEmpty(companyId) && a.CompanyTag.Contains(companyId)))
            && a.IsActive == "Y"))
            {
                foreach (var item in organizationUnits.Where(w => w.OrganizationUnitParentID == organizationUnitId && w.IsActive == "Y").ToList())
                {
                    if (this.IsLeaf(item.OrganizationUnitID) && !children.Any(a => a.OrganizationUnitID == item.OrganizationUnitID))
                    {
                        children.Add(item);
                    }
                    PopulateLeafChildren(organizationUnits, item.OrganizationUnitID, ref children, companyId);
                }
            }
        }
    }
}
