using ExcelTemplateLib.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTemplateLib.Interfaces {
    public interface IProgressTransfer {
        void ReportProgress(BaseExcelTemplate excelTemplete, int progress);
    }
}
