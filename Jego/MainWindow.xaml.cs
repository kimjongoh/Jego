using Fluent;
using Jego.Dialogs;
using Jego.FSM.Managers;
using Jego.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jego {
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : RibbonWindow {
        public MainWindow() {
            InitializeComponent();
            init();
        }

        private void init() {
            mainPage_Frame.Content = new MainPage();
        }

        private void category_button_Click(object sender, RoutedEventArgs e) {
            TypeManageDialog dialog = new TypeManageDialog();
            dialog.Show();
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            FSMInputOutputManagerHub.GetSaveInputOutputManager().Process();
        }

        private void ExcelTodayButton_Click(object sender, RoutedEventArgs e) {
            FSMInputOutputManagerHub.GetTodayExcelWriteManager().Process();
        }

        private void ExcelTodaySimpleButton_Click(object sender, RoutedEventArgs e) {
            FSMInputOutputManagerHub.GetTodaySimpleExcelWriteManager().Process();
        }
    }
}
