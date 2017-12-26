using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_organization_unit_map")]
    public class OrganizationUnitMap
    {
        [Key]
        [Column("org_unit_id", Order = 0)]
        [Display(Name = "Organization Unit ID")]
        [MaxLength(12)]
        public string OrganizationUnitID { get; set; }

        [Key]
        [Column("company_id", Order = 1)]
        [Display(Name = "Company ID")]
        [MaxLength(5)]
        public string CompanyID { get; set; }

        [Key]
        [Index(IsUnique = true)]
        [Column("cost_center_id", Order = 2)]
        [Display(Name = "Cost Center ID")]
        [MaxLength(12)]
        public string CostCenterID { get; set; }

        public virtual Company Company { get; set; }

    }
}
