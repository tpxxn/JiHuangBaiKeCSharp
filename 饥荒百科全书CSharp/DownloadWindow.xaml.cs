using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 饥荒百科全书CSharp;

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
                MainWindow.MWVisivility = true;
                Close();
        }

        private void DownloadURL_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(MainWindow.updatePan.DownloadURL);
        }
    }
}
