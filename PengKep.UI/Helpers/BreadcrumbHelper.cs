using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PengKep.ViewModels;

namespace PengKep.Helpers
{
    public static class BreadcrumbHelper
    {
        public static List<OrganizationUnitViewModel> Create(string organizationUnitId, List<OrganizationUnitViewModel> organizationUnits)
        {
            // create breadcrumb
            List<PengKep.ViewModels.OrganizationUnitViewModel> organizationUnitBreadcrumb = new List<PengKep.ViewModels.OrganizationUnitViewModel>();
            var organizationUnitParentID =
                (from q in organizationUnits
                 where q.OrganizationUnitID == organizationUnitId 
                 select q.OrganizationUnitParentID).SingleOrDefault();

            while (!String.IsNullOrEmpty(organizationUnitParentID) && organizationUnitParentID != "ALL")
            {
                var organizationUnit =
                    (from q in organizationUnits
                     where q.OrganizationUnitID == organizationUnitParentID 
                     select q).SingleOrDefault();

                if (organizationUnit != null)
                {
                    organizationUnitBreadcrumb.Add(new OrganizationUnitViewModel { OrganizationUnitID = organizationUnit.OrganizationUnitID, OrganizationUnitName = organizationUnit.OrganizationUnitName });
                    organizationUnitParentID = organizationUnit.OrganizationUnitParentID;
                }
                else
                {
                    organizationUnitParentID = null;
                }
            }
            organizationUnitBreadcrumb.Reverse();
            return organizationUnitBreadcrumb;
        }
    }
}