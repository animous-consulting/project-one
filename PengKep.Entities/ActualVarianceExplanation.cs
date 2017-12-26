using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_actual_variance_explanation")]
    public class ActualVarianceExplanation
    {
        [Key]
        [ForeignKey("OrganizationUnit")]
        [Required]
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
        [ForeignKey("Variance")]
        [Required]
        [Column("var_id", Order = 2)]
        [Display(Name = "Variance ID")]
        [MaxLength(6)]
        public string VarianceID { get; set; }

        [Key]
        [Required]
        [Column("year", Order = 3)]
        [Display(Name = "year")]
        public int Year { get; set; }

        [Key]
        [Required]
        [Column("month", Order = 4)]
        [Display(Name = "month")]
        public int Month { get; set; }

        [Column("content", TypeName = "TEXT")]
        [Display(Name = "Content")]
        public string Content { get; set; }

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
        public virtual Company Company { get; set; }
        public virtual Variance Variance { get; set; }
    }
}