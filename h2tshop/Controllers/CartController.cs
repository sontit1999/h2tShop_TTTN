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
            List<CartItemDTO> listCart = new List<CartItemDTO>();
            var numberItem = 0;
            var cart = Session["cart"];//get key cart
            if (cart != null)
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemDTO>>(cart.ToString());
                numberItem = listCart.Count;
            }
            ViewBag.listCart = listCart;
            ViewBag.numberItem = numberItem;
            return View();
        }
        public ActionResult ThanhToan()
        {
            return View();
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
        public string updateCart(CartUpdateDTO cartUpdate)
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
                        listCart[i].SoLuong = cartUpdate.SoLuong;
                        break;
                    }
                }
               
                string jsonCart = JsonConvert.SerializeObject(listCart);
                Session["cart"] = jsonCart;

            }
            return "true";
        }
    }
}