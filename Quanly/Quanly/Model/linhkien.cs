using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{

    class linhkien
    {
        public string model { get; set; }
        public string ma { get; set; }
        public string ten { get; set; }

        public linhkien()
        {
            this.model = null;
            this.ma = null;
            this.ten = null;
        }
        public linhkien(string model, string ma, string ten)
        {
            this.model = model;
            this.ma = ma;
            this.ten = ten;
        }
    }
}
