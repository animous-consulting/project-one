using System;
using System.Collections.Generic;

using PengKep.BusinessEntities;

namespace PengKep.Common.Interfaces
{
    public interface IApprovalActionRepository : IGenericRepository<ApprovalAction>
    {
        bool HasBranchedAction(string statusId);
        List<ApprovalAction> GetForwardActions();
    }
}
