using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Extensions
{
    public static class StringExtension
    {
        public static string ToBinDebug(this int a)
        {
            return Convert.ToString(a, 2);
        }
        public static string ToBinDebug(this uint a)
        {
            return Convert.ToString(a, 2);
        }
    }
}
