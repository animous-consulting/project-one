using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_config")]
    public class Config
    {
        [Key]
        [Required]
        [Column("config_id")]
        [Display(Name = "Config ID")]
        [MaxLength(5)]
        public string ConfigID { get; set; }

        [Column("config_val")]
        [Display(Name = "Config Value")]
        public string ConfigValue { get; set; }

        [Column("config_desc")]
        [Display(Name = "Config Description")]
        [MaxLength(50)]
        public string ConfigDesc { get; set; }
    }
}
