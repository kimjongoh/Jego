using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelTemplateLib.Models {
    public class ExcelCellRange {
        public int s_row { get; set; }
        public int s_col { get; set; }
        public int e_row { get; set; }
        public int e_col { get; set; }
        public bool isMerge { get; set; }
        public int color { get; set; }


        public ExcelCellRange(int s_row, int s_col) {
            this.s_row = s_row;
            this.s_col = s_col;
            this.e_row = s_row;
            this.e_col = s_col;
            this.isMerge = false;
            this.color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        }

        public ExcelCellRange(int s_row, int s_col, int e_row, int e_col) {
            this.s_row = s_row;
            this.s_col = s_col;
            this.e_row = e_row;
            this.e_col = e_col;
            this.isMerge = true;
            this.color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        }

        public ExcelCellRange(int s_row, int s_col, System.Drawing.Color color) {
            this.s_row = s_row;
            this.s_col = s_col;
            this.e_row = s_row;
            this.e_col = s_col;
            this.isMerge = false;
            this.color = System.Drawing.ColorTranslator.ToOle(color);
        }

        public ExcelCellRange(int s_row, int s_col, int e_row, int e_col, System.Drawing.Color color) {
            this.s_row = s_row;
            this.s_col = s_col;
            this.e_row = e_row;
            this.e_col = e_col;
            this.isMerge = true;
            this.color = System.Drawing.ColorTranslator.ToOle(color);
        }
    }
}
