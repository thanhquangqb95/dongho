using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DongHo.Models;
namespace DongHo.Controllers
{
    public class KhachHangController : Controller
    {
        DONGHODataContext data = new DONGHODataContext();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy( FormCollection colection,KHACHHANG kh)
        {
            var ten = colection["name"];
            var SDT = colection["phonenumble"];
            var MatKhau = colection["password"];
            var DiaChi = colection["DiaChi"];
            var NLMatKhau = colection["password2"];
            if (String.IsNullOrEmpty(ten))
            {
                ViewData["Loi1"] = "Phải nhập tên";
            }
            else if (String.IsNullOrEmpty(SDT))
            {
                ViewData["Loi2"] = "Phải nhập số điện thoại";
            }
            else
            {
                if(String.Compare(MatKhau,NLMatKhau)!=0)
                {
                    ViewData["Loi3"] = "Mật Khâu Nhập Không Giống Nhau";
                }
                else
                {
                    kh.TenKH = ten;
                    kh.SDT = SDT;
                    kh.MatKhau = MatKhau;
                    kh.DiaChi = DiaChi;
                    data.KHACHHANGs.InsertOnSubmit(kh);
                    data.SubmitChanges();
                }
            }
            return RedirectToAction("KhachHang", "Home");
        }
    }
}