using DongHo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using DongHo.Models;
using PagedList;
using PagedList.Mvc;
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
        public ActionResult QuangCao()
        {
            return View();
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
        public ActionResult donghotot2()
        {
            var dongho = donghotot(16);
            return View(dongho);
        }
        public ActionResult donghonew()
        {
            var dongho = donghotot(8);
            return View(dongho);
        }
        public ActionResult SanPhamNu(int? page)
        {
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            var dongho = from dh in data.DONGHOs
                         where dh.MaLoai == 2
                         select dh;
            return View(dongho.ToList().OrderByDescending(m => m.MaDongHo).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamNam(int? page)
        {
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            var dongho = from dh in data.DONGHOs
                         where dh.MaLoai == 1
                         select dh;
            return View(dongho.ToList().OrderByDescending(m => m.MaDongHo).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int id)
        {
            CTSP ct = new CTSP(id);
            return View(ct);
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
                ViewBag.Thongbao1 = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewBag.Thongbao2 = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                TaiKhoan ad = data.TaiKhoans.SingleOrDefault(n => n.TaiKhoan1 == tendn && n.MatKhau == matkhau);
                if (ad != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
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
            if (Session["MaKH"] == null)
            {
                var tendn = collection["username"];
                var matkhau = collection["password"];
                if (String.IsNullOrEmpty(tendn))
                {
                    ViewBag.Thongbao = "Bạn chưa nhập Số Điện Thoại học Mật Khẩu";
                }
                else if (String.IsNullOrEmpty(matkhau))
                {
                    ViewBag.Thongbao = "Bạn chưa nhập Số Điện Thoại học Mật Khẩu";
                }
                else
                {

                    KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.SDT == tendn && n.MatKhau == matkhau);
                    if (kh != null)
                    {
                        // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                        Session["Taikhoan"] = kh;
                        Session["MaKH"] = kh.TenKH;
                        ViewBag.hoten = kh.TenKH;
                        return RedirectToAction("GioHang", "GioHang");
                    }
                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Index");
            }
        }
        public string laytenkh()
        {
            string ten;
            if (Session["MaKH"] != null)
            {

                ten = Session["MaKH"].ToString();
            }
            else
            {
                ten = "MY ACCOUNT";
            }
            return ten;
        }
        public ActionResult TenKH()
        {
            ViewBag.TenKH1 = laytenkh();
                return PartialView();

        }
        [HttpPost]
        public ActionResult TimKiem(FormCollection collection, int? page)
        {
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            var tendn = collection["TimKiem"];
            ViewBag.tendn = tendn;
            List<DONGHO> dh = data.DONGHOs.Where(m => m.TenDongHo.Contains(tendn)).ToList();
            if(dh.Count ==0)
            {
                ViewBag.ThongBao1 = "Không Tim Thấy Tên Sản Phẩm Vừa Nhập";
                return View();
            }
            return View(dh.OrderBy(n=>n.TenDongHo).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult TimKiem( int? page ,string tendn1)
        {
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            ViewBag.tendn = tendn1;
            List<DONGHO> dh = data.DONGHOs.Where(m => m.TenDongHo.Contains(tendn1)).ToList();
            return View(dh.OrderBy(n => n.TenDongHo).ToPagedList(pageNumber, pageSize));
        }
    }


}