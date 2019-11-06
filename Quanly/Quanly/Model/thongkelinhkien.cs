using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    class thongkelinhkien
    {
        public string ma { get; set; }
        public int keyseri { get; set; }
        public string maymoi { get; set; }
        public string mayve { get; set; }
        public string tinhtrang { get; set; }
        public string ttrangmayve { get; set; }
        public string vitrimoi { get; set; }
        public string xuatsu { get; set; }

        public thongkelinhkien()
        {
            this.ma = "";
            this.keyseri = 0;
            this.maymoi = "";
            this.mayve = "";
            this.tinhtrang = "";
            this.ttrangmayve = "";
            this.vitrimoi = "";
            this.xuatsu = "";
        }
        public thongkelinhkien(int keyseri, string ma,string tinhtrang, string maymoi, string vitrimoi, string mayve,string xuatsu, string ttrangmayve)
        {
            this.keyseri = keyseri;
            this.ma = ma;
            this.maymoi = maymoi;
            this.mayve = mayve;
            this.tinhtrang = tinhtrang;
            this.ttrangmayve = ttrangmayve;
            this.vitrimoi = vitrimoi;
            this.xuatsu = xuatsu;
        }
    }
}
