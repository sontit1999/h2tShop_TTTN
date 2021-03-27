using h2tshop.Models;
using h2tshop.Models.entitiDB;
using h2tshop.Models.inputDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(DateTime? from, DateTime? to)
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
            // lấy ra mã sp số lượng bán chạy nhất
            var slspMax = UtilsDatabase.getDaTaBase().ChiTietDonHangs.Max(p => p.SoLuong);
            var spMax = UtilsDatabase.getDaTaBase().ChiTietDonHangs.Where(p => p.SoLuong == slspMax).FirstOrDefault();
            
            // get list SP bán từ ngày = > ngày 
            var listsp = from p in UtilsDatabase.getDaTaBase().SanPhams
                         join ctdh in UtilsDatabase.getDaTaBase().ChiTietDonHangs
                         on p.MaSanPham equals ctdh.MaSanPham
                         join dh in UtilsDatabase.getDaTaBase().DonHangs
                         on ctdh.MaDonHang equals dh.MaDonHang
                         where p.MaSanPham == spMax.MaSanPham
                         select new CartItemDTO
                         {
                             MaSanPham = p.MaSanPham.ToString(),
                             TenSanPham = p.TenSanPham,
                             Gia = p.Gia.ToString(),
                             MoTa = p.MoTa,
                             GhiChu = "",
                             SoLuong = ctdh.SoLuong,
                             NgayTao = (DateTime)dh.NgayTao,
                             LinkAnh = p.LinkAnh
                         };
            var TongDoanhThu = 0;
            TongDoanhThu = UtilsDatabase.getDaTaBase().DonHangs.Sum(p => p.TongTien);
            if (from.HasValue && to.HasValue) {
                listsp.Where(p => p.NgayTao >= from && p.NgayTao <= to);
                TongDoanhThu = UtilsDatabase.getDaTaBase().DonHangs.Where(dh=>dh.NgayTao >= from && dh.NgayTao <= to).Sum(p => p.TongTien);
            }
            ViewBag.TongDoanhThu = TongDoanhThu;
            ViewBag.listsp = listsp.ToList();
            ViewBag.dt = dt;


            return View();
        }
        public ActionResult DashBoard(DateTime? from, DateTime? to,int? type)
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
            // lấy ra mã sp số lượng bán chạy nhất
            var slspMax = UtilsDatabase.getDaTaBase().ChiTietDonHangs.Max(p => p.SoLuong);
            var spMax = UtilsDatabase.getDaTaBase().ChiTietDonHangs.Where(p => p.SoLuong == slspMax).FirstOrDefault();
            var totalDonHang = UtilsDatabase.getDaTaBase().DonHangs.Count();
            var totalDoanhThu = UtilsDatabase.getDaTaBase().HoaDons.Sum(p => p.TongTien);
            DateTime now = DateTime.Now;
            if (type.HasValue)
            {
                switch (type)
                {
                    case 0:
                        totalDonHang = UtilsDatabase.getDaTaBase().DonHangs.Where(dh=>dh.NgayTao.Value ==now).Count();
                        totalDoanhThu = UtilsDatabase.getDaTaBase().HoaDons.Where(hd => hd.NgayLap.Value == now).Sum(p => p.TongTien);
                        break;
                    case 1:
                        now = now.AddDays(-7);
                        totalDonHang = UtilsDatabase.getDaTaBase().DonHangs.Where(dh => dh.NgayTao.Value >= now && dh.NgayTao.Value <= DateTime.Now).Count();
                        totalDoanhThu = UtilsDatabase.getDaTaBase().HoaDons.Where(hd => hd.NgayLap.Value >= now && hd.NgayLap.Value <= DateTime.Now).Sum(p => p.TongTien);
                        break;
                    case 2:
                        now = now.AddDays(-30);
                        totalDonHang = UtilsDatabase.getDaTaBase().DonHangs.Where(dh => dh.NgayTao.Value >= now && dh.NgayTao.Value <= DateTime.Now).Count();
                        totalDoanhThu = UtilsDatabase.getDaTaBase().HoaDons.Where(hd => hd.NgayLap.Value >= now && hd.NgayLap.Value <= DateTime.Now).Sum(p => p.TongTien);
                        break;
                    case 3:                    
                        totalDonHang = UtilsDatabase.getDaTaBase().DonHangs.Count();
                        totalDoanhThu = UtilsDatabase.getDaTaBase().HoaDons.Sum(p => p.TongTien);
                        break;
                }
            }
            else
            {
                totalDonHang = UtilsDatabase.getDaTaBase().DonHangs.Where(dh => dh.NgayTao.Value == now).Count();
                totalDoanhThu = UtilsDatabase.getDaTaBase().HoaDons.Where(hd => hd.NgayLap.Value == now).Sum(p => p.TongTien);
            }
            
            var totalTruyCap = 500;
            var totalSanPham = UtilsDatabase.getDaTaBase().SanPhams.Count();
            // get list SP bán từ ngày = > ngày 
            var listsp = from p in UtilsDatabase.getDaTaBase().SanPhams
                         join ctdh in UtilsDatabase.getDaTaBase().ChiTietDonHangs
                         on p.MaSanPham equals ctdh.MaSanPham
                         join dh in UtilsDatabase.getDaTaBase().DonHangs
                         on ctdh.MaDonHang equals dh.MaDonHang
                         where p.MaSanPham == spMax.MaSanPham
                         select new CartItemDTO
                         {
                             MaSanPham = p.MaSanPham.ToString(),
                             TenSanPham = p.TenSanPham,
                             Gia = p.Gia.ToString(),
                             MoTa = p.MoTa,
                             GhiChu = "",
                             SoLuong = ctdh.SoLuong,
                             NgayTao = (DateTime)dh.NgayTao,
                             LinkAnh = p.LinkAnh
                         };
            var listSPbanchay = UtilsDatabase.getDaTaBase().SanPhams.OrderByDescending(p => p.SoLuongDaBan).Take(3);
            ViewBag.listSPbanChay = listSPbanchay;
            // get list lsp 
            var listLSP = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.listLSP = listLSP;
            var TongDoanhThu = 0;
            TongDoanhThu = UtilsDatabase.getDaTaBase().DonHangs.Sum(p => p.TongTien);
            if (from.HasValue)
            {
                listsp.Where(p => p.NgayTao.Date == from);
                var dhs = UtilsDatabase.getDaTaBase().DonHangs.Where(dh => dh.NgayTao.Value == from).ToList();
                if (dhs.Count > 0)
                {
                    totalDoanhThu = UtilsDatabase.getDaTaBase().DonHangs.Where(dh => dh.NgayTao.Value == from).Sum(p => p.TongTien);
                }
                else
                {
                    totalDoanhThu = 0 ; 
                }        
                totalDonHang =  UtilsDatabase.getDaTaBase().DonHangs.Where(dh => dh.NgayTao.Value == from).Count();
            }
            // get ds hóa đơn
            var dsdh = from dh in UtilsDatabase.getDaTaBase().DonHangs
                       join ctdh in UtilsDatabase.getDaTaBase().ChiTietDonHangs
                        on dh.MaDonHang equals ctdh.MaDonHang
                        join u in UtilsDatabase.getDaTaBase().Users
                        on dh.IdUser equals u.Id      
                        join hd in UtilsDatabase.getDaTaBase().HoaDons on
                        dh.MaDonHang equals hd.MaDonHang
                       orderby dh.MaDonHang descending
                       select new DonHangDTO
                       {
                           MaDonHang = hd.MaDonHang,
                           TenKhachHang = u.HoTen,
                           TTDonHang = GetInforDonHang(dh.JsonSanPham),
                           TongTien = dh.TongTien,
                           NgayTao = (DateTime)hd.NgayLap,
                           GhiChu = dh.GhiChu,
                           IsAccept = dh.IsAccept,
                           NgayHenTra = dh.NgayHenTra,                         
                       };
           
            var dsDonHang = dsdh.ToList();
            if (from.HasValue)
            {
                dsDonHang.Where(dh => EntityFunctions.TruncateTime(dh.NgayTao) == from);
            }
            ViewBag.dsDonHang = dsDonHang.Take(10);
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            ViewBag.TongDoanhThu = String.Format(info, "{0:c}", totalDoanhThu);
            LoaiSanPhamTK lspTK = new LoaiSanPhamTK();

            lspTK.SoMi = listLSP[0].SoLuongDaBan;
            lspTK.Thun = listLSP[1].SoLuongDaBan;
            lspTK.PoLo = listLSP[2].SoLuongDaBan;
            lspTK.QuanJean = listLSP[3].SoLuongDaBan;
            lspTK.QuanJogger = listLSP[4].SoLuongDaBan;
            lspTK.QuanTay = listLSP[5].SoLuongDaBan;
            lspTK.QuanSooc = listLSP[6].SoLuongDaBan;
            lspTK.AoNi = listLSP[7].SoLuongDaBan;
            lspTK.AoKhoac = listLSP[8].SoLuongDaBan;
            lspTK.AoGio = listLSP[9].SoLuongDaBan;
            ViewBag.listsp = listsp.ToList();
            ViewBag.dt = dt;
            ViewBag.lspTK = lspTK;
            ViewBag.totalDonHang = totalDonHang;
            ViewBag.totalDoanhThu = totalDoanhThu;
            ViewBag.totalTruyCap = totalTruyCap;
            ViewBag.totalSanPham = totalSanPham;
            ViewBag.type = type;
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
                           IsAccept = dh.IsAccept,
                           NgayHenTra = dh.NgayHenTra
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
        public ActionResult QuanLySanPham(string keyword = "")
        {
            var listSPNew = new List<SanPham>();
            if (String.IsNullOrEmpty(keyword)) {
                listSPNew = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.IsActive == 1).OrderByDescending(p => p.MaSanPham).ToList();
            }
            else
            {
                listSPNew = UtilsDatabase.getDaTaBase().SanPhams.Where(p => p.IsActive == 1 && p.TenSanPham.Trim().Contains(keyword)).OrderByDescending(p => p.MaSanPham).ToList();
            }
            
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
                       where u.HoTen.Contains(keyword.Trim()) || dh.MaDonHang == madh || dh.GhiChu.Contains(keyword.Trim())
                       orderby dh.MaDonHang descending
                       select new DonHangDTO
                       {
                           MaDonHang = dh.MaDonHang,
                           TenKhachHang = u.HoTen,
                           TTDonHang = GetInforDonHang(dh.JsonSanPham),
                           TongTien = dh.TongTien,
                           NgayTao = (DateTime)dh.NgayTao,
                           GhiChu = dh.GhiChu,
                           IsAccept = dh.IsAccept,
                           NgayHenTra = dh.NgayHenTra
                       };
            var dsDonHang = dsdh.ToList();           
            ViewBag.dsDonHang = dsDonHang;         
            return View();
        }
    }
}