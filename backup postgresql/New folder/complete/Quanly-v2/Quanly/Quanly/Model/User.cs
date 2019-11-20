using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanly.Model
{
    public static class User
    {
        //public const string Vaitro;


        /// <summary>
        /// Static value protected by access routine.
        /// </summary>
        static string _vaitro;

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static string vaitro
        {
            get
            {
                return _vaitro;
            }
            set
            {
                _vaitro = value;
            }
        }


        /// <summary>
        /// Global static field.
        /// </summary>
       // public static bool GlobalBoolean;
    }
}
