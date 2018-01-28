using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PengKep.BusinessEntities;
using PengKep.Helpers;
using PengKep.Common.Interfaces;
using PengKep.Common.Constants;
using PengKep.ViewModels;

namespace PengKep.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrganizationUnitController : Controller
    {

        private IUnitOfWork unitOfWork;
        private IOrganizationUnitRepository organizationUnitRepository;

        public OrganizationUnitController(
            IUnitOfWork unitOfWork,
            IOrganizationUnitRepository organizationUnitRepository)
        {
            this.unitOfWork = unitOfWork;
            this.organizationUnitRepository = organizationUnitRepository;
        }

        // GET: OrganizationUnit
        public ActionResult Index()
        {
            var model = GetData();

            return View(model);
        }

        private List<OrganizationUnitViewModel> GetData()
        {
            var organizationUnits =
                (from q in organizationUnitRepository.Get()
                 where q.OrganizationUnitID != "ALL"
                 select q).ToList();
            organizationUnitRepository.ReorderByHierarchy(ref organizationUnits, true);
            var model = AutoMapper.Mapper.Map<List<OrganizationUnitViewModel>>(organizationUnits);
            return model;
        }

        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "CreatedBy, CreatedDate, LastUpdatedDate")]OrganizationUnit model)
        {
            String result = "";
            bool process = true;
            if (ModelState.IsValid)
            {
                if (organizationUnitRepository.Get().Any(a => a.OrganizationUnitID.ToLower() == model.OrganizationUnitID.ToLower()))
                {
                    result = "Organization Unit ID specified has already exists"; process = false;
                }

                if (process)
                {

                    if (String.IsNullOrEmpty(model.OrganizationUnitParentID)) model.OrganizationUnitParentID = "ALL";

                    model.CreatedBy = User.Identity.Name;
                    model.CreatedDate = DateTime.Now;
                    organizationUnitRepository.Insert(model);
                    unitOfWork.Commit();

                    var returnmodel = AutoMapper.Mapper.Map<OrganizationUnitViewModel>(model);
                    return Json(new { result = "OK", model = GetData() });
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var err in ModelState[key].Errors)
                    {
                        result = err.ErrorMessage + "\n";
                    }
                }
            }
            return Json(new { result = result });
        }

        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "CreatedBy, CreatedDate, LastUpdatedDate")]OrganizationUnit model)
        {
            String result = "";
            bool process = true;
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(model.LastUpdateRemark))
                {
                    result = "Update reason cannot be blank"; process = false;
                }

                if (process)
                {

                    var editedModel = organizationUnitRepository.GetByID(model.OrganizationUnitID);

                    var parentIDChanged = false;
                    if (editedModel.OrganizationUnitParentID != model.OrganizationUnitParentID) parentIDChanged = true;

                    editedModel.IsActive = model.IsActive;
                    editedModel.OrganizationUnitName = model.OrganizationUnitName;
                    editedModel.OrganizationUnitParentID = model.OrganizationUnitParentID;
                    if (String.IsNullOrEmpty(model.OrganizationUnitParentID)) editedModel.OrganizationUnitParentID = "ALL";
                    editedModel.LastUpdatedBy = User.Identity.Name;
                    editedModel.LastUpdatedDate = DateTime.Now;
                    organizationUnitRepository.Update(editedModel);
                    unitOfWork.Commit();

                    if (!parentIDChanged)
                    {
                        List<OrganizationUnit> organizationUnits = new List<OrganizationUnit>() { model };
                        var organizationUnit = model;
                        while (!String.IsNullOrEmpty(organizationUnit.OrganizationUnitParentID) && organizationUnit.OrganizationUnitParentID != "ALL")
                        {
                            // get parent hierarchy to have proper indentation
                            organizationUnit = organizationUnitRepository.GetByID(organizationUnit.OrganizationUnitParentID);
                            organizationUnits.Add(organizationUnit);
                        }
                        organizationUnitRepository.ReorderByHierarchy(ref organizationUnits, true);
                        return Json(new { result = "OK", model = AutoMapper.Mapper.Map<OrganizationUnitViewModel>(organizationUnits.Last()) });
                    }
                    else
                    {
                        return Json(new { result = "OK", model = GetData() });
                    }
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var err in ModelState[key].Errors)
                    {
                        result = err.ErrorMessage + "\n";
                    }
                }
            }
            return Json(new { result = result });
        }
    }
}