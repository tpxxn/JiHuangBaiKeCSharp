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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 饥荒百科全书CSharp.Class;
using Application = System.Windows.Application;
using Control = System.Windows.Forms.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace 饥荒百科全书CSharp.View.SettingChildPage
{
    /// <summary>
    /// SettingChildPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingChildPage : Page
    {
        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            RootScrollViewer.FontWeight = Global.FontWeight;
        }

        public SettingChildPage()
        {
            InitializeComponent();
            Global.SettingRootFrame.NavigationService.LoadCompleted += LoadCompleted;
            // 加载设置
            LoadSetting();
        }

        /// <summary>
        /// 加载设置
        /// </summary>
        private void LoadSetting()
        {
            // 读取BOSSKEY设置
            var hotkeyBossKeyControlKeys = RegeditRw.RegRead("HotkeyBossKeyControlKeys");
            var hotkeyBossKeyMainKey = RegeditRw.RegRead("HotkeyBossKeyMainKey");
            if (hotkeyBossKeyControlKeys == 0 && hotkeyBossKeyMainKey == 0)
            {
                hotkeyBossKeyControlKeys = 3; //Ctrl+Alt
                hotkeyBossKeyMainKey = 66; //B
            }
            var controlKeysString = ControlKeyToString(hotkeyBossKeyControlKeys);
            var mainKey = DoubleToKey(hotkeyBossKeyMainKey);
            var mainKeyString = "";
            if ((mainKey >= Key.A && mainKey <= Key.Z) || (mainKey >= Key.F1 && mainKey <= Key.F12) || (mainKey >= Key.NumPad0 && mainKey <= Key.NumPad9) || (mainKey == Key.Space))
            {
                mainKeyString = mainKey.ToString();
            }
            if (mainKey >= Key.D0 && mainKey <= Key.D9)
            {
                mainKeyString = mainKey.ToString().Replace("D", "");
            }
            SeBossKeyKey.Content = controlKeysString + mainKeyString;
            // 读取点击关闭按钮设置
            if (string.IsNullOrEmpty(RegeditRw.RegReadString("HideToNotifyIconPrompt")))
            {
                HideToNotifyIconRadioButton.IsChecked = true;
            }
            else
            {
                if (Settings.HideToNotifyIcon)
                {
                    HideToNotifyIconRadioButton.IsChecked = true;
                }
                else
                {
                    ExitRadioButton.IsChecked = true;
                }
            }
        }

        private static string ControlKeyToString(double controlKeyValue)
        {
            switch (controlKeyValue)
            {
                case 1:
                    return "Alt + ";
                case 2:
                    return "Ctrl + ";
                case 3:
                    return "Ctrl + Alt + ";
                case 4:
                    return "Shift + ";
                case 5:
                    return "Alt + Shift + ";
                case 6:
                    return "Ctrl + Shift + ";
                case 7:
                    return "Ctrl + Alt + Shift + ";
                default:
                    return "";
            }
        }

        private static int KeyToInt(Key key)
        {
            if (key >= Key.A && key <= Key.Z)
                return (int)key + 21;
            if (key >= Key.D0 && key <= Key.D9)
                return (int)key + 14;
            if (key >= Key.F1 && key <= Key.F12)
                return (int)key + 22;
            if (key >= Key.NumPad0 && key <= Key.NumPad9)
                return (int)key + 22;
            return 0;
        }

        private static Key DoubleToKey(double keyValue)
        {
            if (keyValue >= 65 && keyValue <= 90)
                return (Key)(keyValue - 21);
            if (keyValue >= 48 && keyValue <= 57)
                return (Key)(keyValue - 14);
            if (keyValue >= 112 && keyValue <= 123)
                return (Key)(keyValue - 22);
            if (keyValue >= 96 && keyValue <= 105)
                return (Key)(keyValue - 22);
            return Key.B;
        }

        //老板键
        private void Se_BossKey_Key_KeyDown(object sender, KeyEventArgs e)
        {
            var mainKey = ""; //主键值(文本)
            //字母 || F1-F12 || 小键盘区的数字 || 空格?
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.F1 && e.Key <= Key.F12) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Space))
            {
                mainKey = e.Key.ToString();
            }
            //字母区上面的数字
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                mainKey = e.Key.ToString().Replace("D", "");
            }
            //Alt Ctrl Shift键判断
            var pressAlt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt ? (byte)1 : (byte)0;
            var pressCtrl = (Control.ModifierKeys & Keys.Control) == Keys.Control ? (byte)2 : (byte)0;
            var pressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift ? (byte)4 : (byte)0;
            var controlKeys = pressAlt + pressCtrl + pressShift;
            var controlKeysString = ControlKeyToString(controlKeys);
            e.Handled = true;
            // 主键值
            var mainKeyValue = KeyToInt(e.Key);
            //输出值
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (!string.IsNullOrEmpty(mainKey))
            {
                SeBossKeyKey.Content = controlKeysString + mainKey;
                KeyboardHandler.UnregisterHotKey(mainWindow.intPtr, mainWindow.KeyboardHandler.GetType().GetHashCode());
                mainWindow.KeyboardHandler.SetupHotKey(mainWindow.intPtr, (Global.KeyModifiers)controlKeys, mainKeyValue);
                RegeditRw.RegWrite("HotkeyBossKeyControlKeys", controlKeys);
                RegeditRw.RegWrite("HotkeyBossKeyMainKey", mainKeyValue);
                var copySplashWindow = new CopySplashScreen("已保存");
                copySplashWindow.InitializeComponent();
                copySplashWindow.Show();
            }
        }

        private void NotifyIconRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (HideToNotifyIconRadioButton.IsChecked == true)
            {
                Settings.HideToNotifyIcon = true;
                RegeditRw.RegWrite("HideToNotifyIcon", "True");
            }
            else if (ExitRadioButton.IsChecked == true)
            {
                Settings.HideToNotifyIcon = false;
                RegeditRw.RegWrite("HideToNotifyIcon", "False");
            }
            var copySplashWindow = new CopySplashScreen("已保存");
            copySplashWindow.InitializeComponent();
            copySplashWindow.Show();
        }
    }
}
