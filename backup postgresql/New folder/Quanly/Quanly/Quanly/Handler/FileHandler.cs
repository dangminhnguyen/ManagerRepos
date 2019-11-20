using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Handler
{
    class FileHandler
    {
        string connString;
        public FileHandler()
        {
            connString = new Property().ConnString;

        }
    }
}
