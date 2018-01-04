using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Interfaces
{
    public interface IApprovalHistoryRepository : IGenericRepository<ApprovalHistory>
    {
        int GetNewSequence(string organizationUnitId, DateTime period);
    }
}
