using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DongHo.Models;
namespace DongHo.Models
{
    public class ChiTietDonDatHang
    {
        DONGHODataContext data = new DONGHODataContext();
        private String hoTenKh;
        private String sDT;
        private DateTime ngayDat;
        private DateTime ngayGiao;
        private int maDongHo;
        private int soLuong;
        private int tongTien;
        private String hinhDH;
        private int MaKhachHang;
        public string HoTenKh
        {
            get
            {
                return hoTenKh;
            }

            set
            {
                hoTenKh = value;
            }
        }
        public string SDT
        {
            get
            {
                return sDT;
            }

            set
            {
                sDT = value;
            }
        }
        public DateTime NgayDat
        {
            get
            {
                return ngayDat;
            }

            set
            {
                ngayDat = value;
            }
        }
        public DateTime NgayGiao
        {
            get
            {
                return ngayGiao;
            }

            set
            {
                ngayGiao = value;
            }
        }
        public int MaKhachHang1
        {
            get
            {
                return MaKhachHang;
            }

            set
            {
                MaKhachHang = value;
            }
        }
        public ChiTietDonDatHang(int DDH)
        {
            DONDATHANG dondathang = data.DONDATHANGs.Single(m => m.MaDDH == DDH);
            NgayDat = Convert.ToDateTime(dondathang.NgayDat);
            NgayGiao = Convert.ToDateTime(dondathang.NgayGiao);
            MaKhachHang1 = dondathang.MaKH;
            KHACHHANG kh = data.KHACHHANGs.Single(m => m.MaKH == MaKhachHang1);
            HoTenKh = kh.TenKH;
            sDT = kh.SDT;
        }

    }
}