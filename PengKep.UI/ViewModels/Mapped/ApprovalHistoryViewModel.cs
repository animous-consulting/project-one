using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class ApprovalHistoryViewModel
    {

        [Display(Name = "Approval Year Month")]
        public string ApprovalYearMonth { get; set; }

        [Display(Name = "Organization Unit ID")]
        public string OrganizationUnitID { get; set; }

        [Display(Name = "Sequence")]
        public int Seq { get; set; }

        [Display(Name = "Approval Status ID")]
        public string ApprovalStatusID { get; set; }

        public string ActBy { get; set; }

        public DateTime ActDate { get; set; }

        public string ActRemark { get; set; }

        public virtual ApprovalStatusViewModel ApprovalStatus { get; set; }

    }
}