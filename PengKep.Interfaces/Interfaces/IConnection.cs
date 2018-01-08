using System;
using System.Collections.Generic;
using System.Data.Common;

namespace PengKep.Common.Interfaces
{
    public interface IConnection
    {
        string Environment { get; }
        DbConnection GetConnection { get; }
    }
}
