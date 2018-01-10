using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class ApprovalViewModel
    {
        [Display(Name = "Organization Unit ID")]
        public string OrganizationUnitID { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Approval Status")]
        public string ApprovalStatusID { get; set; }

        [Display(Name = "Approval Remark")]
        public string ApprovalRemark { get; set; }

        public virtual OrganizationUnitViewModel OrganizationUnit { get; set; }
        public virtual ApprovalStatusViewModel ApprovalStatus { get; set; }
    }
}