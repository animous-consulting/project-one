using System;
using System.Collections.Generic;

using PengKep.BusinessEntities;

namespace PengKep.Common.Interfaces
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        List<Company> GetAccessibleCompanies(string userId);
    }
}
