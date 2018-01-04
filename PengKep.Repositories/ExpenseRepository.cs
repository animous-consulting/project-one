using System;
using System.Collections.Generic;

using PengKep.Entities;
using PengKep.Interfaces;

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
