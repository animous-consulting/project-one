using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.Entities;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class ApprovalActionRepository : GenericRepository<ApprovalAction>, IApprovalActionRepository
    {
        public ApprovalActionRepository(DBContext context)
            : base(context)
        {

        }

        public bool HasBranchedAction(string statusId)
        {
            return this.Get().ToList().Where(w => w.ApprovalStatusIDInit == statusId && GetForwardActions().Any(a => a.ActionID == w.ActionID)).Count() > 1;
        }

        public List<ApprovalAction> GetForwardActions()
        {
            return this.Get().Where(w => w.ActionID.Substring(0, 1) != "R").ToList();
            //bool isBranchForward = false;
            //List<ApprovalAction> approvalActions = new List<ApprovalAction>();
            //var step = 1;
            //List<dynamic> approvalStatusIDs = new List<dynamic>() { new { ApprovalStatusID = initialStatusID, Step = step } };
            //PopulateForwardAction(initialStatusID, step, ref approvalActions, ref approvalStatusIDs, ref isBranchForward);
            //return approvalActions;
        }

        //public void PopulateForwardAction(string statusId, int step, ref List<ApprovalAction> approvalActions, ref List<dynamic> approvalStatusIDs, ref bool isBranchForward)
        //{
        //    isBranchForward = false;
        //    var apprStatusIDs = approvalStatusIDs;
        //    var nextActions = this.Get().Where(w => w.ApprovalStatusIDCurr == statusId);
        //    if (nextActions.Count() == 0) isBranchForward = true;
        //    var approvalStep = step + 1;
        //    foreach (var item in nextActions.Where(w => !apprStatusIDs.Any(a => a.ApprovalStatusID == w.ApprovalStatusIDNext) || apprStatusIDs.Any(a => a.ApprovalStatusID == w.ApprovalStatusIDNext && a.Step > approvalStep)))
        //    {
        //        var branchApprovalActions = approvalActions;
        //        var branchApprovalStatusID = approvalStatusIDs;
        //        branchApprovalStatusID.Add(new { ApprovalStatusID = item.ApprovalStatusIDNext, Step = approvalStep });
        //        branchApprovalActions.Add(item);
        //        PopulateForwardAction(item.ApprovalStatusIDNext, approvalStep, ref branchApprovalActions, ref branchApprovalStatusID, ref isBranchForward);
        //        if (isBranchForward == true)
        //        {
        //            approvalStatusIDs = approvalStatusIDs.Concat(branchApprovalStatusID).ToList();
        //            approvalActions = approvalActions.Concat(branchApprovalActions).ToList();
        //        }
        //    }
        //}
    }
}
