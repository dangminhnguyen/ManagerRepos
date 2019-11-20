using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class Model
    {
        public string ma { get; set; }
        public string ten { get; set; }
        public string mamay { get; set; }

        public Model(string ma, string ten,string mamay)
        {
            this.ma = ma;
            this.ten = ten;
            this.mamay = mamay;
        }
        public Model()
        {
            this.ma = null;
            this.ten = null;
            mamay = null;
        }
    }
}
