using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class ApprovalStatusRepository : GenericRepository<ApprovalStatus>, IApprovalStatusRepository
    {
        public ApprovalStatusRepository(DBContext context)
            : base(context)
        {

        }
    }
}
