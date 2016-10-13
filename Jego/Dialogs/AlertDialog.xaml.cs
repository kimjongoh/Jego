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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Jego.Dialogs {
    /// <summary>
    /// AlertDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AlertDialog : Window {
        public delegate void ButtonClickDelegate(AlertDialog alertDialog);
        public ButtonClickDelegate positiveButtonClickCallBack;
        public ButtonClickDelegate negativeButtonClickCallBack;
        public AlertDialog() {
            InitializeComponent();
            ShowInTaskbar = false;
        }

        public AlertDialog(DependencyObject dependencyObject) {
            InitializeComponent();
            Owner = Window.GetWindow(dependencyObject);
            ShowInTaskbar = false;
            negative_button.Visibility = Visibility.Collapsed;
        }

        public static AlertDialog newInstance(DependencyObject dependencyObject) {
            return new AlertDialog(dependencyObject);
        }

        public AlertDialog setPositiveButton(string buttonTitle, ButtonClickDelegate positiveButtonClickDelegate) {
            positive_button.Content = buttonTitle;
            positiveButtonClickCallBack = positiveButtonClickDelegate;
            return this;
        }

        public AlertDialog setPositiveButton(string buttonTitle) {
            positive_button.Content = buttonTitle;
            positiveButtonClickCallBack = null;
            return this;
        }

        public AlertDialog setNegativeButton(string buttonTitle, ButtonClickDelegate negativeButtonClickDelegate) {
            negative_button.Visibility = Visibility.Visible;
            negative_button.Content = buttonTitle;
            negativeButtonClickCallBack = negativeButtonClickDelegate;
            return this;
        }

        public AlertDialog setNegativeButton(string buttonTitle) {
            negative_button.Visibility = Visibility.Visible;
            negative_button.Content = buttonTitle;
            negativeButtonClickCallBack = null;
            return this;
        }


        public AlertDialog setTitle(string title) {
            title_textBlock.Text = title;
            return this;
        }

        public AlertDialog setMessage(string message) {
            message_textBlock.Text = message;
            return this;
        }

        private void positive_button_Click(object sender, RoutedEventArgs e) {
            if (positiveButtonClickCallBack != null)
                positiveButtonClickCallBack(this);
            Close();
        }

        private void negative_button_Click(object sender, RoutedEventArgs e) {
            if (negativeButtonClickCallBack != null)
                negativeButtonClickCallBack(this);
            Close();
        }

        private void negative_button_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (negative_button.Visibility == Visibility.Collapsed) {
                Grid.SetColumnSpan(positive_button, 2);
            } else {
                Grid.SetColumnSpan(positive_button, 1);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            ColorAnimation colorChangeAnimation = new ColorAnimation();

            Color transparentBackgroundColor = (Color)ColorConverter.ConvertFromString("#00000000");
            Color blackBackgroundColor = (Color)ColorConverter.ConvertFromString("#88000000");

            colorChangeAnimation.From = transparentBackgroundColor;
            colorChangeAnimation.To = blackBackgroundColor;
            colorChangeAnimation.Duration = new TimeSpan(500 * 1000);

            PropertyPath colorTargetPath = new PropertyPath("(Panel.Background).(SolidColorBrush.Color)");
            Storyboard CellBackgroundChangeStory = new Storyboard();
            Storyboard.SetTarget(colorChangeAnimation, this);
            Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
            CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
            CellBackgroundChangeStory.Begin();
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
