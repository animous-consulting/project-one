using System;
using System.Collections.Generic;

using PengKep.BusinessEntities;

namespace PengKep.Common.Interfaces
{
    public interface IActualExpenseRepository : IGenericRepository<ActualExpense>
    {
        List<ActualExpense> GetActualExpense(string organizationUnitId, int year, int? month = null);
        List<ActualExpense> GetActualExpenseOfCurrentOrganizationUnit(string organizationUnitid, int year, int? month = null);
        ActualExpense GetActualExpenseTotal(string organizationUnitId, int year, int? month = null, string expenseId = "");
        List<ActualExpense> GetActualExpenseTotalGroupedByOrganizationUnit(string organizationUnitId, int year, int? month = null, string expenseId = "");
        List<ActualExpense> GetActualExpenseTotalGroupedByExpenseCategory(string organizationUnitId, int year, int? month = null);
        double? GetActualExpenseTotalValue(string organizationUnitId, int year, int month, string expenseId = "");
        double? GetCumulativeActualExpenseTotalValue(string organizationUnitId, int year, int month, string expenseId = "");
    }
}
