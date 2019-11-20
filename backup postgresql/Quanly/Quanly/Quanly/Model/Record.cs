using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class Record
    {
        public int pk { get; set; }
        public int keyseri { get; set; }
        public string hientrang { get; set; }
        public string khacphuc { get; set; }
        public string phutrach { get; set; }
        public string ghichu { get; set; }
        public DateTime thoigian { get; set; }
        public string filepath { get; set; }

        public Record()
        {
            pk = 0;
            keyseri = 0;
            hientrang = "";
            khacphuc = "";
            phutrach = "";
            ghichu = "";
            thoigian = DateTime.MinValue;
            filepath = "";

        }
    }
}
