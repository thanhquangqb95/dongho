using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DongHo.Models;

namespace DongHo.Models
{
    public class GioHang
    {
        DONGHODataContext data = new DONGHODataContext();
        public int MaDongHO { set; get; }
        public string TenDH { set; get; }
        public string Hinh { set; get; }
        public Double DonGia { set; get; }
        public int SoLuong { set; get; }
        public Double dThanhtien
        {
            get { return SoLuong * DonGia; }

        }
        //Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
        public GioHang(int mdh)
        {
            MaDongHO = mdh;
            DONGHO dongho = data.DONGHOs.Single(n => n.MaDongHo == MaDongHO);
            TenDH = dongho.TenDongHo;
            Hinh = dongho.hinh;
            DonGia = double.Parse(dongho.DonGia.ToString());
            SoLuong = 1;
        }
    }
}