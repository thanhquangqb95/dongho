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
            return View();
        }
        public ActionResult QLSANPHAM(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(data.DONGHOs.ToList().OrderByDescending(m=>m.MaDongHo).ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNSX), "MaNCC", "TenNSX");
            ViewBag.MaLoai = new SelectList(data.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            return View();
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
            //Lay ra doi tuong sach theo ma
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
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(DONGHO dongho, HttpPostedFileBase fileUpload)
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
        public ActionResult DonDatHang()
        {
            var groupJoinQuery =
          from ddh in data.DONDATHANGs where ddh.ThanhToan == false
          select ddh;
          return View(groupJoinQuery);
        }
        public ActionResult DatHang()
        {
            var groupJoinQuery =
          from ddh in data.DONDATHANGs
          where ddh.ThanhToan == false
          select ddh;
            return View(groupJoinQuery);
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
                try
                {
                    data.SubmitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return RedirectToAction("Index");
        }
    }
}