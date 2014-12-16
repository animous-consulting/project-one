using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Common;


using aspnetmvc4.Models;
using aspnetmvc4.DAL;

namespace aspnetmvc4.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    //[DbConfigurationType(GetType(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DBContext : DbContext
    {

        public DBContext() : base("myConn")
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        static DBContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<User> Users { get; set; }
    }
}