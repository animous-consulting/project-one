using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengKep.Entities
{

    [Table("tbl_company")]
    public class Company
    {
        [Key]
        [Column("company_id")]
        [Display(Name = "Company ID")]
        [MaxLength(5)]
        public string CompanyID { get; set; }

        [Column("company_name")]
        [Display(Name = "Company Name")]
        [MaxLength(30)]
        public string CompanyName { get; set; }
    }
}
