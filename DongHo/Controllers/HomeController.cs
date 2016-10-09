using DongHo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DongHo.Models;
namespace DongHo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DONGHODataContext data = new DONGHODataContext();
        private List<DONGHO> laydonghomoi(int dem)
        {
            return data.DONGHOs.OrderByDescending(a => a.MaDongHo).Take(dem).ToList();
        }
        public ActionResult Index()
        {
            var dongho = laydonghomoi(8);
            return View(dongho);
        }
        private IList<DONGHO> donghotot(int dem)
        {
            return data.DONGHOs.Where(b => b.MaLoai == 2).OrderByDescending(a => a.MaDongHo).Take(dem).ToList();
        }
        public ActionResult donghotot()
        {
            var dongho = donghotot(8);
            return View(dongho);
        }
        public ActionResult donghonew()
        {
            var dongho = laydonghomoi(8);
            return View(dongho);
        }
        public ActionResult SanPhamNu()
        {
            var dongho = from dh in data.DONGHOs
                         where dh.MaLoai == 2
                         select dh;
            return View(dongho);
        }
        public ActionResult SanPhamNam()
        {
            var dongho = from dh in data.DONGHOs
                         where dh.MaLoai == 1
                         select dh;
            return View(dongho);
        }
        public ActionResult Details(int id)
        {
            var dongho = from dh in data.DONGHOs where dh.MaDongHo == id select dh;
            return View(dongho.Single());
        }
        public ActionResult QuanLy()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                TaiKhoan ad = data.TaiKhoans.SingleOrDefault(n => n.TaiKhoan1 == tendn && n.MatKhau == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("QuanLy","Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult KhachHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KhachHang(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.SDT == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("GioHang", "GioHang");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult Seach(string seach)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(seach) ? "name" : "";
            ViewBag.MSSortParm = String.IsNullOrEmpty(seach) ? "MS" : "";
            var dongho = from s in data.DONGHOs
                           select s;
            switch (seach)
            {
                case "name":
                    dongho = dongho.OrderByDescending(s => s.TenDongHo);
                    break;
                case "MS":
                    dongho = dongho.OrderBy(s => s.MaDongHo);
                    break;
               
                default:
                    dongho = dongho.OrderBy(s => s.MaLoai);
                    break;
            }
            return View(dongho.ToList());
        }
    }
   
}