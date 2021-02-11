using h2tshop.Models;
using h2tshop.Models.entitiDB;
using h2tshop.Models.inputDTO;
using Newtonsoft.Json;
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
            var dsdh = from dh in UtilsDatabase.getDaTaBase().DonHangs join ctdh in UtilsDatabase.getDaTaBase().ChiTietDonHangs
                       on dh.MaDonHang equals ctdh.MaDonHang join u in UtilsDatabase.getDaTaBase().Users
                       on dh.IdUser equals u.Id
                       where dh.IsAccept == 0
                       orderby dh.MaDonHang descending
                       select new DonHangDTO {
                           MaDonHang = dh.MaDonHang,
                           TenKhachHang = u.HoTen,
                           TTDonHang = GetInforDonHang(dh.JsonSanPham),
                           TongTien = dh.TongTien,
                           NgayTao = (DateTime) dh.NgayTao,
                           GhiChu = dh.GhiChu                        
                       };
            var dsDonHang = dsdh.ToList();
            ViewBag.dsDonHang = dsDonHang;
            return View();
        }
        public string GetInforDonHang(string stringCartItem)
        {
            List<CartItemDTO> listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(stringCartItem);
            string ttdh = "";
            foreach(var item in listCart)
            {
                ttdh += item.TenSanPham + "(" + item.SoLuong + ")," ;
            }
            return ttdh;
        }
        public ActionResult QuanLyTaiKhoan()
        {
            return View();
        }
        public ActionResult QuanLySanPham()
        {
            var listSPNew = UtilsDatabase.getDaTaBase().SanPhams.Where(p=>p.IsActive==1).OrderByDescending(p=>p.MaSanPham).ToList();
            ViewBag.listSP = listSPNew;
            return View();
        }
        public ActionResult QuanLyLoaiSanPham()
        {
            var listLoaiSP= UtilsDatabase.getDaTaBase().LoaiSanPhams.Where(l=>l.IsActive==1).OrderByDescending(p => p.MaLoai).ToList();
            ViewBag.listLoaiSP = listLoaiSP;
            return View();
        }
    }
}