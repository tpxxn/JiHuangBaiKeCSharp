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
using ServerTools;
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
using WpfLearn.UserControls;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow主类
    /// </summary>
    public partial class MainWindow : Window
    {
        
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
            #region "读取注册表(必须在初始化之前读取)"
            //背景图片
            string bg = RegeditRW.RegReadString("Background");
            double bgStretch = RegeditRW.RegRead("BackgroundStretch");
            //透明度
            double bgAlpha = RegeditRW.RegRead("BGAlpha");
            double bgPanelAlpha = RegeditRW.RegRead("BGPanelAlpha");
            double windowAlpha = RegeditRW.RegRead("WindowAlpha");
            //窗口大小
            double mainWindowHeight = RegeditRW.RegRead("MainWindowHeight");
            double mainWindowWidth = RegeditRW.RegRead("MainWindowWidth");
            //字体
            string mainWindowFont = RegeditRW.RegReadString("MainWindowFont");
            //设置菜单
            double winTopmost = RegeditRW.RegRead("Topmost");
            //游戏版本
            double gameVersion = RegeditRW.RegRead("GameVersion");
            #endregion
            //初始化
            InitializeComponent();
            //窗口缩放
            SourceInitialized += delegate (object sender, EventArgs e) { _HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource; };
            MouseMove += new System.Windows.Input.MouseEventHandler(Window_MouseMove);
            //mainWindow初始化标志
            MWInit = true;
            #region "读取设置"
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
                    var brush = new ImageBrush()
                    {
                        ImageSource = new BitmapImage(new Uri(bg))
                    };
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
            #endregion
        }
        //MainWindow窗口加载
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new KeyboardHandler(this);//加载快捷键
            foreach (string str in RF())//加载字体
            {
                TextBlock TB = new TextBlock()
                {
                    Text = str,
                    FontFamily = new FontFamily(str)
                };
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
            DediButtomPanelInitalize();//服务器面板初始化
            InitServer();// 加载服务器文件
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

        #region "服务器面板"

        // 游戏平台改变,初始化一切
        private void DediSettingGameVersionSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 赋值

            GamePingTai = e.AddedItems[0].ToString();
            if (GamePingTai=="TGP")
            {
                DediCtrateOpenGame.Visibility = Visibility.Collapsed;
                DediCtrateWorldButton.Content = "保存世界";
            }
            else
            {
                DediCtrateOpenGame.Visibility = Visibility.Visible;
                DediCtrateWorldButton.Content = "创建世界";
            }

            if (e.RemovedItems.Count!=0)
            {
                InitServer();
            }
         

        }

        // 选择游戏exe文件
        private void DediSettingGameDirSelect_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            // fileDialog.InitialDirectory = "d:\\";
            fileDialog.Title = "选择游戏exe文件";
            if (gamePingTai == "TGP")
            {
                fileDialog.Filter = "饥荒游戏exe文件(*.exe)|dontstarve_rail.exe";
            }
            else
            {
                fileDialog.Filter = "饥荒游戏exe文件(*.exe)|dontstarve_steam.exe";
            }
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String fileName = fileDialog.FileName;
                if (String.IsNullOrEmpty(fileName) || !fileName.Contains("dontstarve_"))
                {
                    System.Windows.MessageBox.Show("文件选择错误,请选择正确文件,以免出错");
                    return;
                }
                pathAll.Client_FilePath = fileName;
                DediSettingGameDirSelectTextBox.Text = fileName;
                XmlHelper.WriteClientPath("ServerConfig.xml", fileName, GamePingTai);

            }
        }

        // 选择服务器文件
        private void DediSettingDediDirSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            // fileDialog.InitialDirectory = "d:\\";
            fileDialog.Title = "选择服务器exe文件";

            fileDialog.Filter = "饥荒服务器exe文件(*.exe)|dontstarve_dedicated_server_nullrenderer.exe";


            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String fileName = fileDialog.FileName;
                if (String.IsNullOrEmpty(fileName) || !fileName.Contains("dontstarve_dedicated_server_nullrenderer"))
                {
                    System.Windows.MessageBox.Show("文件选择错误,请选择正确文件,以免出错");
                    return;
                }
                pathAll.Server_FilePath = fileName;
                DediSettingDediDirSelectTextBox.Text = fileName;
                XmlHelper.WriteServerPath("ServerConfig.xml", fileName, GamePingTai);

                // 读取mods
                mods = null;
                if (!string.IsNullOrEmpty(pathAll.ServerMods_DirPath))
                {
                    mods = new Mods(pathAll.ServerMods_DirPath);
                }

                SetModSet();
            }

        }

        // 双击打开所在文件夹"客户端"
        private void DediSettingGameDirSelectTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(pathAll.Client_FilePath) && File.Exists(pathAll.Client_FilePath))
            {
                Process.Start(Path.GetDirectoryName(pathAll.Client_FilePath));
            }
        }
        // 双击打开所在文件夹"服务端"
        private void DediSettingDediDirSelectTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(pathAll.Server_FilePath) && File.Exists(pathAll.Server_FilePath))
            {
                Process.Start(Path.GetDirectoryName(pathAll.Server_FilePath));
            }
        }

        // 删除当前存档按钮
        private void DediMainTop_Delete_Click(object sender, RoutedEventArgs e)
        {

            // 0. 关闭服务器

            Process[] ps = Process.GetProcesses();
            foreach (Process item in ps)
            {
                if (item.ProcessName == "dontstarve_dedicated_server_nullrenderer")
                {
                    item.Kill();
                }
            }

            // 1. radioBox 写 创建世界
            ((System.Windows.Controls.RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + CunDangCao)).Content = "创建世界";
            // 2. 删除当前存档
            if (Directory.Exists(pathAll.YyServer_DirPath))
            {
                Directory.Delete(pathAll.YyServer_DirPath, true);
            }
           
            // 2.1 取消选择,谁都不选
           ((System.Windows.Controls.RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + CunDangCao)).IsChecked = false;

            // 2.2 
         
            // DediMainBorder.IsEnabled = false;
            Jinyong(false);
            //// 3. 复制一份新的过来                 
            //ServerTools.Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);


            //if (!Directory.Exists(pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao))
            //{
            //    Directory.Move(pathAll.DoNotStarveTogether_DirPath + "\\Server", pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao);
            //}
            //// 4. 读取新的存档
            //SetBaseSet();

        }

        // 创建世界按钮
        private void DediCtrateWorldButton_Click(object sender, RoutedEventArgs e)
        {
            RunServer();
        }
        // 打开游戏
        private void DediOpenGameButton_Click(object sender, RoutedEventArgs e)
        {
            RunClient();
        }


        #endregion


    }
}