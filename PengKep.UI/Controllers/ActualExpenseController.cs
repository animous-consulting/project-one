using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PengKep.BusinessEntities;
using PengKep.Common.Interfaces;
using PengKep.Common.Constants;
using PengKep.ViewModels;
using PengKep.Helpers;

namespace PengKep.UI.Controllers
{
    [Authorize()]
    public class ActualExpenseController : Controller
    {

        private IUnitOfWork unitOfWork;
        private IApprovalRepository approvalRepository;
        private IOrganizationUnitRepository organizationUnitRepository;
        private IExpenseCategoryRepository expenseCategoryRepository;
        private IExpenseRepository expenseRepository;
        private IActualExpenseRepository actualExpenseRepository;

        public ActualExpenseController(
            IUnitOfWork unitOfWork,
            IApprovalRepository approvalRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            IExpenseCategoryRepository expenseCategoryRepository,
            IExpenseRepository expenseRepository,
            IActualExpenseRepository actualExpenseRepository)
        {
            this.unitOfWork = unitOfWork;
            this.approvalRepository = approvalRepository;
            this.organizationUnitRepository = organizationUnitRepository;
            this.expenseCategoryRepository = expenseCategoryRepository;
            this.expenseRepository = expenseRepository;
            this.actualExpenseRepository = actualExpenseRepository;
        }

        //
        // GET: /ActualExpense/

        public ActionResult Index(string organizationUnitId, int? year)
        {

            var accessibleOrganizationUnits = organizationUnitRepository.GetAccessibleOrganizationUnit(User.Identity.Name)
               .Select(s => new OrganizationUnit { OrganizationUnitID = s.OrganizationUnitID, OrganizationUnitName = s.OrganizationUnitName, OrganizationUnitParentID = s.OrganizationUnitParentID }).ToList();

            ActualExpenseDisplayViewModel model = new ActualExpenseDisplayViewModel();

            if (String.IsNullOrEmpty(organizationUnitId))
            {
                if (Session["_organizationunitid"] != null)
                {
                    int _year = DateTime.Now.Year;
                    var _organizationUnitid = Session["_organizationunitid"] as string;
                    if (Session["_year"] != null)
                    {
                        _year = Convert.ToInt32(Session["_year"] as string);
                    }
                    return RedirectToAction("Index", new { organizationUnitId = _organizationUnitid, year = _year });
                }
            }

            if (!String.IsNullOrEmpty(organizationUnitId) && year != null)
            {
                if (!accessibleOrganizationUnits.Any(a => a.OrganizationUnitID == organizationUnitId))
                {
                    ViewBag.ErrorMessage = "You are not authorized to view this data";
                }
                else
                {

                    // set sessions
                    Session["_organizationunitid"] = organizationUnitId;
                    Session["_year"] = year.Value.ToString();

                    var organizationUnit = organizationUnitRepository.GetByID(organizationUnitId);

                    model.ActualExpense = AutoMapper.Mapper.Map<List<ActualExpenseViewModel>>(
                        actualExpenseRepository.GetActualExpense( organizationUnitId, year.Value, null));

                    if (!organizationUnitRepository.IsLeaf(organizationUnitId))
                    {
                        model.ActualExpenseByOrganizationUnit = AutoMapper.Mapper.Map<List<ActualExpenseViewModel>>(
                            actualExpenseRepository.GetActualExpenseTotalGroupedByOrganizationUnit(organizationUnitId, year.Value, null));
                    }

                    ViewBag.IsEditable = false;

                    ViewBag.OrganizationUnitID = organizationUnitId;
                    ViewBag.OrganizationUnitName = organizationUnitId != "ALL" ? organizationUnit.OrganizationUnitName : "";
                    ViewBag.Year = year;
                }
            }

            organizationUnitRepository.ReorderByHierarchy(ref accessibleOrganizationUnits, false);

            ViewBag.OrganizationUnitTreeData = organizationUnitRepository.GetOrganizationUnitTreeData(User.Identity.Name);
            ViewBag.OrganizationUnits = AutoMapper.Mapper.Map<List<OrganizationUnitViewModel>>(accessibleOrganizationUnits);
            ViewBag.ExpenseCategories = AutoMapper.Mapper.Map<List<ExpenseCategory>>(
                (from q in expenseCategoryRepository.Get()
                 select q).ToList());

            int minyear = actualExpenseRepository.Get().Select(s => s.Year).DefaultIfEmpty(DateTime.Now.Year).Min();
            int maxyear = actualExpenseRepository.Get().Select(s => s.Year).DefaultIfEmpty(DateTime.Now.Year).Max();

            ViewBag.Years =
                (from q in Enumerable.Range(minyear, maxyear - minyear + 2)
                 orderby q
                 select q).ToList();

            return View(model);
        }

        [HttpPost]
        public JsonResult GetChartData(string organizationUnitId, int year)
        {

            var actualExpenses =
                (from q in actualExpenseRepository.GetActualExpenseTotalGroupedByExpenseCategory(organizationUnitId, year, null)
                 select new ActualExpense()
                 {
                     Expense = q.Expense,
                     Jan = (q.Jan ?? 0) / 1000,
                     Feb = (q.Feb ?? 0) / 1000,
                     Mar = (q.Mar ?? 0) / 1000,
                     Apr = (q.Apr ?? 0) / 1000,
                     May = (q.May ?? 0) / 1000,
                     Jun = (q.Jun ?? 0) / 1000,
                     Jul = (q.Jul ?? 0) / 1000,
                     Aug = (q.Aug ?? 0) / 1000,
                     Sep = (q.Sep ?? 0) / 1000,
                     Oct = (q.Oct ?? 0) / 1000,
                     Nov = (q.Nov ?? 0) / 1000,
                     Dec = (q.Dec ?? 0) / 1000,
                 }).DefaultIfEmpty(new ActualExpense()).ToList();

            var actualExpense =
               (from q in actualExpenses
                group q by 1 into g
                select new ActualExpense()
                {
                    OrganizationUnitID = organizationUnitId,
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
                    Dec = g.Sum(s => s.Dec ?? 0),
                }).SingleOrDefault();

            var actualExpenseRoutine = actualExpenses.Where(w => w.Expense != null && w.Expense.ExpenseCategoryID == "ROU").DefaultIfEmpty(new ActualExpense()).SingleOrDefault();
            var actualExpenseNonRoutine = actualExpenses.Where(w => w.Expense != null && w.Expense.ExpenseCategoryID == "NRO").DefaultIfEmpty(new ActualExpense()).SingleOrDefault();

            var chartDataActualExpenseRoutine =
                new List<double>{
                        Math.Round((actualExpenseRoutine.Jan ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Feb ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Mar ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Apr ?? 0), 2),
                        Math.Round((actualExpenseRoutine.May ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Jun ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Jul ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Aug ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Sep ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Oct ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Nov ?? 0), 2),
                        Math.Round((actualExpenseRoutine.Dec ?? 0), 2)
                    };

            var chartDataActualExpenseNonRoutine =
                new List<double>{
                        Math.Round((actualExpenseNonRoutine.Jan ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Feb ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Mar ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Apr ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.May ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Jun ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Jul ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Aug ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Sep ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Oct ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Nov ?? 0), 2),
                        Math.Round((actualExpenseNonRoutine.Dec ?? 0), 2)
                    };

            List<double> chartDataActualExpenseCummulative = new List<double>();
            if (actualExpense != null)
            {
                chartDataActualExpenseCummulative = new List<double>
                {
                    Math.Round((actualExpense.Jan ?? 0 ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0)), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0)), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0)), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) + (actualExpense.Jun ?? 0) ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) + (actualExpense.Jun ?? 0) + (actualExpense.Jul ?? 0) ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) + (actualExpense.Jun ?? 0) + (actualExpense.Jul ?? 0) + (actualExpense.Aug ?? 0) ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) + (actualExpense.Jun ?? 0) + (actualExpense.Jul ?? 0) + (actualExpense.Aug ?? 0) + (actualExpense.Sep ?? 0) ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) + (actualExpense.Jun ?? 0) + (actualExpense.Jul ?? 0) + (actualExpense.Aug ?? 0) + (actualExpense.Sep ?? 0)+  (actualExpense.Oct ?? 0) ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) + (actualExpense.Jun ?? 0) + (actualExpense.Jul ?? 0) + (actualExpense.Aug ?? 0) + (actualExpense.Sep ?? 0)+  (actualExpense.Oct ?? 0) +  (actualExpense.Nov ?? 0) ), 2),
                    Math.Round(((actualExpense.Jan ?? 0) +  (actualExpense.Feb ?? 0) + (actualExpense.Mar ?? 0) + (actualExpense.Apr ?? 0) + (actualExpense.May ?? 0) + (actualExpense.Jun ?? 0) + (actualExpense.Jul ?? 0) + (actualExpense.Aug ?? 0) + (actualExpense.Sep ?? 0)+  (actualExpense.Oct ?? 0) +  (actualExpense.Nov ?? 0) + (actualExpense.Dec ?? 0) ), 2)
                };
            }

            if (chartDataActualExpenseRoutine == null) { chartDataActualExpenseRoutine = (from q in Enumerable.Range(1, 12) select (double)0).ToList(); }
            if (chartDataActualExpenseNonRoutine == null) { chartDataActualExpenseNonRoutine = (from q in Enumerable.Range(1, 12) select (double)0).ToList(); }
            if (chartDataActualExpenseCummulative == null) { chartDataActualExpenseCummulative = (from q in Enumerable.Range(1, 12) select (double)0).ToList(); }

            IEnumerable<double>[] chartData =
            {
                chartDataActualExpenseCummulative,
                chartDataActualExpenseRoutine,
                chartDataActualExpenseNonRoutine
            };
            return Json(new { status = "OK", data = chartData });
        }
    }
}
