using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.BusinessEntities
{
    [Table("tbl_organization_unit")]
    public class OrganizationUnit
    {
        [Key]
        [Required]
        [Column("org_unit_id")]
        [Display(Name = "Organization Unit ID")]
        [MaxLength(12)]
        public string OrganizationUnitID { get; set; }

        [Column("org_unit_name")]
        [Display(Name = "Organization Unit")]
        [MaxLength(50)]
        public string OrganizationUnitName { get; set; }

        [Column("org_unit_parent_id")]
        [Display(Name = "Organization Unit Parent")]
        [MaxLength(12)]
        public string OrganizationUnitParentID { get; set; }

        [Column("org_unit_display_order")]
        [Display(Name = "Organization Unit Display Order")]
        public int? OrganizationUnitDisplayOrder { get; set; }

        [Column("company_tag")]
        [Display(Name = "Company Tag")]
        [MaxLength(500)]
        public string CompanyTag { get; set; }

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
