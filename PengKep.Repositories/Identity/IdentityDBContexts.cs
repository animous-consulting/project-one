using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using PengKep.BusinessEntities;

namespace PengKep.Repositories
{

    public class IdentityDBContext : IdentityDbContext<ApplicationUser>
    {
        static IdentityDBContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }
        public IdentityDBContext()
            : base("DbIdentityConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser>, IUserStore<ApplicationUser>
    {
        public ApplicationUserStore(IdentityDBContext dbContext)
            : base(dbContext) { }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole>, IRoleStore<ApplicationRole>
    {
        public ApplicationRoleStore(IdentityDBContext dbContext)
            : base(dbContext) { }
    }

}