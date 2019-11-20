using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class serinew
    {
        public int pk { get; set; }
        public string ma { get; set; }
        public string ten { get; set; }
        public string mamodel { get; set; }
        public string mamay { get; set; }
        public string khachhang { get; set; }
        public string hientrang { get; set; }
        public DateTime? ngaylapdat { get; set; }
        public string tenmodel { get; set; }
        public string tenmay { get; set; }
        public string tenkhachhang { get; set;}
        public string tenhientrang { get; set; }
        public string ghichu { get; set; }
        public string kysu { get; set; }
        public DateTime? currentTime { get; set; }








        public serinew(string ma, string ten, string mamay,
            string mamodel, string khachhang, string hientrang, DateTime? ngaylapdat, string tenmodel, string tenmay, string tenkhachhang, string tenhientrang, string ghichu, string kysu, DateTime? currentTime)
        {
            this.ma = ma;
            this.ten = ten;
            this.mamodel = mamodel;
            this.mamay = mamay;
            this.khachhang = khachhang;
            this.hientrang = hientrang;
            this.ngaylapdat = ngaylapdat;
            this.tenmodel = tenmodel;
            this.tenmay = tenmay;
            this.tenkhachhang = tenkhachhang;
            this.tenhientrang = tenhientrang;
            this.ghichu = ghichu;
            this.kysu = kysu;
            this.currentTime = currentTime;
        }
        public serinew()
        {
            this.ma = null;
            this.ten = null;
            this.mamodel = null;
            this.mamay = null;
            this.khachhang = null;
            this.hientrang = null;
            this.pk = 0;
            this.ngaylapdat = null;
            this.tenmodel = null;
            this.tenmay = null;
            this.tenhientrang = null;
            this.tenkhachhang = null;
            this.ghichu = null;
            this.kysu = null;
            this.currentTime = null;
        }
    }
}
