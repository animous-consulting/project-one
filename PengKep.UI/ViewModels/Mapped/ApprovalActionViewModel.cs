using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class ApprovalActionViewModel
    {

        public string ActionID { get; set; }

        public string ActionName { get; set; }

        public string ActionDesc { get; set; }

        public string ApprovalStatusIDCurr { get; set; }

        public string ApprovalStatusIDNext { get; set; }

        public string ApprovalStatusIDHistoryNext { get; set; }

        public string RemarkRequired { get; set; }

        public int? DisplayOrder { get; set; }

    }
}