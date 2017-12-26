using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_expense")]
    public class Expense
    {
        [Key]
        [Required]
        [Column("expense_id")]
        [Display(Name = "Expense ID")]
        [MaxLength(10)]
        public string ExpenseID { get; set; }

        [Column("expense_name")]
        [Display(Name = "Expense")]
        [MaxLength(50)]
        public string ExpenseName { get; set; }

        [ForeignKey("ExpenseCategory")]
        [Column("expense_category_id")]
        [MaxLength(3)]
        public string ExpenseCategoryID { get; set; }

        [Column("display_order")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Column("is_active")]
        [Display(Name = "Is Active")]
        [MaxLength(1)]
        public string IsActive { get; set; }

        [Column("crt_by")]
        [MaxLength(30)]
        public string CreatedBy { get; set; }

        [Column("crt_date")]
        public DateTime? CreatedDate { get; set; }

        [Column("last_upd_by")]
        [MaxLength(30)]
        public string LastUpdatedBy { get; set; }

        [Column("last_upd_date")]
        public DateTime? LastUpdatedDate { get; set; }

        [Column("last_upd_remark")]
        [MaxLength(50)]
        public string LastUpdateRemark { get; set; }

        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
}
