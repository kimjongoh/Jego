using Jego.Dialogs;
using Jego.FSM.Managers;
using Jego.FSM.Managers.SubFSMs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Managers {
    public abstract class IFSMWriteExcelManager {
        protected BackgroundWorker thread;

        public IFSMWriteExcelManager() {
            initThread();
        }

        private void initThread() {
            this.thread = new BackgroundWorker();
            thread.WorkerReportsProgress = true;
            thread.WorkerSupportsCancellation = false;
            thread.DoWork += new DoWorkEventHandler(thread_DoWork);
            thread.ProgressChanged += new ProgressChangedEventHandler(thread_ProgressChanged);
            thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_RunWorkerCompleted);
        }

        public void Process() {
            ExcelProgressWindow.ShowLoadingWindow(this);
        }

        void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            FSMExcelLoadingWindowManager manager = FSMInputOutputManagerHub.GetExcelLoadingWindowManager();
            manager.CloseWindow();
        }
        void thread_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            int value = e.ProgressPercentage;
            FSMExcelLoadingWindowManager manager = FSMInputOutputManagerHub.GetExcelLoadingWindowManager();
            manager.Process(value);
        }

        public abstract void thread_DoWork(object sender, DoWorkEventArgs e);
        public void BackgroundProcess() {
            thread.RunWorkerAsync();
        }

        public abstract string getFilePath();
    }
}
