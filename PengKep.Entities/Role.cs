using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_role")]
    public class Role
    {
        [Key]
        [Column("role_id")]
        [MinLength(2, ErrorMessage = "{0} field must be at least {1} characters length")]
        [MaxLength(5, ErrorMessage = "Maximum characters length for {0} field is {1} characters")]
        [Display(Name = "Role ID")]
        public string RoleID { get; set; }

        [Column("role_name")]
        [MaxLength(30)]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Column("role_desc")]
        [MaxLength(200)]
        [Display(Name = "Description")]
        public string RoleDesc { get; set; }

        [Column("display_order")]
        public int? DisplayOrder { get; set; }
    }
}