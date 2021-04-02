using h2tshop.Models;
using h2tshop.Models.entitiDB;
using h2tshop.Models.inputDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace h2tshop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.lsp = lsp;
            int tongtien = 0;
            List<CartItemDTO> listCart = new List<CartItemDTO>();
            var numberItem = 0;
            var cart = Session["cart"];//get key cart
            if (cart != null)
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(cart.ToString());
                numberItem = listCart.Count;
            }
            foreach (var item in listCart)
            {
                tongtien += (Convert.ToInt32(item.Gia) * Convert.ToInt32(item.SoLuong));
            }
            ViewBag.tongtien = string.Format("{0:#,##0}", tongtien);
            ViewBag.listCart = listCart;
            ViewBag.numberItem = numberItem;
            return View();
        }
        [HttpPost]
        public ActionResult ThanhToan()
        {
            var lsp = UtilsDatabase.getDaTaBase().LoaiSanPhams.ToList();
            ViewBag.lsp = lsp;
            var user = new Users();
            string mystring = "";
            if (Request.Cookies["acc"] != null)
            {
                mystring = Request.Cookies["acc"].Value.ToString();
            }
            user = JsonConvert.DeserializeObject<Users>(mystring);
            var cart = Session["cart"];
            List<CartItemDTO> listCart = new List<CartItemDTO>();
            if (cart != null)
            {
                 listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(cart.ToString());
            }
            
            ViewBag.listsp = listCart;
            return View(user);
        }
        [HttpPost]
        public ActionResult addCart(CartItemDTO cartItem)
        {         
            var carrt = cartItem;
            var cart = Session["cart"];//get key cart

            // cập nhật số lượng đã bán bảng LoaiSanPham
            var SP = UtilsDatabase.getDaTaBase().SanPhams.Where(o => o.MaSanPham == Convert.ToInt32(cartItem.MaSanPham)).FirstOrDefault();
            if (SP != null)
            {
                var LSP = UtilsDatabase.getDaTaBase().LoaiSanPhams.Where(o => o.MaLoai == Convert.ToInt32(SP.MaLoai)).FirstOrDefault();
                if (LSP != null)
                {
                    LSP.SoLuongDaBan = SP.SoLuongDaBan + cartItem.SoLuong;                
                    UpdateModel(SP);
                    UtilsDatabase.getDaTaBase().SubmitChanges();
                }
                UpdateModel(SP);
                UtilsDatabase.getDaTaBase().SubmitChanges();
            }

            if (cart == null)
            {
                List<CartItemDTO> listCart = new List<CartItemDTO>();
                listCart.Add(cartItem);
                string jsonCart = JsonConvert.SerializeObject(listCart);
                Session["cart"] = jsonCart;
            }
            else
            {
                List<CartItemDTO> listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(cart.ToString());
                bool check = true;
                for (int i = 0; i < listCart.Count; i++)
                {
                    if (listCart[i].MaSanPham == cartItem.MaSanPham)
                    {
                        listCart[i].SoLuong += cartItem.SoLuong;
                        check = false;
                    }
                }
                if (check)
                {
                    listCart.Add(cartItem);
                }
                string jsonCart = JsonConvert.SerializeObject(listCart);
                Session["cart"] = jsonCart;
                           
            }

            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public int updateCart(CartUpdateDTO cartUpdate)
        {
            int tongtien = 0;
            var cart = Session["cart"];//get key cart
            if (cart == null)
            {
                List<CartItemDTO> listCart = new List<CartItemDTO>();               
                string jsonCart = JsonConvert.SerializeObject(listCart);
                Session["cart"] = jsonCart;
            }
            else
            {
                List<CartItemDTO> listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(cart.ToString());
               
                for (int i = 0; i < listCart.Count; i++)
                {
                    if (listCart[i].MaSanPham == cartUpdate.MaSanPham)
                    {
                        listCart[i].SoLuong = cartUpdate.SoLuong;
                        break;
                    }
                }
                foreach(var item in listCart)
                {
                    tongtien += (Convert.ToInt32(item.Gia) * Convert.ToInt32(item.SoLuong));
                }
                string jsonCart = JsonConvert.SerializeObject(listCart);
                Session["cart"] = jsonCart;
               
            }
            return tongtien;
        }
        [HttpPost]
        public List<String> getstringSuggest(string keyword)
        {
            var listString = new List<String>();
            var dssp = UtilsDatabase.getDaTaBase().SanPhams.Where(o => o.TenSanPham.Contains(keyword.Trim())).ToList();
            foreach(var item in dssp)
            {
                listString.Add(item.TenSanPham);
            }
            return listString;
        }
        [HttpPost]
        public int deleteCart(CartUpdateDTO cartUpdate)
        {
           
            var cart = Session["cart"];//get key cart
            if (cart == null)
            {
                List<CartItemDTO> listCart = new List<CartItemDTO>();
                string jsonCart = JsonConvert.SerializeObject(listCart);
                Session["cart"] = jsonCart;
            }
            else
            {
                List<CartItemDTO> listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(cart.ToString());

                for (int i = 0; i < listCart.Count; i++)
                {
                    if (listCart[i].MaSanPham == cartUpdate.MaSanPham)
                    {
                        listCart.Remove(listCart[i]);
                        break;
                    }
                }
                
                string jsonCart = JsonConvert.SerializeObject(listCart);
                Session["cart"] = jsonCart;

            }
            return 1;
        }
        [HttpPost]
        public ActionResult DatHang(Users user)
        {
           
             
            try
            {
                // đặt hàng 
                var cart = Session["cart"];//get key cart
                if (cart == null)
                {
                    return this.ThanhToan();
                }
                else
                {  

                    int tongtien = 0;
                    int slsp = 0;
                    List<CartItemDTO> listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(cart.ToString());
                    string jsonDonHang = JsonConvert.SerializeObject(listCart);
                    foreach (var item in listCart)
                    {
                        tongtien += (Convert.ToInt32(item.Gia) * Convert.ToInt32(item.SoLuong));
                        slsp += item.SoLuong;
                    }
                    // insert to DB
                    user.Id = 1;
                    DonHang dh = new DonHang();
                    dh.NgayTao = DateTime.Now;
                    dh.TongTien = tongtien;
                    dh.SoLuongSanPham = slsp ;
                    dh.GhiChu = "Giao cho Anh/chị  " + user.HoTen + " ,Đ/c: " + user.DiaChi + ", Số điện thoại " + user.SoDienThoai  ;
                    dh.IsAccept = 0;
                    dh.IdUser = user.Id;
                    dh.JsonSanPham = jsonDonHang;
                                
                    UtilsDatabase.getDaTaBase().DonHangs.InsertOnSubmit(dh);
                    UtilsDatabase.getDaTaBase().SubmitChanges();
                   // updateSoLuongDaBanSanPham(listCart);
                    
                    int IDdonHang = dh.MaDonHang;
                    List<ChiTietDonHang> listCTDH = new List<ChiTietDonHang>();
                    foreach(var item in listCart)
                    {   

                        ChiTietDonHang ctdh = new ChiTietDonHang();
                        ctdh.MaSanPham = Convert.ToInt32(item.MaSanPham);
                        ctdh.SoLuong = item.SoLuong;
                        ctdh.MaDonHang = IDdonHang;
                        listCTDH.Add(ctdh);
                        
                    }
                    UtilsDatabase.getDaTaBase().ChiTietDonHangs.InsertAllOnSubmit(listCTDH);
                    UtilsDatabase.getDaTaBase().SubmitChanges();                    
                    // reset giỏ hàng
                    string jsonCart = JsonConvert.SerializeObject(new List<CartItemDTO>());
                    Session["cart"] = jsonCart;
                    return RedirectToAction("Thanks","Home");
                }


                
            }
            catch(Exception e)
            {

                return this.ThanhToan();
            }
            
        }

        private void updateSoLuongDaBanSanPham(List<CartItemDTO> listCart)
        {
            foreach (var item in listCart)
            {

                var SP = UtilsDatabase.getDaTaBase().SanPhams.Where(o => o.MaSanPham == Convert.ToInt32(item.MaSanPham)).FirstOrDefault();
                if (SP != null)
                {
                    SP.SoLuongDaBan = SP.SoLuongDaBan + item.SoLuong;
                    SP.SoLuong = SP.SoLuong - item.SoLuong;              
                    UpdateModel(SP);
                    UtilsDatabase.getDaTaBase().SubmitChanges();
                }

            }
        }

        public ActionResult AcceptOrder(int id, int isAccept)
        {
            var Order = UtilsDatabase.getDaTaBase().DonHangs.Where(o => o.MaDonHang == id).FirstOrDefault();
            if(Order != null)
            {
                if (isAccept == 1)
                {
                    Order.IsAccept = 1;
                    
                    Order.NgayHenTra = DateTime.Now.AddDays(4);
                    // insert hóa đơn 
                    HoaDon hd = new HoaDon();
                    hd.NgayLap = DateTime.Now;
                    hd.MaDonHang = id;
                    hd.IdUser = Order.IdUser;
                    hd.GhiChu = Order.GhiChu;
                    hd.TongTien = Order.TongTien;
                    UtilsDatabase.getDaTaBase().HoaDons.InsertOnSubmit(hd);
                    UtilsDatabase.getDaTaBase().SubmitChanges();
                }
                else if(isAccept==2)
                {
                    Order.IsAccept = 2;
                }else if(isAccept == 3)
                {   
                    var dh = UtilsDatabase.getDaTaBase().DonHangs.Where(o => o.MaDonHang == id).FirstOrDefault();
                    var ctdh = UtilsDatabase.getDaTaBase().ChiTietDonHangs.Where(o => o.MaDonHang == dh.MaDonHang).ToList();
                    foreach(var item in ctdh)
                    {
                        var SP = UtilsDatabase.getDaTaBase().SanPhams.Where(o => o.MaSanPham == item.MaSanPham).FirstOrDefault();
                        if (SP != null)
                        {
                            SP.SoLuongDaBan = SP.SoLuongDaBan - item.SoLuong;
                            SP.SoLuong = SP.SoLuong + item.SoLuong;
                            UpdateModel(SP);
                            UtilsDatabase.getDaTaBase().SubmitChanges();
                        }
                    }
                   
                }
                UpdateModel(Order);
                UtilsDatabase.getDaTaBase().SubmitChanges();
                
                

            }
            return RedirectToAction("QuanLyDonHang", "Admin");
        }      
    }
}