using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.Entities;
using PengKep.Interfaces;

namespace PengKep.Repositories
{
    public class PMExpenseRepository : GenericRepository<PMExpense>, IPMExpenseRepository
    {

        private IUnitOfWork unitOfWork;
        private ICompanyRepository companyRepository;
        private IOrganizationUnitRepository organizationUnitRepository;

        public PMExpenseRepository(
            DBContext context,
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            IOrganizationUnitRepository organizationUnitRepository)
            : base(context)
        {
            this.unitOfWork = unitOfWork;
            this.companyRepository = companyRepository;
            this.organizationUnitRepository = organizationUnitRepository;
        }

        public List<PMExpense> GetPMExpense(List<string> companyIDs, string organizationUnitId, int year, int? month = null)
        {
            List<PMExpense> pmExpenses;
            List<string> allOrganizationUnitChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId, companyIDs).Select(s => s.OrganizationUnitID).ToList();
            if (companyIDs == null) companyIDs = new List<string>();
            pmExpenses =
                (from q in this.Get(includeProperties: "Expense, OrganizationUnit")
                 where (allOrganizationUnitChildrenIDs.Contains(q.OrganizationUnitID) || q.OrganizationUnitID == organizationUnitId) && q.Year == year &&
                 companyIDs.Contains(q.CompanyID)
                 group q by q.ExpenseID into g
                 select new
                 {
                     ExpenseID = g.FirstOrDefault().ExpenseID,
                     Expense = g.FirstOrDefault().Expense,
                     OrganizationUnitID = g.FirstOrDefault().OrganizationUnitID,
                     OrganizationUnit = g.FirstOrDefault().OrganizationUnit,
                     CompanyID = g.FirstOrDefault().CompanyID,
                     Jan = g.Sum(s => s.Jan ?? 0),
                     Feb = g.Sum(s => s.Feb ?? 0),
                     Mar = g.Sum(s => s.Mar ?? 0),
                     Apr = g.Sum(s => s.Apr ?? 0),
                     May = g.Sum(s => s.May ?? 0),
                     Jun = g.Sum(s => s.Jun ?? 0),
                     Jul = g.Sum(s => s.Jul ?? 0),
                     Aug = g.Sum(s => s.Aug ?? 0),
                     Sep = g.Sum(s => s.Sep ?? 0),
                     Oct = g.Sum(s => s.Oct ?? 0),
                     Nov = g.Sum(s => s.Nov ?? 0),
                     Dec = g.Sum(s => s.Dec ?? 0)
                 }).ToList().Select(s => new PMExpense()
                 {
                     ExpenseID = s.ExpenseID,
                     Expense = s.Expense,
                     OrganizationUnitID = s.OrganizationUnitID,
                     OrganizationUnit = s.OrganizationUnit,
                     CompanyID = s.CompanyID,
                     Year = year,
                     Jan = (month >= 1 || month == null ? s.Jan : 0),
                     Feb = (month >= 2 || month == null ? s.Feb : 0),
                     Mar = (month >= 3 || month == null ? s.Mar : 0),
                     Apr = (month >= 4 || month == null ? s.Apr : 0),
                     May = (month >= 5 || month == null ? s.May : 0),
                     Jun = (month >= 6 || month == null ? s.Jun : 0),
                     Jul = (month >= 7 || month == null ? s.Jul : 0),
                     Aug = (month >= 8 || month == null ? s.Aug : 0),
                     Sep = (month >= 9 || month == null ? s.Sep : 0),
                     Oct = (month >= 10 || month == null ? s.Oct : 0),
                     Nov = (month >= 11 || month == null ? s.Nov : 0),
                     Dec = (month >= 12 || month == null ? s.Dec : 0)
                 }).ToList();
            return pmExpenses;
        }

        public List<PMExpense> GetPMExpenseOfCurrentOrganizationUnit(List<string> companyIDs, string organizationUnitId, int year, int? month = null)
        {
            List<PMExpense> pmExpenses;
            if (companyIDs == null) companyIDs = new List<string>();
            pmExpenses =
                (from q in this.Get(includeProperties: "Expense, OrganizationUnit")
                 where q.OrganizationUnitID == organizationUnitId && q.Year == year &&
                 companyIDs.Contains(q.CompanyID)
                 group q by q.ExpenseID into g
                 select new
                 {
                     ExpenseID = g.FirstOrDefault().ExpenseID,
                     Expense = g.FirstOrDefault().Expense,
                     OrganizationUnitID = g.FirstOrDefault().OrganizationUnitID,
                     OrganizationUnit = g.FirstOrDefault().OrganizationUnit,
                     CompanyID = g.FirstOrDefault().CompanyID,
                     Jan = g.Sum(s => s.Jan ?? 0),
                     Feb = g.Sum(s => s.Feb ?? 0),
                     Mar = g.Sum(s => s.Mar ?? 0),
                     Apr = g.Sum(s => s.Apr ?? 0),
                     May = g.Sum(s => s.May ?? 0),
                     Jun = g.Sum(s => s.Jun ?? 0),
                     Jul = g.Sum(s => s.Jul ?? 0),
                     Aug = g.Sum(s => s.Aug ?? 0),
                     Sep = g.Sum(s => s.Sep ?? 0),
                     Oct = g.Sum(s => s.Oct ?? 0),
                     Nov = g.Sum(s => s.Nov ?? 0),
                     Dec = g.Sum(s => s.Dec ?? 0)
                 }).ToList().Select(s => new PMExpense()
                 {
                     ExpenseID = s.ExpenseID,
                     Expense = s.Expense,
                     OrganizationUnitID = s.OrganizationUnitID,
                     OrganizationUnit = s.OrganizationUnit,
                     Year = year,
                     Jan = (month >= 1 || month == null ? s.Jan : 0),
                     Feb = (month >= 2 || month == null ? s.Feb : 0),
                     Mar = (month >= 3 || month == null ? s.Mar : 0),
                     Apr = (month >= 4 || month == null ? s.Apr : 0),
                     May = (month >= 5 || month == null ? s.May : 0),
                     Jun = (month >= 6 || month == null ? s.Jun : 0),
                     Jul = (month >= 7 || month == null ? s.Jul : 0),
                     Aug = (month >= 8 || month == null ? s.Aug : 0),
                     Sep = (month >= 9 || month == null ? s.Sep : 0),
                     Oct = (month >= 10 || month == null ? s.Oct : 0),
                     Nov = (month >= 11 || month == null ? s.Nov : 0),
                     Dec = (month >= 12 || month == null ? s.Dec : 0)
                 }).ToList();
            return pmExpenses;
        }

        public PMExpense GetPMExpenseTotal(List<string> companyIDs, string organizationUnitId, int year, int? month = null, string expenseId = "")
        {
            PMExpense pmExpense;
            List<string> allOrganizationUnitChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId, companyIDs).Select(s => s.OrganizationUnitID).ToList();
            var organizationUnit = organizationUnitRepository.GetByID(organizationUnitId);
            pmExpense =
                (from q in this.Get(includeProperties: "Expense")
                 where (allOrganizationUnitChildrenIDs.Contains(q.OrganizationUnitID) || q.OrganizationUnitID == organizationUnitId) && q.Year == year &&
                 (String.IsNullOrEmpty(expenseId) || (!String.IsNullOrEmpty(expenseId) && q.ExpenseID == expenseId)) &&
                 companyIDs.Contains(q.CompanyID)
                 group q by 1 into g
                 select new
                 {
                     Jan = g.Sum(s => s.Jan ?? 0),
                     Feb = g.Sum(s => s.Feb ?? 0),
                     Mar = g.Sum(s => s.Mar ?? 0),
                     Apr = g.Sum(s => s.Apr ?? 0),
                     May = g.Sum(s => s.May ?? 0),
                     Jun = g.Sum(s => s.Jun ?? 0),
                     Jul = g.Sum(s => s.Jul ?? 0),
                     Aug = g.Sum(s => s.Aug ?? 0),
                     Sep = g.Sum(s => s.Sep ?? 0),
                     Oct = g.Sum(s => s.Oct ?? 0),
                     Nov = g.Sum(s => s.Nov ?? 0),
                     Dec = g.Sum(s => s.Dec ?? 0)
                 }).ToList().Select(s => new PMExpense()
                 {
                     OrganizationUnitID = organizationUnitId,
                     OrganizationUnit = organizationUnit,
                     Year = year,
                     Jan = (month >= 1 || month == null ? s.Jan : 0),
                     Feb = (month >= 2 || month == null ? s.Feb : 0),
                     Mar = (month >= 3 || month == null ? s.Mar : 0),
                     Apr = (month >= 4 || month == null ? s.Apr : 0),
                     May = (month >= 5 || month == null ? s.May : 0),
                     Jun = (month >= 6 || month == null ? s.Jun : 0),
                     Jul = (month >= 7 || month == null ? s.Jul : 0),
                     Aug = (month >= 8 || month == null ? s.Aug : 0),
                     Sep = (month >= 9 || month == null ? s.Sep : 0),
                     Oct = (month >= 10 || month == null ? s.Oct : 0),
                     Nov = (month >= 11 || month == null ? s.Nov : 0),
                     Dec = (month >= 12 || month == null ? s.Dec : 0)
                 }).DefaultIfEmpty(new PMExpense()
                 {
                     OrganizationUnitID = organizationUnitId,
                     OrganizationUnit = organizationUnit,
                     Year = year,
                     Jan = 0,
                     Feb = 0,
                     Mar = 0,
                     Apr = 0,
                     May = 0,
                     Jun = 0,
                     Jul = 0,
                     Aug = 0,
                     Sep = 0,
                     Oct = 0,
                     Nov = 0,
                     Dec = 0
                 }).SingleOrDefault();
            return pmExpense;
        }

        public PMExpense GetPMExpenseTotalOfCurrentOrganizationUnit(List<string> companyIDs, string organizationUnitId, int year, int? month = null, string expenseId = "")
        {
            PMExpense pmExpense;
            var organizationUnit = organizationUnitRepository.GetByID(organizationUnitId);
            pmExpense =
                (from q in this.Get(includeProperties: "Expense")
                 where q.OrganizationUnitID == organizationUnitId && q.Year == year &&
                 (String.IsNullOrEmpty(expenseId) || (!String.IsNullOrEmpty(expenseId) && q.ExpenseID == expenseId)) &&
                 companyIDs.Contains(q.CompanyID)
                 group q by 1 into g
                 select new
                 {
                     Jan = g.Sum(s => s.Jan ?? 0),
                     Feb = g.Sum(s => s.Feb ?? 0),
                     Mar = g.Sum(s => s.Mar ?? 0),
                     Apr = g.Sum(s => s.Apr ?? 0),
                     May = g.Sum(s => s.May ?? 0),
                     Jun = g.Sum(s => s.Jun ?? 0),
                     Jul = g.Sum(s => s.Jul ?? 0),
                     Aug = g.Sum(s => s.Aug ?? 0),
                     Sep = g.Sum(s => s.Sep ?? 0),
                     Oct = g.Sum(s => s.Oct ?? 0),
                     Nov = g.Sum(s => s.Nov ?? 0),
                     Dec = g.Sum(s => s.Dec ?? 0)
                 }).ToList().Select(s => new PMExpense()
                 {
                     OrganizationUnitID = organizationUnitId,
                     OrganizationUnit = organizationUnit,
                     Year = year,
                     Jan = (month >= 1 || month == null ? s.Jan : 0),
                     Feb = (month >= 2 || month == null ? s.Feb : 0),
                     Mar = (month >= 3 || month == null ? s.Mar : 0),
                     Apr = (month >= 4 || month == null ? s.Apr : 0),
                     May = (month >= 5 || month == null ? s.May : 0),
                     Jun = (month >= 6 || month == null ? s.Jun : 0),
                     Jul = (month >= 7 || month == null ? s.Jul : 0),
                     Aug = (month >= 8 || month == null ? s.Aug : 0),
                     Sep = (month >= 9 || month == null ? s.Sep : 0),
                     Oct = (month >= 10 || month == null ? s.Oct : 0),
                     Nov = (month >= 11 || month == null ? s.Nov : 0),
                     Dec = (month >= 12 || month == null ? s.Dec : 0)
                 }).DefaultIfEmpty(new PMExpense()
                 {
                     OrganizationUnitID = organizationUnitId,
                     OrganizationUnit = organizationUnit,
                     Year = year,
                     Jan = 0,
                     Feb = 0,
                     Mar = 0,
                     Apr = 0,
                     May = 0,
                     Jun = 0,
                     Jul = 0,
                     Aug = 0,
                     Sep = 0,
                     Oct = 0,
                     Nov = 0,
                     Dec = 0
                 }).SingleOrDefault();
            return pmExpense;
        }

        public List<PMExpense> GetPMExpenseTotalGroupedByOrganizationUnit(List<string> companyIDs, string organizationUnitId, int year, int? month = null, string expenseId = "")
        {
            List<PMExpense> pmExpenses = new List<PMExpense>();
            var allOrganizationUnitChildren = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId, companyIDs, 2).ToList();
            organizationUnitRepository.ReorderByHierarchy(ref allOrganizationUnitChildren, false);
            var organizationUnitChildrenIDs = allOrganizationUnitChildren.Where(w => w.OrganizationUnitParentID == organizationUnitId).ToDictionary(t => t.OrganizationUnitID);

            foreach (KeyValuePair<string, OrganizationUnit> entry in organizationUnitChildrenIDs)
            {
                List<string> allOrganizationUnitGrandChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(entry.Key, companyIDs).Select(s => s.OrganizationUnitID).ToList();
                PMExpense pmExpense;

                pmExpense =
                    (from q in this.Get()
                     where (q.OrganizationUnitID == entry.Key || allOrganizationUnitGrandChildrenIDs.Contains(q.OrganizationUnitID)) && q.Year == year &&
                     (String.IsNullOrEmpty(expenseId) || (!String.IsNullOrEmpty(expenseId) && q.ExpenseID == expenseId)) &&
                     companyIDs.Contains(q.CompanyID)
                     group q by 1 into g
                     select new
                     {
                         Jan = g.Sum(s => s.Jan ?? 0),
                         Feb = g.Sum(s => s.Feb ?? 0),
                         Mar = g.Sum(s => s.Mar ?? 0),
                         Apr = g.Sum(s => s.Apr ?? 0),
                         May = g.Sum(s => s.May ?? 0),
                         Jun = g.Sum(s => s.Jun ?? 0),
                         Jul = g.Sum(s => s.Jul ?? 0),
                         Aug = g.Sum(s => s.Aug ?? 0),
                         Sep = g.Sum(s => s.Sep ?? 0),
                         Oct = g.Sum(s => s.Oct ?? 0),
                         //var organizationUnitChildren = organizationUnitRepository.GetOrganizationUnitChildren(organizationUnitId, companyId).ToList().ToDictionary(t => t.OrganizationUnitID);
                         Nov = g.Sum(s => s.Nov ?? 0),
                         Dec = g.Sum(s => s.Dec ?? 0),
                     }).ToList().Select(s => new PMExpense()
                     {
                         OrganizationUnitID = entry.Key,
                         OrganizationUnit = entry.Value,
                         ExpenseID = expenseId,
                         Year = year,
                         Jan = (month >= 1 || month == null ? s.Jan : 0),
                         Feb = (month >= 2 || month == null ? s.Feb : 0),
                         Mar = (month >= 3 || month == null ? s.Mar : 0),
                         Apr = (month >= 4 || month == null ? s.Apr : 0),
                         May = (month >= 5 || month == null ? s.May : 0),
                         Jun = (month >= 6 || month == null ? s.Jun : 0),
                         Jul = (month >= 7 || month == null ? s.Jul : 0),
                         Aug = (month >= 8 || month == null ? s.Aug : 0),
                         Sep = (month >= 9 || month == null ? s.Sep : 0),
                         Oct = (month >= 10 || month == null ? s.Oct : 0),
                         Nov = (month >= 11 || month == null ? s.Nov : 0),
                         Dec = (month >= 12 || month == null ? s.Dec : 0)
                     }).DefaultIfEmpty(new PMExpense()
                     {
                         OrganizationUnitID = entry.Key,
                         OrganizationUnit = entry.Value,
                         ExpenseID = expenseId,
                         Year = year,
                         Jan = 0,
                         Feb = 0,
                         Mar = 0,
                         Apr = 0,
                         May = 0,
                         Jun = 0,
                         Jul = 0,
                         Aug = 0,
                         Sep = 0,
                         Oct = 0,
                         Nov = 0,
                         Dec = 0
                     }).SingleOrDefault();

                if (pmExpense != null) pmExpenses.Add(pmExpense);
            }

            if (organizationUnitId != "ALL")
            {
                var pmExpenseCurrentOrganizationUnit = this.GetPMExpenseTotalOfCurrentOrganizationUnit(companyIDs, organizationUnitId, year, month, expenseId);
                pmExpenses.Insert(0, pmExpenseCurrentOrganizationUnit);
            }
            return pmExpenses;
        }

        public List<PMExpense> GetPMExpenseTotalGroupedByExpenseCategory(List<string> companyIDs, string organizationUnitId, int year, int? month = null)
        {
            List<PMExpense> pmExpenses;
            List<string> allOrganizationUnitChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId, companyIDs).Select(s => s.OrganizationUnitID).ToList();
            pmExpenses =
                (from q in this.Get(includeProperties: "Expense")
                 where (allOrganizationUnitChildrenIDs.Contains(q.OrganizationUnitID) || q.OrganizationUnitID == organizationUnitId) && q.Year == year &&
                 companyIDs.Contains(q.CompanyID)
                 group q by q.Expense.ExpenseCategoryID into g
                 select new
                 {
                     ExpenseCategoryID = g.Key,
                     Year = g.FirstOrDefault().Year,
                     Jan = g.Sum(s => s.Jan ?? 0),
                     Feb = g.Sum(s => s.Feb ?? 0),
                     Mar = g.Sum(s => s.Mar ?? 0),
                     Apr = g.Sum(s => s.Apr ?? 0),
                     May = g.Sum(s => s.May ?? 0),
                     Jun = g.Sum(s => s.Jun ?? 0),
                     Jul = g.Sum(s => s.Jul ?? 0),
                     Aug = g.Sum(s => s.Aug ?? 0),
                     Sep = g.Sum(s => s.Sep ?? 0),
                     Oct = g.Sum(s => s.Oct ?? 0),
                     Nov = g.Sum(s => s.Nov ?? 0),
                     Dec = g.Sum(s => s.Dec ?? 0)
                 }).ToList().Select(s => new PMExpense()
                 {
                     Expense = new Expense { ExpenseCategoryID = s.ExpenseCategoryID },
                     Year = s.Year,
                     Jan = (month >= 1 || month == null ? s.Jan : 0),
                     Feb = (month >= 2 || month == null ? s.Feb : 0),
                     Mar = (month >= 3 || month == null ? s.Mar : 0),
                     Apr = (month >= 4 || month == null ? s.Apr : 0),
                     May = (month >= 5 || month == null ? s.May : 0),
                     Jun = (month >= 6 || month == null ? s.Jun : 0),
                     Jul = (month >= 7 || month == null ? s.Jul : 0),
                     Aug = (month >= 8 || month == null ? s.Aug : 0),
                     Sep = (month >= 9 || month == null ? s.Sep : 0),
                     Oct = (month >= 10 || month == null ? s.Oct : 0),
                     Nov = (month >= 11 || month == null ? s.Nov : 0),
                     Dec = (month >= 12 || month == null ? s.Dec : 0)
                 }).ToList();
            return pmExpenses;
        }

        public double? GetPMExpenseTotalValue(List<string> companyIDs, string organizationUnitId, int year, int month, string expenseId = "")
        {
            double? value = null;
            var pmExpense = this.GetPMExpenseTotal(companyIDs, organizationUnitId, year, month, expenseId);
            if (pmExpense != null)
            {
                if (month == 1) { value = pmExpense.Jan ?? 0; }
                if (month == 2) { value = pmExpense.Feb ?? 0; }
                if (month == 3) { value = pmExpense.Mar ?? 0; }
                if (month == 4) { value = pmExpense.Apr ?? 0; }
                if (month == 5) { value = pmExpense.May ?? 0; }
                if (month == 6) { value = pmExpense.Jun ?? 0; }
                if (month == 7) { value = pmExpense.Jul ?? 0; }
                if (month == 8) { value = pmExpense.Aug ?? 0; }
                if (month == 9) { value = pmExpense.Sep ?? 0; }
                if (month == 10) { value = pmExpense.Oct ?? 0; }
                if (month == 11) { value = pmExpense.Nov ?? 0; }
                if (month == 12) { value = pmExpense.Dec ?? 0; }
            }
            return value;
        }

        public double? GetCumulativePMExpenseTotalValue(List<string> companyIDs, string organizationUnitId, int year, int month, string expenseId = "")
        {
            double? value = null;
            var pmExpense = this.GetPMExpenseTotal(companyIDs, organizationUnitId, year, month, expenseId);
            if (pmExpense != null)
            {
                value =
                    ((month >= 1 ? pmExpense.Jan : 0) ?? 0) +
                    ((month >= 2 ? pmExpense.Feb : 0) ?? 0) +
                    ((month >= 3 ? pmExpense.Mar : 0) ?? 0) +
                    ((month >= 4 ? pmExpense.Apr : 0) ?? 0) +
                    ((month >= 5 ? pmExpense.May : 0) ?? 0) +
                    ((month >= 6 ? pmExpense.Jun : 0) ?? 0) +
                    ((month >= 7 ? pmExpense.Jul : 0) ?? 0) +
                    ((month >= 8 ? pmExpense.Aug : 0) ?? 0) +
                    ((month >= 9 ? pmExpense.Sep : 0) ?? 0) +
                    ((month >= 10 ? pmExpense.Oct : 0) ?? 0) +
                    ((month >= 11 ? pmExpense.Nov : 0) ?? 0) +
                    ((month >= 12 ? pmExpense.Dec : 0) ?? 0);
            }
            return value;
        }


    }
}
