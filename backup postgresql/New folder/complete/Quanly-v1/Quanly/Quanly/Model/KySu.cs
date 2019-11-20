using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class KySu
    {
        public string ma { get; set; }
        public string ten { get; set; }

        public KySu(string ma, string ten)
        {
            this.ma = ma;
            this.ten = ten;
        }
        public KySu()
        {
            this.ma = null;
            this.ten = null ;
        }
    }
}
