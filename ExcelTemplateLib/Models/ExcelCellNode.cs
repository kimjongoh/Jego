using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelTemplateLib.Models {
    public class ExcelCellNode {
        public string data { get; set; }
        public ExcelCellRange range { get; set; }
        public ExcelFont font { get; set; }

        public bool isNumber { get; set; }

        public bool isFormula { get; set; }
        public ExcelCellNode(string data, ExcelCellRange range, ExcelFont font) {
            this.data = data;
            this.range = range;
            this.font = font;
            this.isFormula = false;
            this.isNumber = false;
        }

        public ExcelCellNode(string fomula, ExcelCellRange range, ExcelFont font, bool isFomula) {
            this.data = fomula;
            this.range = range;
            this.font = font;
            this.isFormula = isFomula;
            this.isNumber = false;
        }

        public ExcelCellNode(string fomula, ExcelCellRange range, ExcelFont font, bool isFomula, bool isNumber) {
            this.data = fomula;
            this.range = range;
            this.font = font;
            this.isFormula = isFomula;
            this.isNumber = isNumber;
        }
    }
}
