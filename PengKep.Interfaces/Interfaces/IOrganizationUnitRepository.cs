using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Interfaces
{
    public interface IOrganizationUnitRepository : IGenericRepository<OrganizationUnit>
    {
        List<OrganizationUnit> GetAccessibleOrganizationUnit(string userId);
        List<OrganizationUnit> GetOrganizationUnitChildren(string organizationUnitId, List<string> companyIDs);
        List<OrganizationUnit> GetAllOrganizationUnitChildren(string organizationUnitId, List<string> companyIDs, int? maxLevel = null);
        bool IsLeaf(string organizationUnitId);
        void ReorderByHierarchy(ref List<OrganizationUnit> organizationUnits, bool indent = true);
    }
}
