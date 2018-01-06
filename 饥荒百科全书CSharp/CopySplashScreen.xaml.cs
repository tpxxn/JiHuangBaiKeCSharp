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
using 饥荒百科全书CSharp.Class;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// CopySplashScreen.xaml 的交互逻辑
    /// </summary>
    public partial class CopySplashScreen : Window
    {
        private readonly Timer _splashTimer = new Timer();
        private int _flag;

        public CopySplashScreen()
        {
            InitializeComponent();
            if (Global.FontFamily != null)
                FontFamily = Global.FontFamily;
            _splashTimer.Interval = 1;
            _splashTimer.Tick += SplashTick;
            _splashTimer.Start();
        }

        private void SplashTick(object sender, EventArgs e)
        {
            var mainWindow = Application.Current.MainWindow;
            // ReSharper disable once PossibleNullReferenceException
            Left = mainWindow.Left + mainWindow.Width / 2 - Width / 2;
            Top = mainWindow.Top + mainWindow.Height / 2 - Height / 2;
            if (_flag < 33)
            {
                Opacity += 0.03;
            }
            if (_flag > 66)
            {
                Opacity -= 0.03;
            }
            _flag += 1;
            if (_flag < 99) return;
            _splashTimer.Enabled = false;
            Close();
        }
    }
}
