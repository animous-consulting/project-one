using System;
using System.Collections.Generic;

namespace PengKep.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        void Rollback();
    }
}
