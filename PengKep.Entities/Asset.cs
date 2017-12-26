using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_asset")]
    public class Asset
    {
        [Key]
        [Required]
        [Column("asset_id")]
        [Display(Name = "Asset ID")]
        [MaxLength(5)]
        public string AssetID { get; set; }

        [Column("asset_name")]
        [Display(Name = "Asset")]
        [MaxLength(50)]
        public string AssetName { get; set; }

        [Column("asset_parent_id")]
        [Display(Name = "Asset Parent ID")]
        [MaxLength(5)]
        public string AssetParentID { get; set; }

        [Column("asset_display_order")]
        [Display(Name = "Asset Display Order")]
        public int? AssetDisplayOrder { get; set; }

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

    }
}
