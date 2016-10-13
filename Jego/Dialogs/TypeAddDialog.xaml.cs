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
using System.Windows.Shapes;

namespace Jego.Dialogs {
    /// <summary>
    /// TypeAddDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TypeAddDialog : Window {
        private TypeAddDialog() {
            InitializeComponent();
        }

        private TypeAddDialog(DependencyObject dependencyObject) {
            InitializeComponent();
            Owner = Window.GetWindow(dependencyObject);
            ShowInTaskbar = false;
        }

        public static TypeAddDialog newInstance(DependencyObject dependencyObject) {
            return new TypeAddDialog(dependencyObject);
        }

        private void positive_button_Click(object sender, RoutedEventArgs e) {
            if (!message_textBlock.Text.Trim().Equals("")) {
                FSMInputOutputManagerHub.GetAddFoodTypeManager().Process(message_textBlock.Text.Trim());
                Close();
            }
            
        }

        private void negative_button_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            Point position = Owner.PointToScreen(new Point(0d, 0d));
            this.Left = position.X;
            this.Top = position.Y;
            this.Width = Owner.ActualWidth;
            this.Height = Owner.ActualHeight;
        }

    }
}
