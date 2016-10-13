using Jego.FSM.Collectors;
using Jego.FSM.Interfaces;
using Jego.FSM.Interfaces.FMS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Jego.Dialogs {
    /// <summary>
    /// ExcelProgressWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ExcelProgressWindow : Window, IFSMControl {
        private IFSMWriteExcelManager manager;

        public static void ShowLoadingWindow(IFSMWriteExcelManager manager) {
            MainWindow mainwindow = Application.Current.MainWindow as MainWindow;
            ExcelProgressWindow loadingWindow = new ExcelProgressWindow(manager);
            loadingWindow.Owner = Window.GetWindow(mainwindow);
            loadingWindow.ShowInTaskbar = false;
            loadingWindow.Show();
        }

        public static void CloseLoadingWindow() {
            WindowCollection windows = Application.Current.Windows;
            foreach (Window window in windows) {
                if (window is ExcelProgressWindow) {
                    ExcelProgressWindow loadingWindow = window as ExcelProgressWindow;
                    loadingWindow.Close();
                }
            }
        }

        public ExcelProgressWindow(IFSMWriteExcelManager manager) {
            InitializeComponent();
            this.manager = manager;
        }

        public void RegisterCollector() {
            InputOutputUICollector.registerExcelProgressWindow(this);
        }

        public void UnRegisterCollector() {
            InputOutputUICollector.unRegisterExcelProgressWindow(this);
        }

        public void setProgress(int progress) {
            progressbar.Value = progress;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            RegisterCollector();
            manager.BackgroundProcess();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            UnRegisterCollector();
            manager = null;
        }
    }
}
