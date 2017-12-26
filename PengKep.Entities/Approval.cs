using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_approval")]
    public class Approval
    {

        [Key]
        [Required]
        [Column("organization_unit_id", Order = 0)]
        [Display(Name = "Organization Unit ID")]
        [MaxLength(12)]
        public string OrganizationUnitID { get; set; }

        [Key]
        [Column("year", Order = 1)]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Key]
        [Column("month", Order = 2)]
        [Display(Name = "Month")]
        public int Month { get; set; }

        [ForeignKey("ApprovalStatus")]
        [Column("approval_status")]
        [Display(Name = "Approval Status")]
        [MaxLength(25)]
        public string ApprovalStatusID { get; set; }

        [Column("approval_remark", TypeName = "TEXT")]
        [Display(Name = "Approval Remark")]
        public string ApprovalRemark { get; set; }

        public virtual OrganizationUnit OrganizationUnit { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }

    }
}
