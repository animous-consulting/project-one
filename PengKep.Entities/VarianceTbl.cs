using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_variance",Schema = "dbo")]
    public class Variance
    {
        [Key]
        [Column("var_id")]
        [Display(Name = "Variance ID")]
        [MaxLength(6)]
        public string VarianceID { get; set; }

        [Column("var_name")]
        [Display(Name = "Variance Name")]
        [MaxLength(255)]
        public string VarianceName { get; set; }
    }
}
