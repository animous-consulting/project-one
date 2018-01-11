using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PengKep.Common.Interfaces;
using PengKep.BusinessEntities;

namespace PengKep.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DBContext context;

        public UnitOfWork(DBContext context)
        {
            this.context = context;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void Rollback()
        {
            context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}