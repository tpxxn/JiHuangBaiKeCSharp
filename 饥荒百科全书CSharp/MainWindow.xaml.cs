//控件计数例子
//int sum = 0;
//foreach (Control vControl in WrapPanel_Right.Children)
//{
//    if (vControl is ButtonWithText)
//    {
//        sum += 1;
//    }
//}

//#region "删除旧版本文件"
//string oldVersionPath = RegeditRW.RegReadString("OldVersionPath");
//if (oldVersionPath != System.Windows.Forms.Application.ExecutablePath && oldVersionPath != "")
//{
//    try
//    {
//        File.Delete(oldVersionPath);
//    }
//    catch (Exception ex)
//    {
//        System.Windows.Forms.MessageBox.Show("删除旧版本错误，请手动删除：" + ex);
//    }
//}
//RegeditRW.RegWrite("OldVersionPath", "");
//#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using Control = System.Windows.Forms.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow主类
    /// </summary>
    public partial class MainWindow : Window
    {
        //检查更新实例 update(网盘)
        public static UpdatePan UpdatePan = new UpdatePan();

        #region "窗口可视化属性"

        private readonly Timer _visiTimer = new Timer();

        public static bool MwVisivility { get; set; }

        private void VisiTimerEvent(object sender, EventArgs e)
        {
            Visibility = MwVisivility ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        #region "窗口是否初始化属性"

        public static bool MwInit { get; set; }

        #endregion

        public static bool LoadFont;
        #region "MainWindow"
        //MainWindow构造函数
        public MainWindow()
        {
            #region "读取注册表(必须在初始化之前读取)"
            //背景图片
            string bg = RegeditRw.RegReadString("Background");
            double bgStretch = RegeditRw.RegRead("BackgroundStretch");
            //透明度
            double bgAlpha = RegeditRw.RegRead("BGAlpha");
            double bgPanelAlpha = RegeditRw.RegRead("BGPanelAlpha");
            double windowAlpha = RegeditRw.RegRead("WindowAlpha");
            //窗口大小
            double mainWindowHeight = RegeditRw.RegRead("MainWindowHeight");
            double mainWindowWidth = RegeditRw.RegRead("MainWindowWidth");
            //字体
            string mainWindowFont = RegeditRw.RegReadString("MainWindowFont");
            //设置菜单
            double winTopmost = RegeditRw.RegRead("Topmost");
            //游戏版本
            double gameVersion = RegeditRw.RegRead("GameVersion");
            #endregion
            //初始化
            InitializeComponent();
            //窗口缩放
            SourceInitialized += delegate (object sender, EventArgs e) { _hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource; };
            MouseMove += Window_MouseMove;
            //mainWindow初始化标志
            MwInit = true;
            #region "读取设置"
            //设置字体
            if (string.IsNullOrEmpty(mainWindowFont))
            {
                RegeditRw.RegWrite("MainWindowFont", "微软雅黑");
                mainWindowFont = "微软雅黑";
            }
            mainWindow.FontFamily = new FontFamily(mainWindowFont);
            //版本初始化
            UiVersion.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version;
            //窗口可视化计时器
            _visiTimer.Enabled = true;
            _visiTimer.Interval = 200;
            _visiTimer.Tick += VisiTimerEvent;
            _visiTimer.Start();
            //设置光标资源字典路径
            CursorDictionary.Source = new Uri("Dictionary/CursorDictionary.xaml", UriKind.Relative);
            //显示窗口
            MwVisivility = true;
            //右侧面板Visibility属性初始化
            RightFrame.Navigate(new Uri("../View/WelcomePage.xaml", UriKind.Relative));
            //窗口置顶
            if (winTopmost == 1)
            {
                Se_button_Topmost_Click(null, null);
            }
            //设置背景
            if (bg == "")
            {
                SeBgAlphaText.Foreground = Brushes.Silver;
                SeBgAlpha.IsEnabled = false;
            }
            else
            {
                SeBgAlphaText.Foreground = Brushes.Black;
                try
                {
                    var brush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(bg))
                    };
                    UiBackGroundBorder.Background = brush;
                }
                catch
                {
                    Visi.VisiCol(true, UiBackGroundBorder);
                }
            }
            //设置背景拉伸方式
            if (bgStretch == 0)
            {
                bgStretch = 2;
            }
            SeComboBoxBackgroundStretch.SelectedIndex = (int)bgStretch - 1;
            //设置背景透明度
            if (bgAlpha == 0)
            {
                bgAlpha = 101;
            }
            SeBgAlpha.Value = bgAlpha - 1;
            SeBgAlphaText.Text = "背景不透明度：" + (int)SeBgAlpha.Value + "%";
            UiBackGroundBorder.Opacity = (bgAlpha - 1) / 100;
            //设置面板透明度
            if (bgPanelAlpha == 0)
            {
                bgPanelAlpha = 61;
            }
            SePanelAlpha.Value = bgPanelAlpha - 1;
            SePanelAlphaText.Text = "面板不透明度：" + (int)SePanelAlpha.Value + "%";
            RightGrid.Background.Opacity = (bgPanelAlpha - 1) / 100;
            //设置窗口透明度
            if (windowAlpha == 0)
            {
                windowAlpha = 101;
            }
            SeWindowAlpha.Value = windowAlpha - 1;
            SeWindowAlphaText.Text = "窗口不透明度：" + (int)SeWindowAlpha.Value + "%";
            Opacity = (windowAlpha - 1) / 100;
            //设置高度和宽度
            Width = mainWindowWidth;
            Height = mainWindowHeight;
            //设置游戏版本
            UiGameversion.SelectedIndex = (int)gameVersion;
            #endregion
        }
        //MainWindow窗口加载
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var _ = new KeyboardHandler(this);
            foreach (var str in Rf())//加载字体
            {
                var tb = new TextBlock
                {
                    Text = str,
                    FontFamily = new FontFamily(str)
                };
                SeComboBoxFont.Items.Add(tb);
            }
            var mainWindowFont = RegeditRw.RegReadString("MainWindowFont");
            var ls = new List<string>();
            foreach (TextBlock tb in SeComboBoxFont.Items)
            {
                ls.Add(tb.Text);
            }
            SeComboBoxFont.SelectedIndex = ls.IndexOf(mainWindowFont);
            LoadFont = true;
            LoadGameVersionXml();//加载游戏版本Xml文件
            DediButtomPanelInitalize();//服务器面板初始化
        }
        #endregion

        #region "设置面板"
        //老板键
        private void Se_BossKey_Key_KeyDown(Object sender, KeyEventArgs e)
        {
            byte pressAlt; //Alt
            byte pressCtrl; //Ctrl
            byte pressShift; //Shift
            string preString; //前面的值
            var mainKey = ""; //主值

            //字母 || F1-F12 || 小键盘区的数字 || 空格?
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.F1 && e.Key <= Key.F12) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Space))
            {
                e.Handled = true;
                mainKey = e.Key.ToString();
            }
            //字母区上面的数字
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = true;
                mainKey = e.Key.ToString().Replace("D", "");
            }
            //Alt Ctrl Shift键判断
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                pressAlt = 1;
            else
                pressAlt = 0;

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                pressCtrl = 2;
            else
                pressCtrl = 0;

            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                pressShift = 4;
            else
                pressShift = 0;

            var controlKeys = pressAlt + pressCtrl + pressShift;
            switch (controlKeys)
            {
                case 1:
                    preString = "Alt + ";
                    break;
                case 2:
                    preString = "Ctrl + ";
                    break;
                case 3:
                    preString = "Ctrl + Alt + ";
                    break;
                case 4:
                    preString = "Shift + ";
                    break;
                case 5:
                    preString = "Alt + Shift + ";
                    break;
                case 6:
                    preString = "Ctrl + Shift + ";
                    break;
                case 7:
                    preString = "Ctrl + Alt + Shift + ";
                    break;
                default:
                    preString = "";
                    break;
            }
            //输出值
            if (mainKey != "")
            {
                SeBossKeyKey.Content = preString + mainKey;
            }
            else
            {
                SeBossKeyKey.Content = "Ctrl + Alt + B";
            }
        }
        #endregion
    }
}