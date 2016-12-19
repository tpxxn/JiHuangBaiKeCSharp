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
        private Timer splashTimer = new Timer() { };

        public SplashScreen()
        {
            InitializeComponent();
            splashTimer.Interval = 1;
            splashTimer.Tick += new EventHandler(splashStop);
            splashTimer.Start();
        }
        //todo:建议是MainWindows 读取
        void splashStop(object sender, EventArgs e)
        {
            splashTimer.Enabled = false;
            MainWindow MainWindowShow = new MainWindow();
            MainWindowShow.InitializeComponent();
            MainWindowShow.Show();
            MainWindowShow.Activate();
            Close();
        }

    }
}
