using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_expense_element")]
    public class ExpenseElement
    {
        [Key]
        [Required]
        [Column("expense_element_id")]
        [Display(Name = "Expense Element ID")]
        [MaxLength(15)]
        public string ExpenseElementID { get; set; }

        [Column("expense_element_name")]
        [Display(Name = "Expense Element")]
        [MaxLength(50)]
        public string ExpenseElementName { get; set; }

        [ForeignKey("Expense")]
        [Column("expense_id")]
        [MaxLength(3)]
        public string ExpenseID { get; set; }

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

        public virtual Expense Expense { get; set; }
    }
}
