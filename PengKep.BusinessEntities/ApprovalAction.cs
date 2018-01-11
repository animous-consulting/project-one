using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.BusinessEntities
{
    [Table("tbl_approval_action")]
    public class ApprovalAction
    {

        [Key]
        [Column("action_id")]
        [MaxLength(7)]
        public string ActionID { get; set; }

        [Column("action_name")]
        [MaxLength(20)]
        public string ActionName { get; set; }

        [Column("action_desc")]
        [MaxLength(50)]
        public string ActionDesc { get; set; }

        [Column("status_id_init")]
        [MaxLength(25)]
        public string ApprovalStatusIDInit { get; set; }

        [Column("status_id_next")]
        [MaxLength(25)]
        public string ApprovalStatusIDNext { get; set; }

        [Column("status_id_history_next")]
        [MaxLength(25)]
        public string ApprovalStatusIDHistoryNext { get; set; }

        [Column("remark_required")]
        [MaxLength(1)]
        public string RemarkRequired { get; set; }

        [Column("display_order")]
        public int? DisplayOrder { get; set; }

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
