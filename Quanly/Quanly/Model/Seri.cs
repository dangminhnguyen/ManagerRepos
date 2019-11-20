using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class Seri
    {
        public int pk { get; set; }
        public string ma { get; set; }
        public string ten { get; set; }
        public string mamodel { get; set; }
        public string mamay { get; set; }
        public string khachhang { get; set; }
        public int hientrang { get; set; }
        public string phutrach { get; set; }
        public DateTime? ngaylapdat { get; set; }

        public Seri(string ma, string ten, string mamay,
            string mamodel,string khachhang,int hientrang,DateTime? ngaylapdat, string phutrach)
        {
            this.ma = ma;
            this.ten = ten;
            this.mamodel = mamodel;
            this.mamay = mamay;
            this.khachhang = khachhang;
            this.hientrang = hientrang;
            this.ngaylapdat = ngaylapdat;
            this.phutrach = phutrach;
        }
        public Seri()
        {
            this.ma = null;
            this.ten = null;
            this.mamodel = null;
            this.mamay = null;
            this.khachhang = null;
            this.hientrang = 0;
            this.pk = 0;
            this.ngaylapdat = null;
            this.phutrach = null;
        }
    }
}
