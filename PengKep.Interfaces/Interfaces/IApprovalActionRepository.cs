using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Interfaces
{
    public interface IApprovalActionRepository : IGenericRepository<ApprovalAction>
    {
        bool HasBranchedAction(string statusId);
        List<ApprovalAction> GetForwardActions();
    }
}
