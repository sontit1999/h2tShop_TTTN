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
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
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
            string mystring = Request.Cookies["acc"].Value.ToString();
            var user = JsonConvert.DeserializeObject<Users>(mystring);
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
                    return RedirectToAction("Index","Home");
                }


                
            }
            catch(Exception e)
            {

                return this.ThanhToan();
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
                else
                {
                    Order.IsAccept = 2;
                }
                UpdateModel(Order);
                UtilsDatabase.getDaTaBase().SubmitChanges();
                
                

            }
            return RedirectToAction("QuanLyDonHang", "Admin");
        }
    }
}