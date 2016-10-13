using Jego.FSM.Collectors;
using Jego.FSM.Interfaces;
using Jego.FSM.Interfaces.FMS.During;
using Jego.FSM.Interfaces.FMS.Input;
using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Managers;
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

namespace Jego.Controls.MainPages {
    /// <summary>
    /// DateControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DateControl : UserControl, IFSMControl, IFSMChangeDateDuring, IFSMSaveInputOutputDate {
        private DateTime date;
        public DateControl() {
            InitializeComponent();
            RegisterCollector();
        }

        public void RegisterCollector() {
            InputOutputUICollector.registerDateControl(this);
        }

        public void UnRegisterCollector() {
            InputOutputUICollector.unRegisterDateControl(this);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            UnRegisterCollector();
        }

        public void IFSMD_ChangeDate(DateTime date) {
            this.date = date;
            year_textblock.Text = date.Year.ToString();
            month_textblock.Text = date.Month.ToString();
            day_textblock.Text = date.Day.ToString();
        }

        public DateTime FSMI_SaveInputOutputDate() {
            return date;
        }

        private void PrevDateButton_Click(object sender, RoutedEventArgs e) {
            FSMInputOutputManagerHub.GetChangeDateManager().Process(date.AddDays(-1));
        }

        private void NextDateButton_Click(object sender, RoutedEventArgs e) {
            FSMInputOutputManagerHub.GetChangeDateManager().Process(date.AddDays(1));
        }

        public DateTime GetDate() {
            return date;
        }
    }
}
