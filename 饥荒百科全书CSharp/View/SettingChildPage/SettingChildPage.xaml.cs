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
            // 读取BossKey设置
            var hotkeyBossKeyControlKeys = RegeditRw.RegRead("HotkeyBossKeyControlKeys");
            var hotkeyBossKeyMainKey = RegeditRw.RegRead("HotkeyBossKeyMainKey");
            if (hotkeyBossKeyControlKeys == 0 && hotkeyBossKeyMainKey == 0)
            {
                hotkeyBossKeyControlKeys = 3; //Ctrl + Alt
                hotkeyBossKeyMainKey = 0x57; //W
            }
            var BossKeyControlKeysString = ControlKeyToString(hotkeyBossKeyControlKeys);
            var BossKeyMainKey = DoubleToKey(hotkeyBossKeyMainKey);
            var BossKeyMainKeyString = KeyToString(BossKeyMainKey);
            SeBossKeyKey.Content = BossKeyControlKeysString + BossKeyMainKeyString;
            // 读取ConsoleKey设置
            var hotkeyConsoleKeyControlKeys = RegeditRw.RegRead("HotkeyConsoleKeyControlKeys");
            var hotkeyConsoleKeyMainKey = RegeditRw.RegRead("HotkeyConsoleKeyMainKey");
            if (hotkeyConsoleKeyControlKeys == 0 && hotkeyConsoleKeyMainKey == 0)
            {
                hotkeyConsoleKeyControlKeys = 0;
                hotkeyConsoleKeyMainKey = 0x71; // F2
            }
            var ConsoleKeyControlKeysString = ControlKeyToString(hotkeyConsoleKeyControlKeys);
            var ConsoleKeyMainKey = DoubleToKey(hotkeyConsoleKeyMainKey);
            var ConsoleKeyMainKeyString = KeyToString(ConsoleKeyMainKey);
            SeConsoleKeyKey.Content = ConsoleKeyControlKeysString + ConsoleKeyMainKeyString;
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

        private static string KeyToString(Key key)
        {
            //字母 || F1-F12 || 小键盘区的数字 || 空格?
            if ((key >= Key.A && key <= Key.Z) || (key >= Key.F1 && key <= Key.F12) || (key >= Key.NumPad0 && key <= Key.NumPad9) || (key == Key.Space))
            {
                return key.ToString();
            }
            //字母区上面的数字
            if (key >= Key.D0 && key <= Key.D9)
            {
                return key.ToString().Replace("D", "");
            }
            return "";
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

        /// <summary>
        /// 老板键
        /// </summary>
        private void Se_BossKey_Key_KeyDown(object sender, KeyEventArgs e)
        {
            //主键值(文本)
            var mainKey = KeyToString(e.Key);
            //Alt Ctrl Shift键判断
            var pressAlt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt ? (byte)1 : (byte)0;
            var pressCtrl = (Control.ModifierKeys & Keys.Control) == Keys.Control ? (byte)2 : (byte)0;
            var pressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift ? (byte)4 : (byte)0;
            var controlKeys = pressAlt + pressCtrl + pressShift;
            var controlKeysString = ControlKeyToString(controlKeys);
            e.Handled = true;
            // 主键值
            var mainKeyValue = KeyToInt(e.Key);
            // 输出值
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (!string.IsNullOrEmpty(mainKey))
            {
                SeBossKeyKey.Content = controlKeysString + mainKey;
                var hotkeyBossKeyControlKeys = RegeditRw.RegRead("HotkeyBossKeyControlKeys");
                var hotkeyBossKeyMainKey = RegeditRw.RegRead("HotkeyBossKeyMainKey");
                mainWindow.BossKeyHotKey.UnSetupHotKey(mainWindow.intPtr, (int)hotkeyBossKeyControlKeys + (int)hotkeyBossKeyMainKey * 10);
                mainWindow.BossKeyHotKey.SetupHotKey(mainWindow, (Global.KeyModifiers)controlKeys, (Keys)mainKeyValue);
                RegeditRw.RegWrite("HotkeyBossKeyControlKeys", controlKeys);
                RegeditRw.RegWrite("HotkeyBossKeyMainKey", mainKeyValue);
                var copySplashWindow = new CopySplashScreen("已保存");
                copySplashWindow.InitializeComponent();
                copySplashWindow.Show();
            }
        }

        /// <summary>
        /// 控制台快捷键
        /// </summary>
        private void Se_ConsoleKey_Key_KeyDown(object sender, KeyEventArgs e)
        {
            //主键值(文本)
            var mainKey = KeyToString(e.Key);
            //Alt Ctrl Shift键判断
            var pressAlt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt ? (byte)1 : (byte)0;
            var pressCtrl = (Control.ModifierKeys & Keys.Control) == Keys.Control ? (byte)2 : (byte)0;
            var pressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift ? (byte)4 : (byte)0;
            var controlKeys = pressAlt + pressCtrl + pressShift;
            var controlKeysString = ControlKeyToString(controlKeys);
            e.Handled = true;
            // 主键值
            var mainKeyValue = KeyToInt(e.Key);
            // 输出值
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (!string.IsNullOrEmpty(mainKey))
            {
                SeConsoleKeyKey.Content = controlKeysString + mainKey;
                var hotkeyConsoleKeyControlKeys = RegeditRw.RegRead("HotkeyConsoleKeyControlKeys");
                var hotkeyConsoleKeyMainKey = RegeditRw.RegRead("HotkeyConsoleKeyMainKey");
                mainWindow.BossKeyHotKey.UnSetupHotKey(mainWindow.intPtr, (int)hotkeyConsoleKeyControlKeys + (int)hotkeyConsoleKeyMainKey * 10);
                mainWindow.ConsoleKeyHotKey.SetupHotKey(mainWindow, (Global.KeyModifiers)controlKeys, (Keys)mainKeyValue);
                RegeditRw.RegWrite("HotkeyConsoleKeyControlKeys", controlKeys);
                RegeditRw.RegWrite("HotkeyConsoleKeyMainKey", mainKeyValue);
                var copySplashWindow = new CopySplashScreen("已保存");
                copySplashWindow.InitializeComponent();
                copySplashWindow.Show();
            }
        }

        /// <summary>
        /// 是否隐藏到托盘区
        /// </summary>
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
