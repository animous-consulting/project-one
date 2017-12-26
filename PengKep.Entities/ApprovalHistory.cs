using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_approval_history")]
    public class ApprovalHistory
    {
 
        [Key]
        [Column("organization_unit_id", Order = 0)]
        [MaxLength(12)]
        [Display(Name = "Organization Unit ID")]
        public string OrganizationUnitID { get; set; }

        [Key]
        [Column("year", Order = 1)]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Key]
        [Column("month", Order = 2)]
        [Display(Name = "Month")]
        public int Month { get; set; }

        [Key]
        [Display(Name = "Sequence")]
        [Column("seq", Order = 3)]
        public int Seq { get; set; }

        [ForeignKey("ApprovalStatus")]
        [Column("approval_status")]
        [MaxLength(25)]
        [Display(Name = "Approval Status ID")]
        public string ApprovalStatusID { get; set; }

        [Column("approval_status_working")]
        [MaxLength(25)]
        [Display(Name = "Approval Status Working ID")]
        public string ApprovalStatusWorkingID { get; set; }

        [Column("act_by")]
        [MaxLength(30)]
        public string ActBy { get; set; }

        [Column("act_date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
        public DateTime ActDate { get; set; }

        [Column("act_remark")]
        [MaxLength(200)]
        public string ActRemark { get; set; }

        public virtual ApprovalStatus ApprovalStatus { get; set; }

    }
}
