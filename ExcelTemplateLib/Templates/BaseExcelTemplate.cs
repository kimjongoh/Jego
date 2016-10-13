using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelTemplateLib.Templates {
    
    public abstract class BaseExcelTemplate {
        public BaseExcelTemplate(string excelURL) {
            this.excelURL = excelURL;
        }
        public string excelURL { get; set; }
        public abstract void WriteExcelData(Excel.Workbook wb);
        public abstract string GetExcelURL();

        public abstract void releasSheet();
    }
    
}
