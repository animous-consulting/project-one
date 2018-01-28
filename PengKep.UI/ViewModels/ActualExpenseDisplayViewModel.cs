using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PengKep.ViewModels
{
    public class ActualExpenseDisplayViewModel
    {
        public List<ActualExpenseViewModel> ActualExpense { get; set; }
        public List<ActualExpenseViewModel> ActualExpenseByOrganizationUnit { get; set; }

    }
}