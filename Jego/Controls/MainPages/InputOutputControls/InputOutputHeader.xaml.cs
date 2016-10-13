using Jego.Dialogs;
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
using Jego.FSM.Collectors;
using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Managers;
using Jego.FSM.Interfaces;

namespace Jego.Controls.MainPages.InputOutputControls {
    /// <summary>
    /// InputOutputHeader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputOutputHeader : UserControl, IFSMControl, IFSMAddFoodTypeOutput {
        public InputOutputHeader() {
            InitializeComponent();
            initTypeButtons();
            RegisterCollector();
        }

        private void initTypeButtons() {
            category_stackPanel.Children.Clear();
        }

        private void AddTypeButton(string type) {
            bool isExist = false;
            foreach (Button button in category_stackPanel.Children) {
                if (type.Equals(button.Content as string)) {
                    isExist = true;
                    break;
                }
            }
            if (!isExist) {
                Button button = createFoodTypeButton(type);
                category_stackPanel.Children.Add(button);
            }
        }

        private Button createFoodTypeButton(string type) {
            Button button = new Button();
            button.Width = 90;
            button.Height = 30;
            button.Margin = new Thickness(10, 0, 0, 0);
            button.Content = type;
            button.Tag = type;
            button.Click += typeButton_Click;
            button.ContextMenu = new System.Windows.Controls.ContextMenu();
            button.ContextMenu.Items.Add(createMenuItem(button));
            return button;
        }

        private MenuItem createMenuItem(Button button) {
            MenuItem item = new MenuItem();
            item.Header = "삭제";
            item.Click += typeMenuItemDeleteClick;
            item.Tag = button;
            return item;
        }

        void typeMenuItemDeleteClick(object sender, RoutedEventArgs e) {
            MenuItem menuItem = sender as MenuItem;
            Button button = menuItem.Tag as Button;
            category_stackPanel.Children.Remove(button);
            string type = button.Content as string;
            FSMInputOutputManagerHub.GetRemoveFoodTypeManager().Process(type);
        }

        void typeButton_Click(object sender, RoutedEventArgs e) {
            
        }

        private void addType_button_Click(object sender, RoutedEventArgs e) {
            TypeAddDialog dialog = TypeAddDialog.newInstance(Application.Current.MainWindow);
            dialog.ShowDialog();
        }

        private void addType(string type) {
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            UnRegisterCollector();
        }

        public void IFSMO_AddFoodType(string type) {
            AddTypeButton(type);
        }

        public void RegisterCollector() {
            InputOutputUICollector.registerInputOutputHeader(this);
        }

        public void UnRegisterCollector() {
            InputOutputUICollector.unRegisterInputOutputHeader(this);
        }
    }
}
