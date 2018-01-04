using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Interfaces
{
    public interface IErrorLogRepository : IGenericRepository<ErrorLog>
    {
        String GetNewLogID(DateTime date);
    }
}
