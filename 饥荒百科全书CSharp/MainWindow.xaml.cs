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
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow主类
    /// </summary>
    public partial class MainWindow : Window
    {
        
        #region "颜色常量"
        public const string PBCGreen = "5EB660";  //绿色
        public const string PBCKhaki = "EDB660";  //卡其布色/土黄色
        public const string PBCBlue = "337AB8";   //蓝色
        public const string PBCCyan = "15E3EA";   //青色
        public const string PBCOrange = "F6A60B"; //橙色
        public const string PBCPink = "F085D3";   //粉色
        public const string PBCYellow = "EEE815"; //黄色
        public const string PBCRed = "D8524F";    //红色
        public const string PBCPurple = "A285F0"; //紫色
        #endregion

        //检查更新实例 update(网盘)
        public static UpdatePan updatePan = new UpdatePan();

        #region "窗口可视化属性"
        Timer VisiTimer = new Timer();

        public static bool mWVisivility;
        public static bool MWVisivility
        {
            get
            {
                return mWVisivility;
            }
            set
            {
                mWVisivility = value;
            }
        }

        void VisiTimerEvent(object sender, EventArgs e)
        {
            if (MWVisivility == true)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region "窗口是否初始化属性"
        public static bool mWInit = false;
        public static bool MWInit
        {
            get
            {
                return mWInit;
            }
            set
            {
                mWInit = value;
            }
        }
        #endregion

        public static bool loadFont = false;
        #region "MainWindow"
        //MainWindow构造函数
        public MainWindow()
        {
            //读取注册表(必须在初始化之前读取)
            ////背景图片
            string bg = RegeditRW.RegReadString("Background");
            double bgStretch = RegeditRW.RegRead("BackgroundStretch");
            ////透明度
            double bgAlpha = RegeditRW.RegRead("BGAlpha");
            double bgPanelAlpha = RegeditRW.RegRead("BGPanelAlpha");
            double windowAlpha = RegeditRW.RegRead("WindowAlpha");
            ////窗口大小
            double mainWindowHeight = RegeditRW.RegRead("MainWindowHeight");
            double mainWindowWidth = RegeditRW.RegRead("MainWindowWidth");
            //字体
            string mainWindowFont = RegeditRW.RegReadString("MainWindowFont");
            //设置菜单
            double winTopmost = RegeditRW.RegRead("Topmost");
            ////游戏版本
            double gameVersion = RegeditRW.RegRead("GameVersion");
            //初始化
            InitializeComponent();
            //窗口缩放
            SourceInitialized += delegate (object sender, EventArgs e){ _HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;};
            MouseMove += new System.Windows.Input.MouseEventHandler(Window_MouseMove);
            //mainWindow初始化标志
            MWInit = true;
            //设置字体
            if (mainWindowFont == "" || mainWindowFont == null)
            {
                RegeditRW.RegWrite("MainWindowFont", "微软雅黑");
                mainWindowFont = "微软雅黑";
            }
            mainWindow.FontFamily = new FontFamily(mainWindowFont);
            //版本初始化
            UI_Version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //窗口可视化计时器
            VisiTimer.Enabled = true;
            VisiTimer.Interval = 200;
            VisiTimer.Tick += new EventHandler(VisiTimerEvent);
            VisiTimer.Start();
            //设置光标资源字典路径
            cursorDictionary.Source = new Uri("Dictionary/CursorDictionary.xaml", UriKind.Relative);
            //显示窗口
            MWVisivility = true;
            //右侧面板Visibility属性初始化
            RightPanelVisibility("Welcome");

            //窗口置顶
            if (winTopmost == 1)
            {
                Se_button_Topmost_Click(null, null);
            }
            //设置背景
            if (bg == "")
            {
                Se_BG_Alpha_Text.Foreground = Brushes.Silver;
                Se_BG_Alpha.IsEnabled = false;
            }
            else
            {
                Se_BG_Alpha_Text.Foreground = Brushes.Black;
                try
                {
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(bg));
                    UI_BackGroundBorder.Background = brush;
                }
                catch
                {
                    Visi.VisiCol(true, UI_BackGroundBorder);
                }
            }
            //设置背景拉伸方式
            if (bgStretch == 0)
            {
                bgStretch = 2;
            }
            Se_ComboBox_Background_Stretch.SelectedIndex = (int)bgStretch - 1;
            //设置背景透明度
            if (bgAlpha == 0)
            {
                bgAlpha = 101;
            }
            Se_BG_Alpha.Value = bgAlpha - 1;
            Se_BG_Alpha_Text.Text = "背景不透明度：" + (int)Se_BG_Alpha.Value + "%";
            UI_BackGroundBorder.Opacity = (bgAlpha - 1) / 100;
            //设置面板透明度
            if (bgPanelAlpha == 0)
            {
                bgPanelAlpha = 61;
            }
            Se_Panel_Alpha.Value = bgPanelAlpha - 1;
            Se_Panel_Alpha_Text.Text = "面板不透明度：" + (int)Se_Panel_Alpha.Value + "%";
            RightGrid.Background.Opacity = (bgPanelAlpha - 1) / 100;
            //设置窗口透明度
            if (windowAlpha == 0)
            {
                windowAlpha = 101;
            }
            Se_Window_Alpha.Value = windowAlpha - 1;
            Se_Window_Alpha_Text.Text = "窗口不透明度：" + (int)Se_Window_Alpha.Value + "%";
            Opacity = (windowAlpha - 1) / 100;
            //设置高度和宽度
            Width = mainWindowWidth;
            Height = mainWindowHeight;
            //设置游戏版本
            UI_gameversion.SelectedIndex = (int)gameVersion;
        }
        //MainWindow窗口加载
        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new KeyboardHandler(this);//加载快捷键
            foreach (string str in rF())//加载字体
            {
                TextBlock TB = new TextBlock();
                TB.Text = str;
                TB.FontFamily = new FontFamily(str);
                Se_ComboBox_Font.Items.Add(TB);
            }
            string mainWindowFont = RegeditRW.RegReadString("MainWindowFont");
            List<string> Ls = new List<string>();
            foreach (TextBlock TB in Se_ComboBox_Font.Items)
            {
                Ls.Add(TB.Text);
            }
            Se_ComboBox_Font.SelectedIndex = Ls.IndexOf(mainWindowFont);
            loadFont = true;
            LoadGameVersionXml();//加载游戏版本Xml文件            
        }
        #endregion

        #region "主页面板"
        //官网
        private void W_button_official_website_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jihuangbaike.com");
        }
        //Mod
        private void W_button_Mods_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=635215011");
        }
        //DS新闻
        private void W_button_DSNews_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=219740");
        }
        //DST新闻
        private void W_button_DSTNewS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=322330");
        }
        //群二维码
        private void W_button_QRCode_Qun_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("tencent://groupwpa/?subcmd=all&param=7B2267726F757055696E223A3538303333323236382C2274696D655374616D70223A313437303132323238337D0A");
        }
        #endregion

        #region "设置面板"
        //老板键
        private void Se_BossKey_Key_KeyDown(Object sender, System.Windows.Input.KeyEventArgs e)
        {
            byte PressAlt = 0; //Alt
            byte PressCtrl = 0; //Ctrl
            byte PressShift = 0; //Shift
            int ControlKeys = 0; //Alt + Ctrl + Shift的值
            string PreString = ""; //前面的值
            string MainKey = ""; //主值

            //字母 || F1-F12 || 小键盘区的数字 || 空格?
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.F1 && e.Key <= Key.F12) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Space))
            {
                e.Handled = true;
                MainKey = e.Key.ToString();
            }
            //字母区上面的数字
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = true;
                MainKey = e.Key.ToString().Replace("D", "");
            }
            //Alt Ctrl Shift键判断
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                PressAlt = 1;
            else
                PressAlt = 0;

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control)
                PressCtrl = 2;
            else
                PressCtrl = 0;

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                PressShift = 4;
            else
                PressShift = 0;

            ControlKeys = PressAlt + PressCtrl + PressShift;
            switch (ControlKeys)
            {
                case 1:
                    PreString = "Alt + ";
                    break;
                case 2:
                    PreString = "Ctrl + ";
                    break;
                case 3:
                    PreString = "Ctrl + Alt + ";
                    break;
                case 4:
                    PreString = "Shift + ";
                    break;
                case 5:
                    PreString = "Alt + Shift + ";
                    break;
                case 6:
                    PreString = "Ctrl + Shift + ";
                    break;
                case 7:
                    PreString = "Ctrl + Alt + Shift + ";
                    break;
                default:
                    PreString = "";
                    break;
            }
            //输出值
            if (MainKey != "")
            {
                Se_BossKey_Key.Content = PreString + MainKey;
            }
            else
            {
                Se_BossKey_Key.Content = "Ctrl + Alt + B";
            }
        }
        #endregion

    }
}