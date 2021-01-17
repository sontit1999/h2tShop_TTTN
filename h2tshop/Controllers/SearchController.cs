using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Controllers
{
    public class SearchController : Controller
    {
        
        [HttpPost]
        public ActionResult Index(string keyword="")
        {
            ViewBag.keyword = keyword;
            return View();
        }
    }
}