using ExcelTemplateLib.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace ExcelTemplateLib.Managers {
    public class ExcelManager {
        public static void SaveExcel(BaseExcelTemplate excelTemplate) {
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;

            try {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Add();
                excelTemplate.WriteExcelData(wb);

                wb.SaveAs(excelTemplate.GetExcelURL(), Excel.XlFileFormat.xlWorkbookNormal);
                wb.Close(true);
                excelApp.Quit();
            } finally {
                excelTemplate.releasSheet();
                ReleaseExcel(wb, excelApp);
            }
        }

        public static void ReleaseExcel(Excel.Worksheet ws) {
            ReleaseExcelObject(ws);
        }

        public static void ReleaseExcel(Excel.Workbook wb, Excel.Application excelApp) {
            ReleaseExcelObject(wb);
            ReleaseExcelObject(excelApp);
        }

        public static void ReleaseExcel(Excel.Worksheet ws, Excel.Workbook wb, Excel.Application excelApp) {
            ReleaseExcelObject(ws);
            ReleaseExcelObject(wb);
            ReleaseExcelObject(excelApp);
        }

        private static void ReleaseExcelObject(object obj) {
            try {
                if (obj != null) {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            } catch (Exception ex) {
                obj = null;
                throw ex;
            } finally {
                GC.Collect();
            }
        }
    }
}
