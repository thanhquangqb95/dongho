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
        String SDT;
        int MaDDH;
        private List<int> maDdong;
        private List<String> Tendongho;
        private List<int> Giadongho;
        List<CTDDH> dondh;

        public List<DONGHO> dongho;
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

        public KhachHang(int id)
        {
            int makh = 0;
            MaDdong = new List<int>();
            dondh = new List<CTDDH>();
            dongho = new List<DONGHO>();
            DONDATHANG kh = data.DONDATHANGs.Single(m => m.MaDDH == id);
            makh = kh.MaKH;
            KHACHHANG kh2 = data.KHACHHANGs.Single(m => m.MaKH == makh);
            Tenkh = kh2.TenKH;
            SDT1 = kh2.SDT;
            MaDDH1 = kh.MaDDH;
            var cthd = from ct in data.CTDDHs where ct.MaDDH == kh.MaDDH select ct;
            foreach (CTDDH item in cthd)
            {
                MaDdong.Add(item.MaDongHo);
            }
            int x = MaDdong.Count();
            for (int i = 0; i < x; i++)
            {
                var doh = from dh in data.DONGHOs where dh.MaDongHo == MaDdong[i] select dh;
                foreach (DONGHO item in doh)
                {
                     dongho.Add(item);
                }
            }
        }
    }
}