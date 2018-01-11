using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PengKep.BusinessEntities
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
