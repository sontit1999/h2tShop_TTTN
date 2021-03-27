using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Admin.controller
{
    public class AdminsController : Controller
    {
        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }
    }
}