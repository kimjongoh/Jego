using Jego.Controls.TypeManageWindowControls;
using Jego.FSM.Collectors;
using Jego.FSM.Interfaces;
using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Managers;
using Jego.FSM.Managers.SubFSMs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jego.Dialogs {
    /// <summary>
    /// TypeManageDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TypeManageDialog : Window, IFSMControl, IFSMAddFoodTypeOutput {
        public TypeManageDialog() {
            InitializeComponent();
            RegisterCollector();
            FSMAddFoodTypeManager manager = FSMInputOutputManagerHub.GetAddFoodTypeManager();
            manager.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            TypeAddDialog dialog = TypeAddDialog.newInstance(Application.Current.MainWindow);
            dialog.ShowDialog();
        }

        public void IFSMO_AddFoodType(string type) {
            typeListView.Items.Add(new TypeManagerListItem(type));
        }

        public void RegisterCollector() {
            InputOutputUICollector.registerTypeManageDialog(this);
        }

        public void UnRegisterCollector() {
            InputOutputUICollector.unRegisterTypeManageDialog(this);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            UnRegisterCollector();
        }
    }
}
