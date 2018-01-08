using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Common.Interfaces
{
    public interface IConfigRepository : IGenericRepository<Config>
    {
        string GetValue(string configId);
    }
}
