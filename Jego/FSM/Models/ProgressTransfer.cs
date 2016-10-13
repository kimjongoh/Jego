using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelTemplateLib.Interfaces;
using ExcelTemplateLib.Templates;

namespace Jego.FSM.Models {
    public class ProgressTransfer : IProgressTransfer {
        private BackgroundWorker worker;
        public ProgressTransfer(BackgroundWorker worker) {
            this.worker = worker;
        }

        public void ReportProgress(BaseExcelTemplate excelTemplete, int progress) {
            worker.ReportProgress(progress);
        }
    }
}
