﻿using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // custom properties
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        public string DisplayAddress
        {
            get
            {
                string dspAddress = string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity = string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspProvince = string.IsNullOrWhiteSpace(this.Province) ? "" : this.Province;
                string dspPostalCode = string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;
                return string.Format("{0} {1} {2} {3}", dspAddress, dspCity, dspProvince, dspPostalCode);
            }
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
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