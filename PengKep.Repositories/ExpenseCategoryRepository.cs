using System;
using System.Collections.Generic;

using PengKep.Entities;
using PengKep.Interfaces;

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
