using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class ActualExpenseViewModel
    {
        [Display(Name = "Organization Unit ID")]
        public string OrganizationUnitID { get; set; }

        [Display(Name = "Year")]
        public string Year { get; set; }

        [Display(Name = "Version")]
        public int Version { get; set; }

        [Display(Name = "Expense")]
        public string ExpenseID { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Jan { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Feb { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Mar { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Apr { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? May { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Jun { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Jul { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Aug { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Sep { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Oct { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Nov { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Dec { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public double? Total
        {
            get
            {
                return (Jan ?? 0) + (Feb ?? 0) + (Mar ?? 0) + (Apr ?? 0) + (May ?? 0) + (Jun ?? 0) +
                (Jul ?? 0) + (Aug ?? 0) + (Sep ?? 0) + (Oct ?? 0) + (Nov ?? 0) + (Dec ?? 0);
            }
        }

        public virtual OrganizationUnitViewModel OrganizationUnit { get; set; }
        public virtual ExpenseViewModel Expense { get; set; }
    }
}