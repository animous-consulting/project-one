using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_user_role")]
    public class UserRole
    {
        [Key]
        [Column("user_id", Order = 0)]
        [MaxLength(30)]
        public string UserID { get; set; }

        [Key]
        [ForeignKey("Role")]
        [Column("role_id", Order = 1)]
        [MaxLength(5)]
        public string RoleID { get; set; }

        public virtual Role Role { get; set; }
    }
}