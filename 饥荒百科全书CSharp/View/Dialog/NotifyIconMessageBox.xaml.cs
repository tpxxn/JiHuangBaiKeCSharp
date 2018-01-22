using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 饥荒百科全书CSharp.Class;

namespace 饥荒百科全书CSharp.View.Dialog
{
    /// <summary>
    /// NotifyIconMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyIconMessageBox : Window
    {
        public NotifyIconMessageBox()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (HideToNotifyIconRadioButton.IsChecked == true)
            {
                mainWindow.MwVisibility = false;
                mainWindow.NotifyIcon.ShowBalloonTip(1000);
                Settings.HideToNotifyIcon = true;
                RegeditRw.RegWrite("HideToNotifyIcon", "True");
            }
            else if (ExitRadioButton.IsChecked == true)
            {
                Settings.HideToNotifyIcon = false;
                RegeditRw.RegWrite("HideToNotifyIcon", "False");
                mainWindow.NotifyIcon.Visible = false;
                mainWindow.NotifyIcon.Dispose();
                Application.Current.Shutdown();
            }
            if (DoNotShowAgain.IsChecked == true)
            {
                Settings.HideToNotifyIconPrompt = true;
                RegeditRw.RegWrite("HideToNotifyIconPrompt", "True");
            }
            else
            {
                RegeditRw.RegWrite("HideToNotifyIconPrompt", "False");
            }
            Close();
        }
    }
}
