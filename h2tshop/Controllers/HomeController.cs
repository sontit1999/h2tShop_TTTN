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
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.listSPNew = listSPNew;
            ViewBag.lsp = lsp;
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