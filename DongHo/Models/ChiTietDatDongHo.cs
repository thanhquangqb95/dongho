using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DongHo.Models
{
    public class ChiTietDatDongHo
    {
        private string hinhsq;
        private string tensp;
        private int giasq;
        private int soluongsp;
        private int tonggiasp;
        public string Hinhsq
        {
            get
            {
                return hinhsq;
            }

            set
            {
                hinhsq = value;
            }
        }

        public string Tensp
        {
            get
            {
                return tensp;
            }

            set
            {
                tensp = value;
            }
        }

        public int Giasq
        {
            get
            {
                return giasq;
            }

            set
            {
                giasq = value;
            }
        }

        public int Soluongsp
        {
            get
            {
                return soluongsp;
            }

            set
            {
                soluongsp = value;
            }
        }

        public int Tonggiasp
        {
            get
            {
                return tonggiasp;
            }

            set
            {
                tonggiasp = value;
            }
        }
        public ChiTietDatDongHo(string hinhsq, string tensp, int giasq, int soluongsp, int tonggiasp)
        {
            this.hinhsq = hinhsq;
            this.tensp = tensp;
            this.giasq = giasq;
            this.soluongsp = soluongsp;
            this.tonggiasp = tonggiasp;
        }
    }
}