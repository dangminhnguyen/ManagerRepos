using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class HienTrang
    {
        public int ma { get; set; }
        public string ten { get; set; }

        public HienTrang(int ma, string ten)
        {
            this.ma = ma;
            this.ten = ten;
        }
        public HienTrang()
        {
            this.ma = 0;
            this.ten = null;
        }
    }
}
