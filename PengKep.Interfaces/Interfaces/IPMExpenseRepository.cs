using System;
using System.Collections.Generic;

using PengKep.Entities;

namespace PengKep.Interfaces
{
    public interface IPMExpenseRepository : IGenericRepository<PMExpense>
    {
        List<PMExpense> GetPMExpense(List<string> companyIDs, string organizationUnitId, int year, int? month = null);
        List<PMExpense> GetPMExpenseOfCurrentOrganizationUnit(List<string> companyIDs, string organizationUnitid, int year, int? month = null);
        PMExpense GetPMExpenseTotal(List<string> companyIDs, string organizationUnitId, int year, int? month = null, string expenseId = "");
        PMExpense GetPMExpenseTotalOfCurrentOrganizationUnit(List<string> companyIDs, string organizationUnitId, int year, int? month = null, string expenseId = "");
        List<PMExpense> GetPMExpenseTotalGroupedByOrganizationUnit(List<string> companyIDs, string organizationUnitId, int year, int? month = null, string expenseId = "");
        List<PMExpense> GetPMExpenseTotalGroupedByExpenseCategory(List<string> companyIDs, string organizationUnitId, int year, int? month = null);
        double? GetPMExpenseTotalValue(List<string> companyIDs, string organizationUnitId, int year, int month, string expenseId = "");
        double? GetCumulativePMExpenseTotalValue(List<string> companyIDs, string organizationUnitId, int year, int month, string expenseId = "");
    }
}
