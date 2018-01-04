using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.Entities;
using PengKep.Interfaces;

namespace PengKep.Repositories
{
    public class ApprovalHistoryRepository : GenericRepository<ApprovalHistory>, IApprovalHistoryRepository
    {
        public ApprovalHistoryRepository(DBContext context)
            : base(context)
        {

        }

        public int GetNewSequence(string organizationUnitId, DateTime period)
        {
            int max = this.Get().Where(w => w.OrganizationUnitID == organizationUnitId && w.Year == period.Year && w.Month == period.Month).ToList().DefaultIfEmpty(new ApprovalHistory() { Seq = 0 }).Max(w => w.Seq);
            return max + 1;
        }
    }
}
