using System;
using System.Collections.Generic;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(DBContext context)
            : base(context)
        {

        }

    }
}
