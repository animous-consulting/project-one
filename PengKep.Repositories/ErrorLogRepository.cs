using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.Entities;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class ErrorLogRepository : GenericRepository<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(DBContext context)
            : base(context)
        {

        }

        public String GetNewLogID(DateTime date)
        {
            int digit = 5;
            String newid = "";
            String prefix = date.Year.ToString() + date.Month.ToString();
            String maxid = this.Get().Where(w => w.LogID.StartsWith(prefix)).Max(m => m.LogID);
            if (String.IsNullOrEmpty(maxid))
            {
                newid = prefix + (Math.Pow(10, digit) + 1).ToString().Substring(1, digit);
            }
            else
            {
                newid = prefix + (Convert.ToInt32(maxid.Replace(prefix, "")) + Math.Pow(10, digit) + 1).ToString().Substring(1, digit);
            }
            return newid;
        }
    }
}
