using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softgen
{
    public static class FlagManager
    {
        private static string flag;

        public static string Flag 
        { get { return flag; }
            set { flag = value; } }

    }

}
