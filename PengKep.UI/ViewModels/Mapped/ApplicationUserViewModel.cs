using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PengKep.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string DisplayAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public ICollection<ApplicationUserRoleViewModel> Roles { get; set; }
    }
}