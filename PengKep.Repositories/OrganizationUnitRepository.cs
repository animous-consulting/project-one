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

        private UserManager<ApplicationUser> userManager;

        public OrganizationUnitRepository(DBContext context,
            UserManager<ApplicationUser> userManager)
            : base(context)
        {
            this.userManager = userManager;
        }

        public List<OrganizationUnit> GetAccessibleOrganizationUnit(string userId)
        {
            List<OrganizationUnit> accessibleOrganizationUnits = new List<OrganizationUnit>();
            var user = userManager.FindByEmail(userId);
            if (user != null)
            {

                var userRoleIDs = user.Roles.Select(s => s.RoleId).ToList();
                var allOrganizationUnits = this.Get().Where(w => w.IsActive == "Y").ToList();

                //var accessibleOrganizationUnitIDs = this.Get().Where(w => userRoleIDs.Contains(w.OrganizationUnitID) || userRoleIDs.Contains(ApplicationConstants.roleIDAdministrator) && w.IsActive == "Y").Select(s => s.OrganizationUnitID).ToList();
                var accessibleOrganizationUnitIDs = allOrganizationUnits.Select(s => s.OrganizationUnitID).ToList();

                char[] spliter = new char[] { ';' };

                // switch to below code for faster performance
                while (
                    (from q in allOrganizationUnits
                     where accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                     !accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitID)
                     select q.OrganizationUnitID).Any())
                {
                    accessibleOrganizationUnitIDs = accessibleOrganizationUnitIDs.Union(
                            (from q in allOrganizationUnits
                             where accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                             !accessibleOrganizationUnitIDs.Contains(q.OrganizationUnitID)
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

        public List<OrganizationUnit> GetOrganizationUnitChildren(string organizationUnitId)
        {
            var children =
                (from q in this.Get()
                 where q.OrganizationUnitParentID == organizationUnitId && q.IsActive == "Y"
                 select q).ToList();

            var spliter = new char[] { ';' };

            children =
                (from q in children
                 select q).ToList();

            return children;
        }

        public List<OrganizationUnit> GetAllOrganizationUnitChildren(string organizationUnitId, int? maxLevel = null)
        {
            var organizationUnits = this.Get().ToList();

            var allOrganizationUnitIDs =
                (from q in this.Get()
                 where q.IsActive == "Y"
                 select q).ToList();

            //List<OrganizationUnit> children = new List<OrganizationUnit>();
            //PopulateChildren(organizationUnits, organizationUnitId, ref children, companyId);

            var spliter = new char[] { ';' };

            // switch to below code for faster performance
            List<string> organizationUnitIDs = new List<string>() { organizationUnitId };
            int level = 1;
            while (
                (from q in allOrganizationUnitIDs
                 where organizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                 !organizationUnitIDs.Contains(q.OrganizationUnitID)
                 select q.OrganizationUnitID).Any())
            {
                organizationUnitIDs = organizationUnitIDs.Union(
                        (from q in allOrganizationUnitIDs
                         where organizationUnitIDs.Contains(q.OrganizationUnitParentID) &&
                         !organizationUnitIDs.Contains(q.OrganizationUnitID)
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
            var arrOrderedOrganizationUnits = organizationUnits.ToArray();
            var orderedOrganizationUnits = arrOrderedOrganizationUnits.ToList();
            var toporganizationUnits = orderedOrganizationUnits.Where(w => orderedOrganizationUnits.Any(a => a.OrganizationUnitID == w.OrganizationUnitParentID) == false)
                .OrderByDescending(o => orderedOrganizationUnits.Any(a => a.OrganizationUnitParentID == o.OrganizationUnitID))
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

        public dynamic GetOrganizationUnitTreeData(string userId)
        {
            // accessed globally
            var accessibleOrganizationUnits = this.GetAccessibleOrganizationUnit(userId);
            this.ReorderByHierarchy(ref accessibleOrganizationUnits, false);

            List<OrganizationUnit> topLevelOrganizationUnits =
                (from q in accessibleOrganizationUnits
                 where !accessibleOrganizationUnits.Any(a => a.OrganizationUnitID == q.OrganizationUnitParentID)
                 orderby q.OrganizationUnitName
                 select q).ToList();

            List<dynamic> organizationUnitTreeData = new List<dynamic>();
            if (topLevelOrganizationUnits != null && topLevelOrganizationUnits.Count() > 0)
            {
                foreach (var item in topLevelOrganizationUnits)
                {
                    organizationUnitTreeData.Add(new { data = item.OrganizationUnitID, label = item.OrganizationUnitName, children = GetOrganizationUnitTreeDataChildren(accessibleOrganizationUnits, item.OrganizationUnitID) });
                }
            }
            return organizationUnitTreeData;
        }

        private List<dynamic> GetOrganizationUnitTreeDataChildren(List<OrganizationUnit> organizationUnits, string organizationUnitId)
        {
            List<dynamic> returnValue = new List<dynamic>();
            var children = organizationUnits.Where(w => w.OrganizationUnitParentID == organizationUnitId);
            foreach (var item in children)
            {
                if (!organizationUnits.Any(a => a.OrganizationUnitParentID == item.OrganizationUnitID))
                {
                    returnValue.Add(new { data = item.OrganizationUnitID, label = item.OrganizationUnitName.Trim() });
                }
                else
                {
                    returnValue.Add(new { data = item.OrganizationUnitID, label = item.OrganizationUnitName.Trim(), children = GetOrganizationUnitTreeDataChildren(organizationUnits, item.OrganizationUnitID) });
                }
            }
            return returnValue;
        }
    }
}
