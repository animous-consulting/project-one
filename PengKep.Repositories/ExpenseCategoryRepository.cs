using System;
using System.Collections.Generic;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(DBContext context)
            : base(context)
        {

        }

    }
}
