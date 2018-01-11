using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PengKep.ViewModels
{
    public class RoleViewModel
    {
      
        [Display(Name = "Role ID")]
        public string Id { get; set; }

        [Display(Name = "Role")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string RoleGroup { get; set; }
    }
}