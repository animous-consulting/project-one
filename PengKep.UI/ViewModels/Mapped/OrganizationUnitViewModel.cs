using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class OrganizationUnitViewModel
    {
        [Display(Name = "Organization Unit ID")]
        public string OrganizationUnitID { get; set; }

        [Display(Name = "Organization Unit")]
        public string OrganizationUnitName { get; set; }

        [Display(Name = "Organization Unit Parent ID")]
        public string OrganizationUnitParentID { get; set; }

        [Display(Name = "Organization Unit Display Order")]
        public int? OrganizationUnitDisplayOrder { get; set; }

        public string IsActive { get; set; }

    }
}