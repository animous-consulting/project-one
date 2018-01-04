using System;
using System.Collections.Generic;

using PengKep.Entities;
using PengKep.Interfaces;

namespace PengKep.Repositories
{
    public class ConfigRepository : GenericRepository<Config>, IConfigRepository
    {
        public ConfigRepository(DBContext context)
            : base(context)
        {

        }

        public string GetValue(string configId)
        {
            var configVal = String.Empty;
            var config = this.GetByID(configId);
            if (config != null)
            {
                configVal = config.ConfigValue;
            }
            return configVal;
        }
    }
}
