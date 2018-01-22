using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// DownloadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public DownloadWindow(bool newVersion)
        {
            InitializeComponent();
            if (newVersion)
            {
                DownloadVersion.Text = "检测到新版本 " + MainWindow.UpdatePan.NewVersion;
                DownloadURL_TextBlock.Text = "前往百度网盘下载";
            }
            //else
            //{
            //    DownloadVersion.Text = "您正在使用最新版本";
            //}
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DownloadURL_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(MainWindow.UpdatePan.DownloadUrl);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            // ReSharper disable once PossibleNullReferenceException
            mainWindow.MwVisibility = true;
        }
    }
}
