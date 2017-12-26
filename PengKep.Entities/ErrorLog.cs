using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PengKep.Entities
{
    [Table("tbl_error_log")]
    public class ErrorLog
    {
        [Key]
        [Column("log_id")]
        [Display(Name = "Log ID")]
        [MaxLength(20)]
        public string LogID { get; set; }

        [Column("log_date")]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm}")]
        public DateTime LogDate { get; set; }

        [Column("page")]
        [Display(Name = "Page")]
        [MaxLength(200)]
        public string Page { get; set; }

        [Column("user_host_address")]
        [Display(Name = "User Host Address")]
        [MaxLength(30)]
        public string UserHostAddress { get; set; }

        [Column("user_host_name")]
        [Display(Name = "User Host Name")]
        [MaxLength(30)]
        public string UserHostName { get; set; }

        [Column("message")]
        [Display(Name = "Error Message")]
        [DataType("TEXT")]
        public string Message { get; set; }

    }
}
