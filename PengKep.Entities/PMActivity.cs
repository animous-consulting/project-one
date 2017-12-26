using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_pm_activity")]
    public class PMActivity
    {

        [Key]
        [Required]
        [Column("activity_id")]
        [Display(Name = "Activity ID")]
        [MaxLength(15)]
        public string ActivityID { get; set; }

        [Column("period")]
        [Display(Name = "Period")]
        public DateTime Period { get; set; }

        //[Column("posting_date")]
        //[Display(Name = "Posting Date")]
        //public DateTime PostingDate { get; set; }

        [Column("cost_center_id")]
        [Display(Name = "Cost Center ID")]
        [MaxLength(12)]
        public string CostCenterID { get; set; }

        [ForeignKey("Account")]
        [Column("account_code")]
        [Display(Name = "Account Code")]
        [MaxLength(10)]
        public string AccountCode { get; set; }

        [ForeignKey("Expense")]
        [Column("expense_id")]
        [Display(Name = "Expense ID")]
        [MaxLength(15)]
        public string ExpenseID { get; set; }

        [Column("description")]
        [Display(Name = "Description")]
        [MaxLength(200)]
        public string Description { get; set; }

        [Column("description_2")]
        [Display(Name = "Description #2")]
        [MaxLength(200)]
        public string Description2 { get; set; }

        [Column("currency")]
        [MaxLength(5)]
        public string Currency { get; set; }

        [Column("amount")]
        [Display(Name = "Amount")]
        public double? Amount { get; set; }

        [Column("amount_in_usd")]
        [Display(Name = "Amount in USD")]
        public double? AmountInUSD { get; set; }

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
        public virtual Account Account { get; set; }
    }
}
