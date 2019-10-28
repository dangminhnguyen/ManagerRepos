using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly
{
    class Property
    {
        public string ConnString { get; set; }
        public Property()
        {
            ConnString = "Host=localhost;Username=postgres;Password=AVLnguyen;Database=postgres";
            //ConnString = "Host=localhost;Username=postgres;Password=Quangthai1011;Database=new";
            StringCipher sc = new StringCipher();
            //ConnString =sc.Decrypt(Properties.Settings.Default["Database"].ToString());
        }


    }
}
