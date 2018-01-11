using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.BusinessEntities
{
    [Table("tbl_expense_category")]
    public class ExpenseCategory
    {
        [Key]
        [Column("expense_category_id")]
        [MaxLength(3)]
        public string ExpenseCategoryID { get; set; }

        [Column("expense_category_name")]
        [Display(Name = "Expense Category")]
        [MaxLength(30)]
        public string ExpenseCategoryName { get; set; }

        [Column("display_order")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
