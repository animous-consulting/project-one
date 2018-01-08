using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Common.Interfaces
{
    public interface IErrorLogRepository : IGenericRepository<ErrorLog>
    {
        String GetNewLogID(DateTime date);
    }
}
