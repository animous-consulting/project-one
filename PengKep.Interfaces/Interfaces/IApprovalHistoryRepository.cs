using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Common.Interfaces
{
    public interface IApprovalHistoryRepository : IGenericRepository<ApprovalHistory>
    {
        int GetNewSequence(string organizationUnitId, DateTime period);
    }
}
