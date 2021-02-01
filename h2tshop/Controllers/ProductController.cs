using h2tshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int id = 0)
        {
            var sp = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.MaSanPham == id).FirstOrDefault();
            ViewBag.sp = sp;
            return View();
        }
    }
}