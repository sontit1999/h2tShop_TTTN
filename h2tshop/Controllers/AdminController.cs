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
            var currenYear = DateTime.Now.Year;
            var listInYear = UtilsDatabase.getDaTaBase().HoaDons.Where(r => r.NgayLap.Value.Year == currenYear).ToList();
            DoanhThuDTO dt = new DoanhThuDTO();
            dt.Thang1 = listInYear.Where(x => x.NgayLap.Value.Month == 1).Sum(x => x.TongTien);
            dt.Thang2 = listInYear.Where(x => x.NgayLap.Value.Month == 2).Sum(x => x.TongTien);
            dt.Thang3 = listInYear.Where(x => x.NgayLap.Value.Month == 3).Sum(x => x.TongTien);
            dt.Thang4 = listInYear.Where(x => x.NgayLap.Value.Month == 4).Sum(x => x.TongTien);
            dt.Thang5 = listInYear.Where(x => x.NgayLap.Value.Month == 5).Sum(x => x.TongTien);
            dt.Thang6 = listInYear.Where(x => x.NgayLap.Value.Month == 6).Sum(x => x.TongTien);
            dt.Thang7 = listInYear.Where(x => x.NgayLap.Value.Month == 7).Sum(x => x.TongTien);
            dt.Thang8 = listInYear.Where(x => x.NgayLap.Value.Month == 8).Sum(x => x.TongTien);
            dt.Thang9 = listInYear.Where(x => x.NgayLap.Value.Month == 9).Sum(x => x.TongTien);
            dt.Thang10 = listInYear.Where(x => x.NgayLap.Value.Month == 10).Sum(x => x.TongTien);
            dt.Thang11 = listInYear.Where(x => x.NgayLap.Value.Month == 11).Sum(x => x.TongTien);
            dt.Thang12 = listInYear.Where(x => x.NgayLap.Value.Month == 12).Sum(x => x.TongTien);
            ViewBag.dt = dt;
            return View();
        }
        public ActionResult QuanLyDonHang(int type=0)
        {
                    
            var dsdh = from dh in UtilsDatabase.getDaTaBase().DonHangs join ctdh in UtilsDatabase.getDaTaBase().ChiTietDonHangs
                       on dh.MaDonHang equals ctdh.MaDonHang join u in UtilsDatabase.getDaTaBase().Users
                       on dh.IdUser equals u.Id
                       where dh.IsAccept == type
                       orderby dh.MaDonHang descending
                       select new DonHangDTO {
                           MaDonHang = dh.MaDonHang,
                           TenKhachHang = u.HoTen,
                           TTDonHang = GetInforDonHang(dh.JsonSanPham),
                           TongTien = dh.TongTien,
                           NgayTao = (DateTime) dh.NgayTao,
                           GhiChu = dh.GhiChu  ,
                           IsAccept = dh.IsAccept
                       };
            var dsDonHang = dsdh.ToList();
            ViewBag.dsDonHang = dsDonHang;
            ViewBag.type = type;
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
            var listACC = UtilsDatabase.getDaTaBase().Users.Where(l => l.IsActive == 1).OrderByDescending(p => p.Id).ToList();
            ViewBag.listACC = listACC;
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
        
        public ActionResult TraCuuDonHang(string keyword = "")
        {
            int madh = 9999;
            try {
                madh = int.Parse(keyword);
            }
            catch {
                madh = 9999;
            }
            var dsdh = from dh in UtilsDatabase.getDaTaBase().DonHangs
                       join ctdh in UtilsDatabase.getDaTaBase().ChiTietDonHangs
                        on dh.MaDonHang equals ctdh.MaDonHang
                       join u in UtilsDatabase.getDaTaBase().Users
                        on dh.IdUser equals u.Id
                       where u.HoTen.Contains(keyword.Trim()) || dh.MaDonHang == madh
                       orderby dh.MaDonHang descending
                       select new DonHangDTO
                       {
                           MaDonHang = dh.MaDonHang,
                           TenKhachHang = u.HoTen,
                           TTDonHang = GetInforDonHang(dh.JsonSanPham),
                           TongTien = dh.TongTien,
                           NgayTao = (DateTime)dh.NgayTao,
                           GhiChu = dh.GhiChu,
                           IsAccept = dh.IsAccept
                       };
            var dsDonHang = dsdh.ToList();           
            ViewBag.dsDonHang = dsDonHang;         
            return View();
        }
    }
}