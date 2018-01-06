using System;
using System.Windows;
using System.Windows.Forms;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// SplashScreen.xaml 的交互逻辑
    /// </summary>
    public partial class SplashScreen : Window
    {
        private readonly Timer _splashTimer = new Timer();

        public SplashScreen()
        {
            InitializeComponent();
            _splashTimer.Interval = 1;
            _splashTimer.Tick += SplashStop;
            _splashTimer.Start();
        }

        private void SplashStop(object sender, EventArgs e)
        {
            _splashTimer.Enabled = false;
            var mainWindowShow = new MainWindow();
            mainWindowShow.InitializeComponent();
            mainWindowShow.Show();
            mainWindowShow.Activate();
            Close();
        }
    }
}
