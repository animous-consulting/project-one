using System;
using System.Collections.Generic;
using System.Data.Entity;

using aspnetmvc4.Models;

namespace aspnetmvc4.DAL
{
    public class DBInitializer : DropCreateDatabaseAlways<DBContext>
    {

        protected override void Seed(DBContext context)
        {
            base.Seed(context);

            var users = new List<User>
            {
                new User { UserID = "agip", UserName = "Anggi Prima Nadvi", EmailAddress = "anggiprimanyl@gmail.com" },
                new User { UserID = "aston", UserName = "Aston Dihor", EmailAddress = "astondihor@gmail.com" }
            };

            users.ForEach(x => context.Users.Add(x));
            context.SaveChanges();

        }

    }
}