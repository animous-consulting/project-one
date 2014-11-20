using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using aspnetmvc4.DAL;

namespace aspnetmvc4.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

    }
}
