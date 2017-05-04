using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQPatcher
{
    public static class StringExtensions
    {
        public static bool HasValue(this string str)
        {
            return str != null && str.Length > 0;
        }
    }
}
