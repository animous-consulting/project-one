using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_pm_expense")]
    public class PMExpense
    {
        [Key]
        [ForeignKey("OrganizationUnit")]
        [Column("organization_unit_id", Order = 0)]
        [Display(Name = "Organization Unit ID")]
        [MaxLength(12)]
        public string OrganizationUnitID { get; set; }

        [Key]
        [ForeignKey("Company")]
        [Column("company_id", Order = 1)]
        [Display(Name = "Company ID")]
        [MaxLength(5)]
        public string CompanyID { get; set; }

        [Key]
        [Required]
        [Column("year", Order = 2)]
        [Display(Name = "Year")]
        public int Year { get; set; }

        //[Key]
        //[Required]
        //[Column("version", Order = 2)]
        //[Display(Name = "Version")]
        //public int Version { get; set; }

        [Key]
        [ForeignKey("Expense")]
        [Required]
        [Column("expense_id", Order = 3)]
        [Display(Name = "Expense")]
        [MaxLength(10)]
        public string ExpenseID { get; set; }

        [Column("jan")]
        public double? Jan { get; set; }

        [Column("feb")]
        public double? Feb { get; set; }

        [Column("mar")]
        public double? Mar { get; set; }

        [Column("apr")]
        public double? Apr { get; set; }

        [Column("may")]
        public double? May { get; set; }

        [Column("jun")]
        public double? Jun { get; set; }

        [Column("jul")]
        public double? Jul { get; set; }

        [Column("aug")]
        public double? Aug { get; set; }

        [Column("sep")]
        public double? Sep { get; set; }

        [Column("oct")]
        public double? Oct { get; set; }

        [Column("nov")]
        public double? Nov { get; set; }

        [Column("dec")]
        public double? Dec { get; set; }

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
        [MaxLength(100)]
        public string LastUpdateRemark { get; set; }

        public virtual OrganizationUnit OrganizationUnit { get; set; }
        public virtual Expense Expense { get; set; }
        public virtual Company Company { get; set; }

        public virtual double? Total
        {
            get
            {
                return (Jan ?? 0) + (Feb ?? 0) + (Mar ?? 0) + (Apr ?? 0) + (May ?? 0) + (Jun ?? 0) +
                (Jul ?? 0) + (Aug ?? 0) + (Sep ?? 0) + (Oct ?? 0) + (Nov ?? 0) + (Dec ?? 0);
            }
        }

    }
}
