using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class KhachHang
    {
        public string ma { get; set; }
        public string ten { get; set; }
        public string thongtin { get; set; }

        public KhachHang(string ma, string ten,string thongtin)
        {
            this.ma = ma;
            this.ten = ten;
            this.thongtin = thongtin;
        }

        public KhachHang()
        {
            this.ma = null;
            this.ten = null;
            this.thongtin = null;
        }
    }
}
