using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_user")]
    public class User
    {
        [Key]
        [Column("user_id")]
        [MinLength(2, ErrorMessage = "{0} field must be at least {1} characters length")]
        [MaxLength(30, ErrorMessage = "Maximum characters length for {0} field is {1} characters")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Column("user_name")]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Column("email_addres")]
        [MaxLength(50)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Column("last_access")]
        [Display(Name = "Last Access")]
        public DateTime? LastAccess { get; set; }

        [Column("is_active")]
        [MaxLength(1)]
        [Display(Name = "Active")]
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

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}