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
            
        }

        void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //btnStart.Content = "Start";
            //string result = "작업이 완료되었습니다.";

            //// 작업이 취소된 경우
            //if (e.Cancelled) {
            //    result = "작업이 취소되었습니다.";
            //}

            //MessageBox.Show(result);
        }
        void thread_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            int value = e.ProgressPercentage;

            // 변경 값으로 갱신
            //progress.Value = value;
            //progressValue.Text = value.ToString() + "%";
        }

        public abstract void thread_DoWork(object sender, DoWorkEventArgs e);
        public void BackgroundProcess() {
            thread.RunWorkerAsync();
        }

        public abstract string getFilePath();
    }
}
