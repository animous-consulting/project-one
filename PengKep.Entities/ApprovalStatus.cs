using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_approval_status")]
    public class ApprovalStatus
    {

        [Key]
        [Column("approval_status")]
        [MaxLength(25)]
        [Display(Name = "Approval Status")]
        public string ApprovalStatusID { get; set; }

        [MaxLength(30)]
        [Column("approval_status_name")]
        [Display(Name = "Approval Status Name")]
        public string ApprovalStatusName { get; set; }

        [Column("role_id")]
        [Display(Name = "Role ID")]
        [MaxLength(6)]
        public string RoleID { get; set; }

        [Column("approval_node_level")]
        [Display(Name = "Approval Node Level")]
        public int? ApprovalNodeLevel { get; set; }

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

    }
}
