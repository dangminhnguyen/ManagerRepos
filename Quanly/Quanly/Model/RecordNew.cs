using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class RecordNew
    {
        public int pk { get; set; }
        public int keyseri { get; set; }   
        public string khachhang { get; set; }
        public string lklay { get; set; }
        public string kslay { get; set; }
        public string lydolay { get; set; }
        public DateTime ngaylay { get; set; }
        public string ttrlay { get; set; }
        public DateTime ngayPh { get; set; }
        public string ksPh { get; set; }
        public string ttrPhLay { get; set; }
        public string srmayphlap { get; set; }
        public string vtrmoiPhlay { get; set; }
        public string ghichuphlay { get; set; }
        public string ttrphlap { get; set; }
        public string srmayphlay { get; set; }
        public string xuatsu { get; set; }
        public string ghichuphlap { get; set; }
        public RecordNew()
        {
            pk = 0;
            keyseri = 0;
            khachhang = "";
            lklay = "";
            kslay = "";
            lydolay = "";
            ngaylay = DateTime.MinValue;
            ttrlay = "";
            ngayPh = DateTime.MinValue;
            ksPh = "";
            ttrPhLay = "";
            srmayphlap = "";
            vtrmoiPhlay = "";
            ghichuphlay = "";
            ttrphlap = "";
            srmayphlay = "";
            xuatsu = "";
            ghichuphlap = "";

    }
    }
}
