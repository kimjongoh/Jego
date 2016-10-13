using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Jego.Helper {
    public class ImageResourceLoader {
        public static BitmapImage loadImageFromResource(string imageName) {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(@"pack://application:,,,/"
             + Assembly.GetExecutingAssembly().GetName().Name
             + ";component/"
             + "Images/" + imageName, UriKind.Absolute);
            logo.EndInit();
            return logo;
        }

        public delegate void ImageLoadListener(BitmapImage image);
        public static void loadImageFromUri(string uri, ImageLoadListener listener) {
            ThreadPool.QueueUserWorkItem(o => {
                try {
                    var webClient = new WebClient();
                    var buffer = webClient.DownloadData(uri);
                    var bitmapImage = new BitmapImage();

                    using (var stream = new MemoryStream(buffer)) {

                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();

                    }

                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        listener(bitmapImage);
                    }));
                } catch (Exception e) {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }
    }
}
