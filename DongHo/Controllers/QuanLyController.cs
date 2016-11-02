using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DongHo.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace DongHo.Controllers
{
    public class QuanLyController : Controller
    {
        // GET: QuanLy
        DONGHODataContext data = new DONGHODataContext();
        List<DONGHO> dDongHo = new List<DONGHO>();
        int madathang;
        KhachHang khachhang;
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Admin","Home");
            }
        }
        public ActionResult QLSANPHAM(int? page)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(data.DONGHOs.ToList().OrderByDescending(m => m.MaDongHo).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Admin", "Home");
            }
        }
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] != null)
            {
                ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNSX), "MaNCC", "TenNSX");
                ViewBag.MaLoai = new SelectList(data.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                return View();
            }
            else
            {
                return RedirectToAction("Admin", "Home");
            }
           
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DONGHO dongho, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNSX), "MaNCC", "TenNSX");
            ViewBag.MaLoai = new SelectList(data.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/DONGHO"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    dongho.hinh = fileName;
                    //Luu vao CSDL
                    data.DONGHOs.InsertOnSubmit(dongho);
                    data.SubmitChanges();
                }
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                DONGHO dongho = data.DONGHOs.SingleOrDefault(n => n.MaDongHo == id);
                ViewBag.MaDongHo = dongho.MaDongHo;
                if (dongho == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                //Dua du lieu vao dropdownList
                //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
                ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNSX), "MaNCC", "TenNSX");
                ViewBag.MaLoai = new SelectList(data.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                return View(dongho);
            }
            else
            {
                return RedirectToAction("Admin", "Home");
            }
            //Lay ra doi tuong sach theo ma
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(int id ,HttpPostedFileBase fileUpload , FormCollection collection)
        {
            
            //Dua du lieu vao dropdownload
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNSX), "MaNCC", "TenNSX");
            ViewBag.MaLoai = new SelectList(data.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            //Kiem tra duong dan file
            var dongho = data.DONGHOs.First(m => m.MaDongHo == id);
            if (dongho == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View(dongho);
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        //Luu duong dan cua file
                        var path = Path.Combine(Server.MapPath("~/DONGHO"), fileName);
                        //Kiem tra hình anh ton tai chua?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            //Luu hinh anh vao duong dan
                            fileUpload.SaveAs(path);
                        }

                            dongho.hinh = fileName;
                            dongho.TenDongHo = collection["TenDongHo"];
                            dongho.MaLoai = int.Parse(collection["MaLoai"]);
                            dongho.MaNCC = int.Parse(collection["MaNCC"]);
                            dongho.SoLuong = int.Parse(collection["SoLuong"]);
                            dongho.DonGia = int.Parse(collection["DonGia"]);
                    }
                    catch
                    {
                        dongho.hinh = dongho.hinh;
                        dongho.TenDongHo = collection["TenDongHo"];
                        dongho.MaLoai = int.Parse(collection["MaLoai"]);
                        dongho.MaNCC = int.Parse(collection["MaNCC"]);
                        dongho.SoLuong = int.Parse(collection["SoLuong"]);
                        dongho.DonGia = int.Parse(collection["DonGia"]);
                    }
                    //Luu vao CSDL   
                    UpdateModel(dongho);
                    data.SubmitChanges();

                }
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult XOA(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            DONGHO dongho = data.DONGHOs.SingleOrDefault(n => n.MaDongHo == id);
            ViewBag.MaDongHo = dongho.MaDongHo;
            if (dongho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dongho);
        }
        [HttpPost, ActionName("XOA")]
        public ActionResult Xacnhanxoa(int id)
        {
            try
            {
                var chitietddh = from ddh in data.CTDDHs where ddh.MaDongHo == id select ddh;
                foreach(CTDDH ct in chitietddh)
                {
                    data.CTDDHs.DeleteOnSubmit(ct);
                }
                //Lay ra doi tuong sach can xoa theo ma
                DONGHO dongho = data.DONGHOs.SingleOrDefault(n => n.MaDongHo == id);
                ViewBag.MaDongHo = dongho.MaDongHo;
                if (dongho == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                data.DONGHOs.DeleteOnSubmit(dongho);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult DonDatHang()
        {
      

            if (Session["Taikhoanadmin"] != null)
            {
                var groupJoinQuery =
                from ddh in data.DONDATHANGs
                where ddh.ThanhToan == false
                 select ddh;
                Session["SODDH"] = groupJoinQuery.Count();
                return View(groupJoinQuery);
            }
            else
            {
                return RedirectToAction("Admin", "Home");
            }
        }
       
        public ActionResult DetailsDDH(int id)
        {
            khachhang = new KhachHang(id);
            madathang = khachhang.MaDDH1;
            Session["MaDDH"] = id;
            return View(khachhang);
        }
        public ActionResult DetailsDDongho()
        {
            foreach (var item in khachhang.MaDdong)
            {
                dDongHo.Add(data.DONGHOs.Single(m => m.MaDongHo == item));
            }
            return View(dDongHo);
        }
        public ActionResult updateĐH(int id)
        {

            if (Session["MaDDH"] != null)
            {
                id = int.Parse(Session["MaDDH"].ToString());
                var ddh =
                 from dh in data.DONDATHANGs
                 where dh.MaDDH == id
                 select dh;
                foreach (DONDATHANG ord in ddh)
                {
                    ord.GiaHang = true;
                    ord.ThanhToan = true;
                }
                data.SubmitChanges();
                khachhang = new KhachHang(id);
                for (int i = 0; i < khachhang.Sluong1.Count; i++)
                {
                    if (khachhang.MaDdong[i] != 0)
                    {
                        DONGHO dh = data.DONGHOs.Single(m => m.MaDongHo == khachhang.MaDdong[i]);
                        dh.SoLuong = (khachhang.Sluong1[i] - khachhang.SluongHang1[i]);
                        data.SubmitChanges();
                    }
                }
            }
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult TimKiem1(FormCollection collection, int? page)
        {
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            var tendn = collection["TimKiem"];
            ViewBag.tensp = tendn;
            List<DONGHO> dh = data.DONGHOs.Where(m => m.TenDongHo.Contains(tendn)).ToList();
            return View(dh.OrderBy(n => n.TenDongHo).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult TimKiem1( int? page,string tensp)
        {
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            ViewBag.tensp = tensp;
            List<DONGHO> dh = data.DONGHOs.Where(m => m.TenDongHo.Contains(tensp)).ToList();
            return View(dh.OrderBy(n => n.TenDongHo).ToPagedList(pageNumber, pageSize));
        }
    }
}