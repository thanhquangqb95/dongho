using DongHo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DongHo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SanPham()
        {
            return View();

        }
        public ActionResult SanPhamNu()
        {
            var SanPham = new List<SanPhamNu>();
            SanPham.Add(new SanPhamNu(1, "$223", "Rolex1", "/image/dongho/DH1.2.jpg"));
            SanPham.Add(new SanPhamNu(2, "$223", "Rolex1", "/image/dongho/DH1.2.jpg"));
            SanPham.Add(new SanPhamNu(3, "$223", "Rolex1", "/image/dongho/DH1.2.jpg"));
            return View(SanPham);
        }
    
    }
   
}