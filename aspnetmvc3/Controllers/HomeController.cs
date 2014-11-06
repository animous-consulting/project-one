using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using aspnetmvc3.DAL;

namespace aspnetmvc3.Controllers
{
	public class HomeController : Controller
	{

		DBContext db = new DBContext();

		public ActionResult Index ()
		{

			return View (db.Users.ToList());
		}
	}
}

