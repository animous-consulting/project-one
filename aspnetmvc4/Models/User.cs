using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnetmvc4.Models
{
    public class User
    {

        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }

    }
}