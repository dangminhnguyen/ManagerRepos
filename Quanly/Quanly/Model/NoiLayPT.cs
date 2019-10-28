using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class NoiLayPT
    {
        public string ma { get; set; }
        public string ten { get; set; }

        public NoiLayPT()
        {
            this.ma = "";
            this.ten = "";
        }

        public NoiLayPT(string ma, string ten)
        {
            this.ma = ma;
            this.ten = ten;
        }
    }
}
