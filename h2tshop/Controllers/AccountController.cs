﻿using h2tshop.Models;
using h2tshop.Models.entitiDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace h2tshop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username="",string pass="")
        {  
            if(username.Equals("admin") && pass.Equals("123"))
            {
               return RedirectToAction("Index","Admin");
            }
            var user = UtilsDatabase.getDaTaBase().Users.Where(t => t.TenDangNhap == username && t.MatKhau == pass).FirstOrDefault();
                   
            if (user != null)
            {
                HttpCookie myCookie = new HttpCookie("AccountCookie");
                DateTime now = DateTime.Now;

                var acc = new Users();
                acc.Id = user.Id;
                acc.HoTen = user.HoTen;
                acc.DiaChi = user.DiaChi;
                acc.SoDienThoai = user.SoDienThoai;
                acc.TenDangNhap = user.TenDangNhap;
                acc.MatKhau = user.MatKhau;
                acc.Quyen = user.Quyen;
                acc.IsActive = user.IsActive;
                var jsonUser = JsonConvert.SerializeObject(acc);
                // Set the cookie value.
                Response.Cookies["acc"].Value = jsonUser;
                Response.Cookies["acc"].Expires = DateTime.Now.AddDays(200);

                return RedirectToAction("Index", "Home");
            }
            return this.Login(); 
        }
        public ActionResult Register()
        {   

            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            var userRes = new User();
            userRes.HoTen = user.HoTen;
            userRes.DiaChi = user.DiaChi;
            userRes.SoDienThoai = user.SoDienThoai;
            userRes.TenDangNhap = user.TenDangNhap;
            userRes.MatKhau = user.MatKhau;
            userRes.Quyen = 2;
            userRes.IsActive = 1;
            UtilsDatabase.getDaTaBase().Users.InsertOnSubmit(userRes);
            UtilsDatabase.getDaTaBase().SubmitChanges();

            return RedirectToAction("Login","Account");
        }
        public ActionResult CreateOrEditAccount(int id = -1)
        {
            var acc = UtilsDatabase.getDaTaBase().Users.Where(l => l.Id == id).FirstOrDefault();
            ViewBag.acc = acc;
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrEditAccount(User user)
        {
            if (user.Id > 0)
            {
                // update 
                var acccansua = UtilsDatabase.getDaTaBase().Users.Where(p => p.Id == user.Id).First();
                acccansua.IsActive = 1;
                acccansua.HoTen = user.HoTen;
                acccansua.DiaChi = user.DiaChi;
                acccansua.SoDienThoai = user.SoDienThoai;
                acccansua.TenDangNhap = user.TenDangNhap;
                acccansua.MatKhau = user.MatKhau;
                UpdateModel(acccansua);
                UtilsDatabase.getDaTaBase().SubmitChanges();
            }
            else
            {
                // add
                var acccansua = new User();
                acccansua.IsActive = 1;
                acccansua.HoTen = user.HoTen;
                acccansua.DiaChi = user.DiaChi;
                acccansua.SoDienThoai = user.SoDienThoai;
                acccansua.TenDangNhap = user.TenDangNhap;
                acccansua.MatKhau = user.MatKhau;
                acccansua.Quyen = 2;
                acccansua.IsActive = 1;
                
                UtilsDatabase.getDaTaBase().Users.InsertOnSubmit(acccansua);
                UtilsDatabase.getDaTaBase().SubmitChanges();

            }
            return RedirectToAction("QuanLyTaikhoan", "Admin");
        }
        public ActionResult deleteAcc(int id = -1)
        {
            var acccansua = UtilsDatabase.getDaTaBase().Users.Where(p => p.Id == id).FirstOrDefault();
            if (acccansua != null)
            {
                acccansua.IsActive = 0;
                UtilsDatabase.getDaTaBase().SubmitChanges();
            }
            return RedirectToAction("QuanLyTaikhoan", "Admin");
        }
    }
}