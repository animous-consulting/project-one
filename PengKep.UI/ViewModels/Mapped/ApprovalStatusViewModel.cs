using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class ApprovalStatusViewModel
    {

        [Display(Name = "Approval Status")]
        public string ApprovalStatusID { get; set; }

        [Display(Name = "Approval Status Name")]
        public string ApprovalStatusName { get; set; }

        [Display(Name = "Role ID")]
        public string RoleID { get; set; }

        [Display(Name = "Approval Node Level")]
        public int? ApprovalNodeLevel { get; set; }

    }
}