using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.View;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow主类
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 字段/属性
        /// <summary>
        /// 检查更新实例 update(网盘)
        /// </summary>
        public static UpdatePan UpdatePan = new UpdatePan();

        #region 注册快捷键
        /// <summary>
        /// 老板键
        /// </summary>
        public HotKey BossKeyHotKey;

        /// <summary>
        /// 控制台快捷键
        /// </summary>
        public HotKey ConsoleKeyHotKey;

        /// <summary>
        /// mainWindow句柄
        /// </summary>
        public IntPtr intPtr;
        #endregion

        #region "窗口可视化属性"
        private readonly Timer _visiTimer = new Timer();

        public bool MwVisibility { get; set; }

        private void VisiTimerEvent(object sender, EventArgs e)
        {
            Visibility = MwVisibility ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        /// <summary>
        /// 窗口是否初始化
        /// </summary>
        public static bool MwInit { get; set; }

        /// <summary>
        /// 是否加载字体
        /// </summary>
        public static bool LoadFont;
        #endregion

        #region "MainWindow"
        /// <summary>
        /// MainWindow构造函数
        /// </summary>
        public MainWindow()
        {
            Application.Current.MainWindow = this;
            #region "读取注册表(必须在初始化之前读取)"
            // 背景图片
            var bg = RegeditRw.RegReadString("Background");
            var bgStretch = RegeditRw.RegRead("BackgroundStretch");
            // 透明度
            var bgAlpha = RegeditRw.RegRead("BGAlpha");
            var bgPanelAlpha = RegeditRw.RegRead("BGPanelAlpha");
            var windowAlpha = RegeditRw.RegRead("WindowAlpha");
            // 窗口大小
            var mainWindowHeight = RegeditRw.RegRead("MainWindowHeight");
            var mainWindowWidth = RegeditRw.RegRead("MainWindowWidth");
            // 字体
            var mainWindowFont = RegeditRw.RegReadString("MainWindowFont");
            var mainWindowFontWeight = RegeditRw.RegReadString("MainWindowFontWeight");
            // 淡紫色透明光标
            var mainWindowLavenderCursor = RegeditRw.RegReadString("MainWindowLavenderCursor");
            // 设置菜单
            var winTopmost = RegeditRw.RegRead("Topmost");
            // 游戏版本
            var gameVersion = RegeditRw.RegRead("GameVersion");
            // 设置
            Settings.HideToNotifyIcon = RegeditRw.RegReadString("HideToNotifyIcon") == "True";
            Settings.HideToNotifyIconPrompt = RegeditRw.RegReadString("HideToNotifyIconPrompt") == "True";
            Settings.SmallButtonMode = RegeditRw.RegReadString("SmallButtonMode") == "True";
            #endregion
            // 初始化
            InitializeComponent();
            // 窗口缩放
            SourceInitialized += delegate (object sender, EventArgs e) { _hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource; };
            MouseMove += Window_MouseMove;
            // mainWindow初始化标志
            MwInit = true;
            // RightFrame
            Global.RightFrame = RightFrame;
            // MainPageHamburgerButton
            for (var i = 1; i < LeftWrapPanel.Children.Count; i++)
            {
                Global.MainPageHamburgerButton.Add((System.Windows.Controls.RadioButton)LeftWrapPanel.Children[i]);
            }
            #region "读取设置"
            // 设置字体
            if (string.IsNullOrEmpty(mainWindowFont))
            {
                RegeditRw.RegWrite("MainWindowFont", "微软雅黑");
                mainWindowFont = "微软雅黑";
            }
            mainWindow.FontFamily = new FontFamily(mainWindowFont);
            ((TextBlock)((VisualBrush)FindResource("HelpBrush")).Visual).FontFamily = mainWindow.FontFamily;
            // 设置字体加粗
            if (string.IsNullOrEmpty(mainWindowFontWeight))
            {
                RegeditRw.RegWrite("MainWindowFontWeight", "False");
            }
            mainWindow.FontWeight = mainWindowFontWeight == "True" ? FontWeights.Bold : FontWeights.Normal;
            ((TextBlock)((VisualBrush)FindResource("HelpBrush")).Visual).FontWeight = mainWindow.FontWeight;
            Global.FontWeight = mainWindow.FontWeight;
            // 设置淡紫色透明光标
            SeCheckBoxLavenderCursor.IsChecked = mainWindowLavenderCursor == "True";
            // 版本初始化
            UiVersion.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version + Global.VersionSuffix;
            // 窗口可视化计时器
            _visiTimer.Enabled = true;
            _visiTimer.Interval = 200;
            _visiTimer.Tick += VisiTimerEvent;
            _visiTimer.Start();
            // 搜索文本框计时器
            _searchTextBoxTimer.Interval = 100;
            _searchTextBoxTimer.Tick += SearchTimerEvent;
            // 设置光标资源字典路径
            if (SeCheckBoxLavenderCursor.IsChecked == true)
            {
                CursorDictionary.Source =
                    new Uri("pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/CursorDictionary.xaml",
                        UriKind.Absolute);
            }
            else
            {
                CursorDictionary.Source =
                    new Uri("pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DefaultCursorDictionary.xaml",
                        UriKind.Absolute);
            }
            // 显示窗口
            MwVisibility = true;
            // 窗口置顶
            if (winTopmost == 1)
            {
                Se_button_Topmost_Click(null, null);
            }
            // 设置背景
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
                    UiBackGroundBorder.Visibility = Visibility.Collapsed;
                }
            }
            // 设置背景拉伸方式
            if (bgStretch == 0)
            {
                bgStretch = 2;
            }
            SeComboBoxBackgroundStretch.SelectedIndex = (int)bgStretch - 1;
            // 设置背景透明度
            if (bgAlpha == 0)
            {
                bgAlpha = 101;
            }
            SeBgAlpha.Value = bgAlpha - 1;
            SeBgAlphaText.Text = "背景不透明度：" + (int)SeBgAlpha.Value + "%";
            UiBackGroundBorder.Opacity = (bgAlpha - 1) / 100;
            // 设置面板透明度
            if (bgPanelAlpha == 0)
            {
                bgPanelAlpha = 61;
            }
            SePanelAlpha.Value = bgPanelAlpha - 1;
            SePanelAlphaText.Text = "面板不透明度：" + (int)SePanelAlpha.Value + "%";
            // 设置窗口透明度
            if (windowAlpha == 0)
            {
                windowAlpha = 101;
            }
            SeWindowAlpha.Value = windowAlpha - 1;
            SeWindowAlphaText.Text = "窗口不透明度：" + (int)SeWindowAlpha.Value + "%";
            Opacity = (windowAlpha - 1) / 100;
            // 设置高度和宽度
            Width = mainWindowWidth;
            Height = mainWindowHeight;
            // 设置游戏版本
            UiGameversion.SelectedIndex = (int)gameVersion;
            #endregion
            // 设置托盘区图标
            SetNotifyIcon();
            // 右侧面板导航到欢迎界面
            RightFrame.Navigate(new WelcomePage());
            // 是否显示开服工具
            if (Global.TestMode)
                SidebarDedicatedServer.Visibility = Visibility.Visible;
            // 检测新版本
            UpdatePan.UpdateNow();
        }
        
        /// <summary>
        /// MainWindow窗口加载
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 加载快捷键
            // mainWindow句柄
            intPtr = new WindowInteropHelper(this).Handle;
            // BossKey
            var hotkeyBossKeyControlKeys = RegeditRw.RegRead("HotkeyBossKeyControlKeys");
            var hotkeyBossKeyMainKey = RegeditRw.RegRead("HotkeyBossKeyMainKey");
            if (hotkeyBossKeyControlKeys == 0 && hotkeyBossKeyMainKey == 0)
            {
                hotkeyBossKeyControlKeys = 3; //Ctrl + Alt
                RegeditRw.RegWrite("HotkeyBossKeyControlKeys", 3);
                hotkeyBossKeyMainKey = 0x57; //W
                RegeditRw.RegWrite("HotkeyBossKeyMainKey", 0x57);
            }
            BossKeyHotKey = new HotKey(this, (Global.KeyModifiers)hotkeyBossKeyControlKeys, (Keys)hotkeyBossKeyMainKey);
            BossKeyHotKey.OnHotKey += hotKeyBosskey_OnHotKey;
            // ConsoleKey
            var hotkeyConsoleKeyMainKey = RegeditRw.RegRead("HotkeyConsoleKeyMainKey");
            if (hotkeyConsoleKeyMainKey == 0)
            {
                hotkeyConsoleKeyMainKey = 0x71; // F2
                RegeditRw.RegWrite("HotkeyConsoleKeyMainKey", 0x71);
            }
            ConsoleKeyHotKey = new HotKey(this, 0, (Keys)hotkeyConsoleKeyMainKey);
            ConsoleKeyHotKey.OnHotKey += hotKeyConsole_OnHotKey;
            #endregion
            #region 加载字体
            foreach (var str in ReadFont())
            {
                var textBlock = new TextBlock
                {
                    Text = str,
                    FontFamily = new FontFamily(str)
                };
                SeComboBoxFont.Items.Add(textBlock);
            }
            var mainWindowFont = RegeditRw.RegReadString("MainWindowFont");
            var stringList = (from TextBlock textBlock in SeComboBoxFont.Items select textBlock.Text).ToList();
            SeComboBoxFont.SelectedIndex = stringList.IndexOf(mainWindowFont);
            var mainWindowFontWeight = RegeditRw.RegReadString("MainWindowFontWeight");
            SeCheckBoxFontWeight.IsChecked = mainWindowFontWeight == "True";
            LoadFont = true;
            #endregion
        }

        #region 热键方法
        /// <summary>
        /// 热键：老板键
        /// </summary>
        private void hotKeyBosskey_OnHotKey()
        {
            if (mainWindow.NotifyIcon.Visible)
            {
                mainWindow.MwVisibility = false;
                mainWindow.NotifyIcon.Visible = false;
            }
            else
            {
                mainWindow.MwVisibility = true;
                mainWindow.NotifyIcon.Visible = true;
            }
        }

        /// <summary>
        /// 热键：控制台快捷输入
        /// </summary>
        private static void hotKeyConsole_OnHotKey()
        {
            if (Global.ConsoleSendKey != null)
            {
                Global.ConsoleSendKey(null, null);
            }
        }
        #endregion

        #endregion

    }
}
