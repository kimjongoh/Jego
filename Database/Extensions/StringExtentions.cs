using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Extensions {
    public static class StringExtentions {
        public static bool isNotEmptyString(this string str) {
            if (str != null && !"".Equals(str.Trim())) return true;
            else return false;
        }
    }
}
