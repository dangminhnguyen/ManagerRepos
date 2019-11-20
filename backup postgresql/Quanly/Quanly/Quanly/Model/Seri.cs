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
        public string hientrang { get; set; }
        public DateTime? ngaylapdat { get; set; }
      

        public Seri(string ma, string ten, string mamay,
            string mamodel,string khachhang,string hientrang,DateTime? ngaylapdat)
        {
            this.ma = ma;
            this.ten = ten;
            this.mamodel = mamodel;
            this.mamay = mamay;
            this.khachhang = khachhang;
            this.hientrang = hientrang;
            this.ngaylapdat = ngaylapdat;
        }
        public Seri()
        {
            this.ma = null;
            this.ten = null;
            this.mamodel = null;
            this.mamay = null;
            this.khachhang = null;
            this.hientrang = null;
            this.pk = 0;
            this.ngaylapdat = null;
        }
    }
}
