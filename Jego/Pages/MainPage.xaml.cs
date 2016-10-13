using Jego.FSM.Managers;
using Jego.SharedPreferences;
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

namespace Jego.Pages {
    /// <summary>
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
            init();
        }

        private void init() {
            initFoodType();
            initChangeDate();
        }

        private void initFoodType() {
            FSMInputOutputManagerHub.GetAddFoodTypeManager().Refresh();
        }

        private void initChangeDate() {
            FSMInputOutputManagerHub.GetChangeDateManager().Process(DateTime.Now);
        } 
    }
}
