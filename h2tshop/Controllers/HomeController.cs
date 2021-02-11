using h2tshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var listSPNew = UtilsDatabase.getDaTaBase().SanPhams.OrderByDescending(p => p.MaSanPham).ToList();
            ViewBag.listSPNew = listSPNew;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}