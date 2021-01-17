using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Controllers
{
    public class CagetoryController : Controller
    {
        // GET: Cagetory
        public ActionResult Index(int id=0)
        {
            return View();
        }
    }
}