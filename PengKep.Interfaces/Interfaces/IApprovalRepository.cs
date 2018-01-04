using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Interfaces
{
    public interface IApprovalRepository : IGenericRepository<Approval>
    {
        Approval GetOrganizationUnitApprovalStatus(string organizationUnitId, int year, int month);
        string GetOrganizationUnitCurrentRoleID(string organizationUnitID, int year, int month);
    }
}
