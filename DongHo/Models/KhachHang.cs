using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DongHo.Models;
using System.Data;
namespace DongHo.Models
{
    public class KhachHang
    {
        DONGHODataContext data = new DONGHODataContext();
        
        String tenkh;
        String DiaChi;
        String SDT;
        int MaDDH;
        private List<int> maDdong;
        private List<int> Sluong;
        private List<int> SluongHang;
        private List<int> TongGia;
        private List<String> Tendongho;
        private List<String> hinhdongho;
        private List<int> Giadongho;
        List<CTDDH> dondh;
        public List<DONGHO> dongho;
        private List<ChiTietDatDongHo> Dsdongho;

        public string Tenkh
        {
            get
            {
                return tenkh;
            }

            set
            {
                tenkh = value;
            }
        }

        public string SDT1
        {
            get
            {
                return SDT;
            }

            set
            {
                SDT = value;
            }
        }

        public int MaDDH1
        {
            get
            {
                return MaDDH;
            }

            set
            {
                MaDDH = value;
            }
        }


        public List<int> MaDdong
        {
            get
            {
                return maDdong;
            }

            set
            {
                maDdong = value;
            }
        }

        public List<string> Tendongho1
        {
            get
            {
                return Tendongho;
            }

            set
            {
                Tendongho = value;
            }
        }

        public List<int> Giadongho1
        {
            get
            {
                return Giadongho;
            }

            set
            {
                Giadongho = value;
            }
        }

        public List<int> Sluong1
        {
            get
            {
                return Sluong;
            }

            set
            {
                Sluong = value;
            }
        }

        public List<int> SluongHang1
        {
            get
            {
                return SluongHang;
            }

            set
            {
                SluongHang = value;
            }
        }
        

        public string DiaChi1
        {
            get
            {
                return DiaChi;
            }

            set
            {
                DiaChi = value;
            }
        }

        public List<int> TongGia1
        {
            get
            {
                return TongGia;
            }

            set
            {
                TongGia = value;
            }
        }

        public List<ChiTietDatDongHo> Dsdongho1
        {
            get
            {
                return Dsdongho;
            }

            set
            {
                Dsdongho = value;
            }
        }

        public List<string> Hinhdongho
        {
            get
            {
                return hinhdongho;
            }

            set
            {
                hinhdongho = value;
            }
        }


        public KhachHang(int id)
        {
            int makh = 0;
            MaDdong = new List<int>();
            dondh = new List<CTDDH>();
            SluongHang1 = new List<int>();
            Sluong1 = new List<int>();
            TongGia1 = new List<int>();
            Dsdongho1 = new List<ChiTietDatDongHo>();
            Tendongho = new List<string>();
            hinhdongho = new List<string>();
            Giadongho = new List<int>();
            DONDATHANG kh = data.DONDATHANGs.Single(m => m.MaDDH == id);
            makh = kh.MaKH;
            KHACHHANG kh2 = data.KHACHHANGs.Single(m => m.MaKH == makh);
            Tenkh = kh2.TenKH;
            SDT1 = kh2.SDT;
            MaDDH1 = kh.MaDDH;
            DiaChi1 = kh2.DiaChi;
            var cthd = from ct in data.CTDDHs where ct.MaDDH == kh.MaDDH select ct;
            foreach (CTDDH item in cthd)
            {
                MaDdong.Add(item.MaDongHo);
                SluongHang1.Add(int.Parse(item.SoLuong.ToString()));
                TongGia1.Add(int.Parse(item.TongGia.ToString()));
            }
            int x = MaDdong.Count();
            for (int i = 0; i < x; i++)
            {
                var doh = from dh in data.DONGHOs where dh.MaDongHo == MaDdong[i] select dh;
                foreach (DONGHO item in doh)
                {
                    Hinhdongho.Add(item.hinh);
                    Tendongho1.Add(item.TenDongHo);
                    Giadongho1.Add(int.Parse(item.DonGia.ToString()));
                    Sluong1.Add(int.Parse(item.SoLuong.ToString()));
                }
            }
            for (int i = 0; i < x; i++)
            {
                ChiTietDatDongHo dh = new ChiTietDatDongHo(Hinhdongho[i],Tendongho1[i],Giadongho1[i], SluongHang1[i],TongGia1[i]);
                Dsdongho1.Add(dh);
            }
        }
    }
}