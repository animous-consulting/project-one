using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PengKep.Common.Interfaces;

namespace PengKep.Controllers
{
    public class ManageCompanyController : Controller
    {

        private ICompanyRepository companyRepository;

        public ManageCompanyController(
            ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        // GET: ManageCompany
        public ActionResult Index()
        {
            var model = companyRepository.GetAccessibleCompanies(User.Identity.Name);
               
            return View(model);
        }
    }
}