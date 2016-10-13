using Jego.Dialogs;
using Jego.FSM.Interfaces.FMS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMExcelLoadingWindowManager {
        private ExcelProgressWindow progressWindow;
        public FSMExcelLoadingWindowManager(ExcelProgressWindow progressWindow) {
            this.progressWindow = progressWindow;
        }
        public void Process(int progress) {
            progressWindow.setProgress(progress);
        }

        public void CloseWindow() {
            ExcelProgressWindow.CloseLoadingWindow();
        }
    }
}
