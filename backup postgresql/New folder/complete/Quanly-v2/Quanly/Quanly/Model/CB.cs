using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class CB
    {
       // public int pk { get; set; }
        public string seri { get; set; }
        public string mamodel { get; set; }
        public string model { get; set; }
        public string mamay { get; set; }
        public string loaimay { get; set; }
        public string khachhang { get; set; }
        
        public string phutrach { get; set; }
        


        public CB()
        {
           // pk = 0;
            seri = "";
            mamodel = "";
            model = "";
            mamay = "";
            loaimay = "";
            khachhang = "";
            phutrach = "";
           // thoigian = DateTime.MinValue;

        }
    }
}
