using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h2tshop.Models.inputDTO
{
    public class CartItemDTO
    {
        public String MaSanPham { get; set; }
        public String TenSanPham { get; set; }
        public String Gia { get; set; }
        public String MoTa { get; set; }
        public String LinkAnh { get; set; }
        public String Size { get; set; }
        public int SoLuong { get; set; }
        public String GhiChu { get; set; }
    }
}