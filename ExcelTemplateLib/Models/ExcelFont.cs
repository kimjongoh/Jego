using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelTemplateLib.Models {
     public class ExcelFont {
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public bool FontBold { get; set; }

        public Microsoft.Office.Interop.Excel.XlHAlign hAlign { get; set; }

        public ExcelFont(int fontSize, bool fontBold) {
            this.FontName = "돋움";
            this.FontSize = fontSize;
            this.FontBold = fontBold;
            this.hAlign = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }

        public ExcelFont(int fontSize, bool fontBold, Microsoft.Office.Interop.Excel.XlHAlign hAlign) {
            this.FontName = "돋움";
            this.FontSize = fontSize;
            this.FontBold = fontBold;
            this.hAlign = hAlign;
        }
    }
}
