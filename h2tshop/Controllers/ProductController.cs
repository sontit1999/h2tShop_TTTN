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
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.lsp = lsp;
            return View();
        }
        public ActionResult Detail(int id = 0)
        {
            var sp = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.MaSanPham == id).FirstOrDefault();
            var spLienQuan = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.LoaiSanPham == sp.LoaiSanPham && p.MaSanPham!= sp.MaSanPham).ToList();
            ViewBag.sp = sp;
            ViewBag.spLienQuan = spLienQuan;
            return View();
        }
        // administration
        public ActionResult  createOrEditProduct(int id = -1)
        {
            var listlsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            var sp = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.MaSanPham == id).FirstOrDefault();
            ViewBag.lsp = listlsp;
            ViewBag.sp = sp;
            return View();
        }
        [HttpPost]
        public ActionResult createOrEditProduct(SanPham sp, HttpPostedFileBase file)
        {
            try
            {
                if (sp.MaSanPham > 0)
                {
                    // update 
                    var spcansua = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.MaSanPham == sp.MaSanPham).First();
                    if (file != null)
                    {
                        var fileName = System.IO.Path.GetFileName(file.FileName);
                        var path = Server.MapPath("~/Images/" + fileName);
                        file.SaveAs(path);
                        spcansua.LinkAnh = "/Images/" + fileName;
                    }
                    spcansua.IsActive = 1;
                    spcansua.TenSanPham = sp.TenSanPham;
                    spcansua.Gia = sp.Gia;
                    spcansua.SoLuong = sp.SoLuong;
                    spcansua.MoTa = sp.MoTa;
                    spcansua.KhuyenMai = sp.KhuyenMai;
                    UpdateModel(spcansua);
                    UtilsDatabase.getDaTaBase().SubmitChanges();
                }
                else
                {
                    // add
                    var spAdd = new SanPham();
                    if (file != null)
                    {
                        var fileName = System.IO.Path.GetFileName(file.FileName);
                        var path = Server.MapPath("~/Images/" + fileName);
                        file.SaveAs(path);
                        spAdd.LinkAnh = "/Images/" + fileName;
                    }
                    spAdd.IsActive = 1;
                    spAdd.TenSanPham = sp.TenSanPham;
                    spAdd.Gia = sp.Gia;
                    spAdd.SoLuong = sp.SoLuong;
                    spAdd.MoTa = sp.MoTa;
                    spAdd.KhuyenMai = sp.KhuyenMai;
                    spAdd.MaLoai = sp.MaLoai;
                    UtilsDatabase.getDaTaBase().SanPhams.InsertOnSubmit(spAdd);
                    UtilsDatabase.getDaTaBase().SubmitChanges();

                }
            }
            catch(Exception e)
            {
                return RedirectToAction("QuanLySanPham", "Admin");
            }
            
            
            return RedirectToAction("QuanLySanPham","Admin");
        }
        public ActionResult deleteProduct(int id = -1)
        {
           
            var sp = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.MaSanPham == id).FirstOrDefault();
            if (sp != null)
            {
                UtilsDatabase.getDaTaBase().SanPhams.DeleteOnSubmit(sp);
                UtilsDatabase.getDaTaBase().SubmitChanges();
            }
            return RedirectToAction("QuanLySanPham", "Admin");
        }
        
    }
}