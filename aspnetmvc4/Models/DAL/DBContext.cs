using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Configuration;

using aspnetmvc4.Models;

namespace aspnetmvc4.DAL

    
{
   // [DbConfigurationType(GetType(MySql.Data.Entity.MySqlEFConfiguration))]

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DBContext : DbContext
    {

        public DBContext()
            : base("myConn")
        {
            
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        static DBContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}