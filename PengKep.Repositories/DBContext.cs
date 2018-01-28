using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

using PengKep.Common.Interfaces;
using PengKep.BusinessEntities;

namespace PengKep.Repositories
{
    public class DBContext : DbContext
    {
        static DBContext()
        {
            Database.SetInitializer<DBContext>(new DropCreateDatabaseIfModelChanges<DBContext>());
        }
        public DBContext()
            : base("DbApplicationConnection")
        {

        }

        public DbSet<Approval> Approvals { get; set; }
        public DbSet<ApprovalAction> ApprovalActions { get; set; }
        public DbSet<ApprovalHistory> ApprovalHistories { get; set; }
        public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<ActualExpense> PMExpenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // avoid cascade delete
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}