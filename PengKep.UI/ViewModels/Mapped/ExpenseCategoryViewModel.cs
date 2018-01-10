using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class ExpenseCategoryViewModel
    {
        public string ExpenseCategoryID { get; set; }

        [Display(Name = "Expense Category")]
        public string ExpenseCategoryName { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}