using h2tshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuanLyDonHang()
        {
            return View();
        }
        public ActionResult QuanLyTaiKhoan()
        {
            return View();
        }
        public ActionResult QuanLySanPham()
        {
            var listSPNew = UtilsDatabase.getDaTaBase().SanPhams.ToList();
            ViewBag.listSP = listSPNew;
            return View();
        }
        public ActionResult QuanLyLoaiSanPham()
        {
            return View();
        }
    }
}