using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class ExpenseViewModel
    {
         
        [Display(Name = "Expense ID")]
        public string ExpenseID { get; set; }

        [Display(Name = "Expense")]
        public string ExpenseName { get; set; }

        public string ExpenseCategoryID { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Is Active")]
        public string IsActive { get; set; }

        public virtual ExpenseCategoryViewModel ExpenseCategory { get; set; }

    }
}