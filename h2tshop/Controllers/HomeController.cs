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
            var listSPNew = UtilsDatabase.getDaTaBase().SanPhams.OrderByDescending(p => p.MaSanPham).OrderBy(p => Guid.NewGuid())
                        .Take(8).ToList();
            var listSPSale = UtilsDatabase.getDaTaBase().SanPhams.Where(p=>p.KhuyenMai>=20).OrderByDescending(p => p.KhuyenMai).OrderBy(p => Guid.NewGuid())
                        .Take(8).ToList();
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.listSPNew = listSPNew;
            ViewBag.listSPSale = listSPSale;
            ViewBag.lsp = lsp;
            return View();
        }
        public ActionResult Thanks()
        {
            var listSPNew = UtilsDatabase.getDaTaBase().SanPhams.OrderByDescending(p => p.MaSanPham).OrderBy(p => Guid.NewGuid())
                        .Take(8).ToList();
            var listSPSale = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.KhuyenMai >= 20).OrderByDescending(p => p.KhuyenMai).OrderBy(p => Guid.NewGuid())
                        .Take(8).ToList();
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.listSPNew = listSPNew;
            ViewBag.listSPSale = listSPSale;
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