using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_variance_threshold")]
    public class VarianceThreshold
    {
        [Key]
        [ForeignKey("Variance")]
        [Column("var_id", Order = 0)]
        [Display(Name = "Variance ID")]
        [MaxLength(6)]
        public string VarianceID { get; set; }

        [Key]
        [ForeignKey("ExpenseCategory")]
        [Column("expense_category_id", Order = 1)]
        [Display(Name = "Expense Category ID")]
        [MaxLength(3)]
        public string ExpenseCategoryID { get; set; }

        [Column("threshold")]
        public double? Threshold { get; set; }

        public virtual Variance Variance { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
}
