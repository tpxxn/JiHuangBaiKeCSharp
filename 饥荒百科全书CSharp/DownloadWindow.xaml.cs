using System;
using System.Collections.Generic;
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
            UpdateTimer.Enabled = true;
            UpdateTimer.Interval = 200;
            UpdateTimer.Tick += new EventHandler(Update);
            UpdateTimer.Start();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        Timer UpdateTimer = new Timer();
        
        void Update(object sender, EventArgs e)
        {
            if (MainWindow.update.Downloadcompleted == false)
            {
                DownloadSpeed.Text = MainWindow.update.DownloadSpeed;
                DownloadProgressBar.Value = MainWindow.update.DownloadProgress;
                Downloaded.Text = MainWindow.update.Downloaded;
            }
            else
            {
                UpdateTimer.Enabled = false;
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("取消更新？", "检查更新", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                MainWindow.update.DownloadCancel();
                MainWindow.MWVisivility = true;
                Close();
            }
        }
    }
}
