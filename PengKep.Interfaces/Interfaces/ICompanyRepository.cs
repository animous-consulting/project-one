using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Interfaces
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        List<Company> GetAccessibleCompanies(string userId);
    }
}
