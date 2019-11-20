using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class LoaiMay
    {
        public string ma { get; set; }
        public string ten { get; set; }

        public LoaiMay(string ma, string ten)
        {
            this.ma = ma;
            this.ten = ten;
        }
        public LoaiMay()
        {
            this.ma = null;
            this.ten = null;
        }
    }
}
