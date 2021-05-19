using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab16
{
    class Utils
    {
        private static Dictionary<int, String> dict;
        static Utils()
        {

        }

        public static String GetGroupByNumber(int number)
        {
            if (dict.ContainsKey(number))
                return dict[number];
            else
                return "???";
        }
    }
}
