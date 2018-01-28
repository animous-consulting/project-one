using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class ApprovalRepository : GenericRepository<Approval>, IApprovalRepository
    {

        private IOrganizationUnitRepository organizationUnitRepository;
        private IApprovalStatusRepository approvalStatusRepository;

        public ApprovalRepository(DBContext context,
            IOrganizationUnitRepository organizationUnitRepository,
            IApprovalStatusRepository approvalStatusRepository)
            : base(context)
        {
            this.organizationUnitRepository = organizationUnitRepository;
            this.approvalStatusRepository = approvalStatusRepository;
        }

        public Approval GetOrganizationUnitApprovalStatus(string organizationUnitId, int year, int month)
        {
            var approval = this.Get(includeProperties: "ApprovalStatus, OrganizationUnit").Where(w => w.OrganizationUnitID == organizationUnitId && w.Year == year && w.Month == month).SingleOrDefault();
            if (approval == null)
            {
                var organizationUnitChildrenIDs = organizationUnitRepository.GetOrganizationUnitChildren(organizationUnitId).Select(s => s.OrganizationUnitID);
                if ( this.Get().Where(w => organizationUnitChildrenIDs.Contains(w.OrganizationUnitID) && w.Year == year && w.Month == month).Select(s => s.ApprovalStatusID).Distinct().Count() == 1)
                {
                    approval = this.Get(includeProperties: "ApprovalStatus").Where(w => organizationUnitChildrenIDs.Contains(w.OrganizationUnitID) && w.Year == year && w.Month == month).FirstOrDefault();
                }
            }
            return approval;
        }

        public string GetOrganizationUnitCurrentRoleID(string organizationUnitId, int year, int month)
        {
            var organizationUnitApprovalStatus = GetOrganizationUnitApprovalStatus(organizationUnitId, year, month);
            if (organizationUnitApprovalStatus != null)
            {
                var approvalStatus = approvalStatusRepository.Get().Where(w => w.ApprovalStatusID == organizationUnitId).SingleOrDefault();
                if (approvalStatus != null) return approvalStatus.RoleID;
            }
            return null;
        }
    }
}
