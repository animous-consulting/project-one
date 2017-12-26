using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengKep.Entities
{
    [Table("tbl_account")]
    public class Account
    {
        [Key]
        [Column("account_code")]
        [Display(Name = "Account Code")]
        [MaxLength(15)]
        public string AccountCode { get; set; }

        [Column("account_description")]
        [Display(Name = "Account Description")]
        [MaxLength(50)]
        public string AccountDescription { get; set; }
    }
}
