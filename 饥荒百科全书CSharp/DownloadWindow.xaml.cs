using System;
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
        public DownloadWindow()
        {
            InitializeComponent();
            DownloadVersion.Text = "新版本 " + MainWindow.updatePan.NewVersion + " 下载地址";
            DownloadURL_TextBlock.Text = MainWindow.updatePan.DownloadURL;
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
            Process.Start(MainWindow.updatePan.DownloadURL);
        }

        private void Window_Unloaded(Object sender, RoutedEventArgs e)
        {
            MainWindow.MWVisivility = true;
        }
    }
}
