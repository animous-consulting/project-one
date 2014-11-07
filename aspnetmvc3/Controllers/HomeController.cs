using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using aspnetmvc3.DAL;

namespace aspnetmvc3.Controllers
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
