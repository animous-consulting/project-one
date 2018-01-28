using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;

namespace PengKep.Repositories
{
    public class ActualExpenseRepository : GenericRepository<ActualExpense>, IActualExpenseRepository
    {

        private IUnitOfWork unitOfWork;
        private IOrganizationUnitRepository organizationUnitRepository;

        public ActualExpenseRepository(
            DBContext context,
            IUnitOfWork unitOfWork,
            IOrganizationUnitRepository organizationUnitRepository)
            : base(context)
        {
            this.unitOfWork = unitOfWork;
            this.organizationUnitRepository = organizationUnitRepository;
        }

        public List<ActualExpense> GetActualExpense(string organizationUnitId, int year, int? month = null)
        {
            List<ActualExpense> actualExpenses;
            List<string> allOrganizationUnitChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId).Select(s => s.OrganizationUnitID).ToList();
            actualExpenses =
                (from q in this.Get(includeProperties: "Expense, OrganizationUnit")
                 where (allOrganizationUnitChildrenIDs.Contains(q.OrganizationUnitID) || q.OrganizationUnitID == organizationUnitId) && q.Year == year
                 group q by q.ExpenseID into g
                 select new
                 {
                     ExpenseID = g.FirstOrDefault().ExpenseID,
                     Expense = g.FirstOrDefault().Expense,
                     OrganizationUnitID = g.FirstOrDefault().OrganizationUnitID,
                     OrganizationUnit = g.FirstOrDefault().OrganizationUnit,
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
                 }).ToList().Select(s => new ActualExpense()
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
            return actualExpenses;
        }

        public List<ActualExpense> GetActualExpenseOfCurrentOrganizationUnit(string organizationUnitId, int year, int? month = null)
        {
            List<ActualExpense> actualExpenses;
            actualExpenses =
                (from q in this.Get(includeProperties: "Expense, OrganizationUnit")
                 where q.OrganizationUnitID == organizationUnitId && q.Year == year 
                 group q by q.ExpenseID into g
                 select new
                 {
                     ExpenseID = g.FirstOrDefault().ExpenseID,
                     Expense = g.FirstOrDefault().Expense,
                     OrganizationUnitID = g.FirstOrDefault().OrganizationUnitID,
                     OrganizationUnit = g.FirstOrDefault().OrganizationUnit,
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
                 }).ToList().Select(s => new ActualExpense()
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
            return actualExpenses;
        }

        public ActualExpense GetActualExpenseTotal(string organizationUnitId, int year, int? month = null, string expenseId = "")
        {
            ActualExpense actualExpense;
            List<string> allOrganizationUnitChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId).Select(s => s.OrganizationUnitID).ToList();
            var organizationUnit = organizationUnitRepository.GetByID(organizationUnitId);
            actualExpense =
                (from q in this.Get(includeProperties: "Expense")
                 where (allOrganizationUnitChildrenIDs.Contains(q.OrganizationUnitID) || q.OrganizationUnitID == organizationUnitId) && q.Year == year &&
                 (String.IsNullOrEmpty(expenseId) || (!String.IsNullOrEmpty(expenseId) && q.ExpenseID == expenseId))
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
                 }).ToList().Select(s => new ActualExpense()
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
                 }).DefaultIfEmpty(new ActualExpense()
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
            return actualExpense;
        }

        public List<ActualExpense> GetActualExpenseTotalGroupedByOrganizationUnit(string organizationUnitId, int year, int? month = null, string expenseId = "")
        {
            List<ActualExpense> actualExpenses = new List<ActualExpense>();
            var allOrganizationUnitChildren = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId, 2).ToList();
            organizationUnitRepository.ReorderByHierarchy(ref allOrganizationUnitChildren, false);
            var organizationUnitChildrenIDs = allOrganizationUnitChildren.Where(w => w.OrganizationUnitParentID == organizationUnitId).ToDictionary(t => t.OrganizationUnitID);
            var organizationUnit = organizationUnitRepository.GetByID(organizationUnitId);

            foreach (KeyValuePair<string, OrganizationUnit> entry in organizationUnitChildrenIDs)
            {
                List<string> allOrganizationUnitGrandChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(entry.Key).Select(s => s.OrganizationUnitID).ToList();
                ActualExpense actualExpense;

                actualExpense =
                    (from q in this.Get()
                     where (q.OrganizationUnitID == entry.Key || allOrganizationUnitGrandChildrenIDs.Contains(q.OrganizationUnitID)) && q.Year == year &&
                     (String.IsNullOrEmpty(expenseId) || (!String.IsNullOrEmpty(expenseId) && q.ExpenseID == expenseId)) 
                     group q by q.Year into g
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
                         Dec = g.Sum(s => s.Dec ?? 0),
                     }).ToList().Select(s => new ActualExpense()
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
                     }).DefaultIfEmpty(new ActualExpense()
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

                if (actualExpense != null) actualExpenses.Add(actualExpense);
            }


            var actualExpenseCurrentOrganizationUnit =
                (from q in this.Get()
                 where q.OrganizationUnitID == organizationUnitId && q.Year == year &&
                 (String.IsNullOrEmpty(expenseId) || (!String.IsNullOrEmpty(expenseId) && q.ExpenseID == expenseId)) 
                 group q by q.Year into g
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
                     Dec = g.Sum(s => s.Dec ?? 0),
                 }).ToList().Select(s => new ActualExpense()
                 {
                     OrganizationUnitID = organizationUnitId,
                     OrganizationUnit = organizationUnit,
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
                 }).DefaultIfEmpty(new ActualExpense()
                 {
                     OrganizationUnitID = organizationUnitId,
                     OrganizationUnit = organizationUnit,
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

            actualExpenses.Insert(0, actualExpenseCurrentOrganizationUnit);

            return actualExpenses;
        }

        public List<ActualExpense> GetActualExpenseTotalGroupedByExpenseCategory(string organizationUnitId, int year, int? month = null)
        {
            List<ActualExpense> actualExpenses;
            List<string> allOrganizationUnitChildrenIDs = organizationUnitRepository.GetAllOrganizationUnitChildren(organizationUnitId).Select(s => s.OrganizationUnitID).ToList();
            actualExpenses =
                (from q in this.Get(includeProperties: "Expense")
                 where (allOrganizationUnitChildrenIDs.Contains(q.OrganizationUnitID) || q.OrganizationUnitID == organizationUnitId) && q.Year == year
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
                 }).ToList().Select(s => new ActualExpense()
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
            return actualExpenses;
        }

        public double? GetActualExpenseTotalValue(string organizationUnitId, int year, int month, string expenseId = "")
        {
            double? value = null;
            var actualExpense = this.GetActualExpenseTotal(organizationUnitId, year, month, expenseId);
            if (actualExpense != null)
            {
                if (month == 1) { value = actualExpense.Jan ?? 0; }
                if (month == 2) { value = actualExpense.Feb ?? 0; }
                if (month == 3) { value = actualExpense.Mar ?? 0; }
                if (month == 4) { value = actualExpense.Apr ?? 0; }
                if (month == 5) { value = actualExpense.May ?? 0; }
                if (month == 6) { value = actualExpense.Jun ?? 0; }
                if (month == 7) { value = actualExpense.Jul ?? 0; }
                if (month == 8) { value = actualExpense.Aug ?? 0; }
                if (month == 9) { value = actualExpense.Sep ?? 0; }
                if (month == 10) { value = actualExpense.Oct ?? 0; }
                if (month == 11) { value = actualExpense.Nov ?? 0; }
                if (month == 12) { value = actualExpense.Dec ?? 0; }
            }
            return value;
        }

        public double? GetCumulativeActualExpenseTotalValue(string organizationUnitId, int year, int month, string expenseId = "")
        {
            double? value = null;
            var actualExpense = this.GetActualExpenseTotal(organizationUnitId, year, month, expenseId);
            if (actualExpense != null)
            {
                value =
                    ((month >= 1 ? actualExpense.Jan : 0) ?? 0) +
                    ((month >= 2 ? actualExpense.Feb : 0) ?? 0) +
                    ((month >= 3 ? actualExpense.Mar : 0) ?? 0) +
                    ((month >= 4 ? actualExpense.Apr : 0) ?? 0) +
                    ((month >= 5 ? actualExpense.May : 0) ?? 0) +
                    ((month >= 6 ? actualExpense.Jun : 0) ?? 0) +
                    ((month >= 7 ? actualExpense.Jul : 0) ?? 0) +
                    ((month >= 8 ? actualExpense.Aug : 0) ?? 0) +
                    ((month >= 9 ? actualExpense.Sep : 0) ?? 0) +
                    ((month >= 10 ? actualExpense.Oct : 0) ?? 0) +
                    ((month >= 11 ? actualExpense.Nov : 0) ?? 0) +
                    ((month >= 12 ? actualExpense.Dec : 0) ?? 0);
            }
            return value;
        }


    }
}
