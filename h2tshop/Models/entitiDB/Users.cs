using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h2tshop.Models.entitiDB
{   
    
    public class Users
    {
        public int Id { get; set; }
        public String HoTen { get; set; }
        public String DiaChi { get; set; }
        public String SoDienThoai { get; set; }
        public String Email { get; set; }
        public String TenDangNhap { get; set; }
        public String MatKhau { get; set; }
        public int Quyen { get; set; }
        public int IsActive { get; set; }
    }
}