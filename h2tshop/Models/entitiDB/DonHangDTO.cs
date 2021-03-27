using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h2tshop.Models.entitiDB
{
    public class DonHangDTO
    {
        public int MaDonHang { get; set; }
        public string TenKhachHang { get; set; }
        public string TTDonHang { get; set; }
        public int TongTien { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
        public int IsAccept { get; set; }
        public DateTime? NgayHenTra { get; set; }
    }
}
