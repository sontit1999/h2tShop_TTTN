using h2tshop.Models;
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
        public ActionResult Index(string keyword="",int page=0)
        {
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.lsp = lsp;
            var listSP = UtilsDatabase.getDaTaBase().SanPhams.Where(p=>p.TenSanPham.ToLower().Contains(keyword.Trim().ToLower()) || p.MoTa.ToLower().Contains(keyword.Trim().ToLower())).ToList();
            ViewBag.listSP = listSP;
            ViewBag.sl = listSP.Count;
            ViewBag.keyword = keyword;
            ViewBag.listsp = listSP;

            return View();
        }
        
    }
}