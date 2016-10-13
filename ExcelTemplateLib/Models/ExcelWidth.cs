using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelTemplateLib.Models {
    public class ExcelWidth {
        public int loc { get; set; }
        public double width { get; set; }
        public ExcelWidth(int loc, double width) {
            this.loc = loc;
            this.width = width;
        }
    }
}
