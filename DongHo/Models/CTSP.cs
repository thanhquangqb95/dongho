using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DongHo.Models;
namespace DongHo.Models
{
    public class CTSP
    {
        DONGHODataContext data = new DONGHODataContext();
        private String Hinh;
        private int dongia;
        private String ten;
        private int maLoai;
        private int MaDH;
        private String TenLoai;
        private int MaNCC;
        private String TenNCC;
        String DiaCHi;
        public string Hinh1
        {
            get
            {
                return Hinh;
            }

            set
            {
                Hinh = value;
            }
        }

        public int Dongia
        {
            get
            {
                return dongia;
            }

            set
            {
                dongia = value;
            }
        }

        public string Ten
        {
            get
            {
                return ten;
            }

            set
            {
                ten = value;
            }
        }

        public int MaLoai
        {
            get
            {
                return maLoai;
            }

            set
            {
                maLoai = value;
            }
        }

        public string TenLoai1
        {
            get
            {
                return TenLoai;
            }

            set
            {
                TenLoai = value;
            }
        }

        public int MaNCC1
        {
            get
            {
                return MaNCC;
            }

            set
            {
                MaNCC = value;
            }
        }

        public string TenNCC1
        {
            get
            {
                return TenNCC;
            }

            set
            {
                TenNCC = value;
            }
        }

        public int MaDH1
        {
            get
            {
                return MaDH;
            }

            set
            {
                MaDH = value;
            }
        }

        public string DiaCHi1
        {
            get
            {
                return DiaCHi;
            }

            set
            {
                DiaCHi = value;
            }
        }

        public CTSP(int mdh)
        {
            DONGHO dh = data.DONGHOs.Single(m => m.MaDongHo == mdh);
            MaDH1 = int.Parse(dh.MaDongHo.ToString());
            Hinh1 = dh.hinh;
            Dongia = int.Parse(dh.DonGia.ToString());
            Ten = dh.TenDongHo.ToString();
            MaLoai = dh.MaLoai;
            MaNCC1 = dh.MaNCC;
            LOAISP sp = data.LOAISPs.Single(m => m.MaLoai == MaLoai);
            TenLoai1 = sp.TenLoai.ToString();
            NHACUNGCAP ncc = data.NHACUNGCAPs.Single(m => m.MaNCC == MaNCC1);
            TenNCC1 = ncc.TenNSX.ToString();
            DiaCHi1 = ncc.DiaChi;
        }
    }
}