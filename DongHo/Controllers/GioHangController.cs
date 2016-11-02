using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DongHo.Models;
namespace DongHo.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        DONGHODataContext data = new DONGHODataContext();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int iMasach, string strURL)
        {
            //Lay ra Session gio hang
            List<GioHang> lstGiohang = Laygiohang();
            //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
            GioHang sanpham = lstGiohang.Find(n => n.MaDongHO == iMasach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMasach);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.SoLuong++;
                return Redirect(strURL);
            }
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.SoLuong);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult GiohangPartial()
        {
            //Session["SOLuong"]= TongSoLuong();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        //Cap nhat Giỏ hàng
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {

            //Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MaDongHO == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                sanpham.SoLuong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        //Xoa Giohang
        public ActionResult XoaGiohang(int iMaSP)
        {
            //Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MaDongHO == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.MaDongHO == iMaSP);
                return RedirectToAction("GioHang");

            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("KhachHang", "Home");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "BookStore");
            }

            //Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            try
            {
                DONDATHANG ddh = new DONDATHANG();
                KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
                List<GioHang> gh = Laygiohang();
                ddh.MaKH = kh.MaKH;
                ddh.NgayDat = DateTime.Now;
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
                ddh.NgayGiao = DateTime.Parse(ngaygiao);
                ddh.GiaHang = false;
                ddh.ThanhToan = false;
                data.DONDATHANGs.InsertOnSubmit(ddh);
                data.SubmitChanges();
                //Them chi tiet don hang            
                foreach (var item in gh)
                {
                    CTDDH ctdh = new CTDDH();
                    ctdh.MaDDH = ddh.MaDDH;
                    ctdh.MaDongHo = item.MaDongHO;
                    ctdh.SoLuong = item.SoLuong;
                    ctdh.TongGia = (int)item.dThanhtien;
                    data.CTDDHs.InsertOnSubmit(ctdh);
                }
                data.SubmitChanges();
                Session["GioHang"] = null;
                return RedirectToAction("Xacnhandonhang", "GioHang");
            }catch(Exception e)
            {
                ViewBag.THONGBAO = "Vui Lòng Kiểm Tra lại Ngày Nhận Hàng";
                return RedirectToAction("DatHang", "GioHang");
            }
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult Onepay()
        {

            double t = TongTien();
            string amount = (t * 100).ToString();
            string url = RedirectOnepay("Thanh Toan Dong Ho", amount, "192.186.0.1");
            return Redirect(url);
        }
        public string RedirectOnepay(string codeInvoice, string amount, string ip)
        {
            // Khoi tao lop thu vien
            VPCRequest conn = new VPCRequest(OnePayProperty.Url_ONEPAY_TEST);
            conn.SetSecureSecret(OnePayProperty.HASH_CODE);

            // Gan cac thong so de truyen sang cong thanh toan onepay
            conn.AddDigitalOrderField("AgainLink", OnePayProperty.AGAIN_LINK);
            conn.AddDigitalOrderField("Title", "Tich hop onepay");
            conn.AddDigitalOrderField("vpc_Locale", OnePayProperty.PAYGATE_LANGUAGE);
            conn.AddDigitalOrderField("vpc_Version", OnePayProperty.VERSION);
            conn.AddDigitalOrderField("vpc_Command", OnePayProperty.COMMAND);
            conn.AddDigitalOrderField("vpc_Merchant", OnePayProperty.MERCHANT_ID);
            conn.AddDigitalOrderField("vpc_AccessCode", OnePayProperty.ACCESS_CODE);
            conn.AddDigitalOrderField("vpc_MerchTxnRef", "Thanhtoan");
            conn.AddDigitalOrderField("vpc_OrderInfo", codeInvoice);
            conn.AddDigitalOrderField("vpc_Amount", amount);
            conn.AddDigitalOrderField("vpc_ReturnURL", Url.Action("OnepayResponse", "GioHang", null, Request.Url.Scheme, null));

            // Thong tin them ve khach hang. De trong neu khong co thong tin
            conn.AddDigitalOrderField("vpc_SHIP_Street01", "");
            conn.AddDigitalOrderField("vpc_SHIP_Provice", "");
            conn.AddDigitalOrderField("vpc_SHIP_City", "");
            conn.AddDigitalOrderField("vpc_SHIP_Country", "");
            conn.AddDigitalOrderField("vpc_Customer_Phone", "");
            conn.AddDigitalOrderField("vpc_Customer_Email", "");
            conn.AddDigitalOrderField("vpc_Customer_Id", "");
            conn.AddDigitalOrderField("vpc_TicketNo", ip);

            string url = conn.Create3PartyQueryString();
            return url;
        }
        public ActionResult OnepayResponse()
        {
            string hashvalidateResult = "";

            // Khoi tao lop thu vien
            VPCRequest conn = new VPCRequest(OnePayProperty.Url_ONEPAY_TEST);
            conn.SetSecureSecret(OnePayProperty.HASH_CODE);

            // Xu ly tham so tra ve va du lieu ma hoa
            hashvalidateResult = conn.Process3PartyResponse(Request.QueryString);

            // Lay tham so tra ve tu cong thanh toan
            string vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode");
            string amount = conn.GetResultField("vpc_Amount");
            string localed = conn.GetResultField("vpc_Locale");
            string command = conn.GetResultField("vpc_Command");
            string version = conn.GetResultField("vpc_Version");
            string cardType = conn.GetResultField("vpc_Card");
            string orderInfo = conn.GetResultField("vpc_OrderInfo");
            string merchantID = conn.GetResultField("vpc_Merchant");
            string authorizeID = conn.GetResultField("vpc_AuthorizeId");
            string merchTxnRef = conn.GetResultField("vpc_MerchTxnRef");
            string transactionNo = conn.GetResultField("vpc_TransactionNo");
            string acqResponseCode = conn.GetResultField("vpc_AcqResponseCode");
            string txnResponseCode = vpc_TxnResponseCode;
            string message = conn.GetResultField("vpc_Message");

            // Kiem tra 2 tham so tra ve quan trong nhat
            if (hashvalidateResult == "CORRECTED" && txnResponseCode.Trim() == "0")
            {
                return View("PaySuccess");
            }
            else if (hashvalidateResult == "INVALIDATED" && txnResponseCode.Trim() == "0")
            {
                return View("PayPending");
            }
            else
            {
                return View("PayUnSuccess");
            }
        }
    }
}