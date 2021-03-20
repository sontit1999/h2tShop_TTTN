using h2tshop.Models;
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
        public ActionResult Index(int id=0,int page = 0,int? filter = 0)
        {
            int pageSize = 4;
            int numberpage = 0;
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            var lspMain = UtilsDatabase.getDaTaBase().LoaiSanPhams.Where(l=>l.MaLoai == id).FirstOrDefault();
            var listsp =  UtilsDatabase.getDaTaBase().SanPhams.ToList();
            
            if (id > 0)
            {
                listsp = UtilsDatabase.getDaTaBase().SanPhams.Where(p=>p.MaLoai==id).ToList();
               
            }
            ViewBag.lsp = lsp;
            ViewBag.maloai = id;
            ViewBag.lspMain = lspMain;
            ViewBag.page = page;
            if (listsp.Count / pageSize - 1 > 0)
            {
                numberpage = listsp.Count / pageSize;
            }
            ViewBag.count = numberpage;
            if (listsp.Count >= pageSize)
            {
                try
                {
                    listsp = listsp.GetRange((page * pageSize), pageSize);
                }
                catch
                {
                    listsp = listsp.GetRange((page * pageSize),listsp.Count-(page * pageSize));
                }
                
            }
            if (filter.HasValue)
            {
                switch (filter)
                {
                    case 1:
                        listsp = listsp.OrderBy(p => p.Gia).ToList();                           
                        break;
                    case 2:
                        listsp = listsp.OrderByDescending(p => p.Gia).ToList();
                        break;
                    case 3:
                        listsp = listsp.OrderBy(p => p.TenSanPham).ToList();
                        break;
                    case 4:
                        listsp = listsp.OrderByDescending(p => p.TenSanPham).ToList();
                        break;
                }
            }
            ViewBag.listsp = listsp;
           
            
            return View();
        }
        [HttpPost]
        public JsonResult getAllCagetory()
        {
           
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();

            return Json(lsp, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string getCagetory(int id)
        {
            LoaiSanPham lspNew = new LoaiSanPham();
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.Where(p => p.MaLoai == id).FirstOrDefault();
            string jsonLSPString = "";
            if (lsp != null)
            {
               jsonLSPString = Json(lsp, JsonRequestBehavior.AllowGet).ToString();
            }
            jsonLSPString= Json(lspNew, JsonRequestBehavior.AllowGet).ToString();
            return jsonLSPString;
        }
        public ActionResult CreateOrEditCagetory(int id=-1)
        {
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.Where(l => l.MaLoai == id).FirstOrDefault();
            ViewBag.lsp = lsp;
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrEditCagetory(LoaiSanPham lsp, HttpPostedFileBase file)
        {
            if (lsp.MaLoai > 0)
            {
                // update 
                var lspcansua = UtilsDatabase.getDaTaBase().LoaiSanPhams.Where(p => p.MaLoai== lsp.MaLoai).First();
                if (file != null)
                {
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    var path = Server.MapPath("~/Images/" + fileName);
                    file.SaveAs(path);
                    lspcansua.LinkAnh = "/Images/" + fileName;
                }
                lspcansua.IsActive = 1;
                lspcansua.MoTa = lsp.MoTa;
                lspcansua.TenLoai = lsp.TenLoai;
                UpdateModel(lspcansua);
                UtilsDatabase.getDaTaBase().SubmitChanges();
            }
            else
            {
                // add
                var lspAdd = new LoaiSanPham();
                if (file != null)
                {
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    var path = Server.MapPath("~/Images/" + fileName);
                    file.SaveAs(path);
                    lspAdd.LinkAnh = "/Images/" + fileName;
                }
                lspAdd.IsActive = 1;
                lspAdd.MoTa = lsp.MoTa;
                lspAdd.TenLoai = lsp.TenLoai;
                UtilsDatabase.getDaTaBase().LoaiSanPhams.InsertOnSubmit(lspAdd);
                UtilsDatabase.getDaTaBase().SubmitChanges();

            }
            return RedirectToAction("QuanLyLoaiSanPham", "Admin");
        }
        public ActionResult deleteCagetory(int id = -1)
        {
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.Where(p => p.MaLoai == id).FirstOrDefault();
            if (lsp != null)
            {
                lsp.IsActive = 0;
                UtilsDatabase.getDaTaBase().SubmitChanges();
            }
            return RedirectToAction("QuanLyLoaiSanPham", "Admin");
        }
    }
}