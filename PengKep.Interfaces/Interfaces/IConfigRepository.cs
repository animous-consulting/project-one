using System;
using System.Collections.Generic;

using PengKep.BusinessEntities;

namespace PengKep.Common.Interfaces
{
    public interface IConfigRepository : IGenericRepository<Config>
    {
        string GetValue(string configId);
    }
}
