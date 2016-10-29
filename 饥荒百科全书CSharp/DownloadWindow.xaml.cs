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

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// DownloadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadWindow : Window
    {
        private Timer UpdateTimer = new Timer();
        
        public DownloadWindow()
        {
            InitializeComponent();
            UpdateTimer.Interval = 200;
            UpdateTimer.Tick += new EventHandler(Update);
            UpdateTimer.Start();    
        }

        void Update(object sender, EventArgs e)
        {
            DownloadSpeed.Text = MainWindow.update.DownloadSpeed;
            DownloadProgressBar.Value= MainWindow.update.DownloadProgress;
            Downloaded.Text = MainWindow.update.Downloaded;
        }

        /// <summary>
        /// 拖动窗口
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
