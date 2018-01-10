using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Web;

using Unity;
using Unity.Injection;
using Unity.Lifetime;

using PengKep.Common.Interfaces;
using PengKep.Repositories;

namespace PengKep
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            // asp.net identity 2.0
            container.RegisterType<IdentityDBContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, ApplicationUserStore>();
            container.RegisterType<IRoleStore<ApplicationRole>, ApplicationRoleStore>();

            // application
            container.RegisterType<DBContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType<IApprovalActionRepository, ApprovalActionRepository>();
            container.RegisterType<IApprovalHistoryRepository, ApprovalHistoryRepository>();
            container.RegisterType<IApprovalRepository, ApprovalRepository>();
            container.RegisterType<IApprovalStatusRepository, ApprovalStatusRepository>();
            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<IConfigRepository, ConfigRepository>();
            container.RegisterType<IErrorLogRepository, ErrorLogRepository>();
            container.RegisterType<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            container.RegisterType<IExpenseRepository, ExpenseRepository>();
            container.RegisterType<IOrganizationUnitRepository, OrganizationUnitRepository>();
            container.RegisterType<IPMExpenseRepository, PMExpenseRepository>();

        }
    }
}