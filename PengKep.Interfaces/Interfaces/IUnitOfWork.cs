using System;
using System.Collections.Generic;

namespace PengKep.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        void Rollback();
    }
}
