using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class tong
    {
        public int pk { get; set; }
        public string seri { get; set; }
        public string model { get; set; }
        public string mamodel { get; set; }
        public string loaimay { get; set; }
        public string mamay { get; set; }
        public string khachhang { get; set; }
        public string makhachhang { get; set; }
        public string hientrang { get; set; }
        public string phutrach { get; set; }
        public DateTime? thoigian { get; set; }
        public string lichsu { get; set; }

        public tong(int pk, string seri, string model,
            string mamodel,string mamay, string loaimay, string makhachhang,string khachhang, string hientrang,string phutrach,string lichsu, DateTime? thoigian)
        {
            this.pk = pk;
            this.seri = seri;
            this.model = model;
            this.mamodel = mamodel;
            this.loaimay = loaimay;
            this.mamay = mamay;
            this.makhachhang = makhachhang;
            this.khachhang = khachhang;
            this.thoigian = thoigian;
            this.phutrach = phutrach;
            this.hientrang = hientrang;
            this.lichsu = lichsu;
        }
        public tong()
        {
            this.pk = 0;
            this.seri = null;
            this.model = null;
            this.mamodel = null;
            this.loaimay = null;
            this.mamay = null;
            this.makhachhang = null;
            this.khachhang = null;
            this.thoigian = null;
            this.phutrach = null;
            this.hientrang = null;
            this.lichsu = null;
            
        }
    }
}
