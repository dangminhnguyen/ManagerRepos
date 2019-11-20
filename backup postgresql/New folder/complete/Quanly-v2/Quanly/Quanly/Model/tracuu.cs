using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class tracuu
    {
        public int pk { get; set; }
        public string seri { get; set; }
        public string model { get; set; }
        public string loaimay { get; set; }
        public string khachhang { get; set; }
        public string hientrang { get; set; }
        public string phutrach { get; set; }
        public string ghichu { get; set; }
        public DateTime thoigian { get; set; }


        public tracuu()
        {
            pk = 0;
            seri = "";
            hientrang = "";
            model = "";
            loaimay = "";
            khachhang = "";
            phutrach = "";
            ghichu = "";
            thoigian = DateTime.MinValue;

        }
    }
}
