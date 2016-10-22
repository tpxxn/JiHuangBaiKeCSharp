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
using System.Windows.Forms;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// SplashScreen.xaml 的交互逻辑
    /// </summary>
    public partial class SplashScreen : Window
    {
        private Timer splashTimer = new Timer() { };

        public SplashScreen()
        {
            InitializeComponent();

            splashTimer.Interval = 1;
            splashTimer.Tick += new EventHandler(splashStop);
            splashTimer.Start();
        }

        void splashStop(object sender, EventArgs e)
        {
            splashTimer.Enabled = false;
            var MainWindowShow = new MainWindow();
            MainWindowShow.InitializeComponent();
            MainWindowShow.Show();
            Close();
        }

    }
}
