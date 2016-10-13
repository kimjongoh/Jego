using Jego.Helper;
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

namespace Jego.Controls.CustomControls {
    /// <summary>
    /// HoverImageButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HoverImageButton : UserControl {

        public string ActiveImageSrc {
            get { return (string)GetValue(ActiveImageSrcProperty); }
            set { SetValue(ActiveImageSrcProperty, value); }
        }
        public string HoverImageSrc {
            get { return (string)GetValue(HoverImageSrcProperty); }
            set { SetValue(HoverImageSrcProperty, value); }
        }

        public event RoutedEventHandler Click {
            add { AddHandler(OnClickEvent, value); }
            remove { RemoveHandler(OnClickEvent, value); }
        }


        public static readonly DependencyProperty ActiveImageSrcProperty = DependencyProperty.Register("ActiveImageSrc", typeof(string), typeof(HoverImageButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(ActiveImageSrcPropertyyChanged)));
        public static readonly DependencyProperty HoverImageSrcProperty = DependencyProperty.Register("HoverImageSrc", typeof(string), typeof(HoverImageButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(HoverSourcePropertyChanged)));
        public static readonly RoutedEvent OnClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(HoverImageButton));


        public HoverImageButton() {
            InitializeComponent();
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            hoverButton.Click += hoverButton_Click;

        }

        void hoverButton_Click(object sender, RoutedEventArgs e) {
            RaiseEvent(new RoutedEventArgs(OnClickEvent));
        }


        private static void ActiveImageSrcPropertyyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            HoverImageButton imageButton = dependencyObject as HoverImageButton;
            var template = imageButton.hoverButton.Template;
            bool isTrue = imageButton.hoverButton.ApplyTemplate();

            Image myControl = (Image)template.FindName("icon_image", imageButton.hoverButton);
            myControl.Source = ImageResourceLoader.loadImageFromResource(e.NewValue as string);

        }

        private static void HoverSourcePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            HoverImageButton imageButton = dependencyObject as HoverImageButton;
            var template = imageButton.hoverButton.Template;

            bool isTrue = imageButton.hoverButton.ApplyTemplate();
            Image myControl = (Image)template.FindName("icon_image_hover", imageButton.hoverButton);
            myControl.Source = ImageResourceLoader.loadImageFromResource(e.NewValue as string);
        }
    }
}
