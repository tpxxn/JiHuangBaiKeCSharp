using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;
//控件计数例子
//int sum = 0;
//foreach (Control vControl in WrapPanel_Right.Children)
//{
//    if (vControl is ButtonWithText)
//    {
//        sum += 1;
//    }
//}
namespace 饥荒百科全书CSharp
{
    public partial class MainWindow : Window
    {
        //引用光标资源字典
        ResourceDictionary cursorDictionary = new ResourceDictionary();

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

        //初始化
        public MainWindow()
        {
            //读取注册表(必须在初始化之前读取)
            ////背景图片
            string bg = RegeditRW.RegReadString("Background");
            ////透明度
            double bgAlpha = RegeditRW.RegRead("BGAlpha");
            double bgPanelAlpha = RegeditRW.RegRead("BGPanelAlpha");
            double windowAlpha = RegeditRW.RegRead("WindowAlpha");
            ////窗口大小
            double mainWindowHeight = RegeditRW.RegRead("MainWindowHeight");
            double mainWindowWidth = RegeditRW.RegRead("MainWindowWidth");
            ////游戏版本
            double gameVersion = RegeditRW.RegRead("GameVersion");
            //设置菜单
            double winTopmost = RegeditRW.RegRead("Topmost");
            //初始化
            InitializeComponent();
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
            //版本初始化
            UI_Version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
            if (mainWindowHeight == 0)
            {
                mainWindowHeight = 660;
            }
            if (mainWindowWidth == 0)
            {
                mainWindowWidth = 1000;
            }
            Width = mainWindowWidth;
            Height = mainWindowHeight;
            //设置游戏版本
            UI_gameversion.SelectedIndex = (int)gameVersion;
            //设置搜索框的最大字符数
            UI_search.MaxLength = 10;
        }

        #region "拖动窗口"
        private void mainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {   
            Cursor = (System.Windows.Input.Cursor)cursorDictionary["Cursor_move"];
            DragMove();
        }
        private void mainWindow_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            Cursor = (System.Windows.Input.Cursor)cursorDictionary["Cursor_pointer"];
        }
        #endregion

        //窗口尺寸改变
        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //最大化
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                UI_btn_maximized_Click(null, null);
            }
            //设置版本号位置
            UI_Version.Margin = new Thickness(10, mainWindow.ActualHeight - 35, 0, 0);
            //左侧面板高度
            LeftCanvas.Height = mainWindow.ActualHeight - 2;
            LeftWrapPanel.Height = mainWindow.ActualHeight - 2;
            //Splitter高度
            UI_Splitter.Height = ActualHeight - 52;
            RegeditRW.RegWrite("MainWindowHeight", ActualHeight);
            RegeditRW.RegWrite("MainWindowWidth", ActualWidth);
        }

        //窗口加载
        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new KeyboardHandler(this);
            #region "删除旧版本文件"
            string oldVersionPath = RegeditRW.RegReadString("OldVersionPath");
            if (oldVersionPath != System.Windows.Forms.Application.ExecutablePath && oldVersionPath != "")
            {
                try
                {
                    File.Delete(oldVersionPath);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("删除旧版本错误，请手动删除：" + ex);
                }
            }
            RegeditRW.RegWrite("OldVersionPath", "");
            #endregion
            //测试
            //test.TextP = "23242342343434";
            //test.ImageP = "F_honeycomb";
            //test.TextWidthP = true;
        }

        #region "右上角按钮"
        #region "搜索框清除按钮显示/隐藏"
        //设置清除按钮可见性
        private void UI_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UI_search.Text != "")
            {
                Visi.VisiCol(false, UI_search_clear);
            }
            else
            {
                Visi.VisiCol(true, UI_search_clear);
            }
        }
        //清除按钮
        private void UI_search_clear_Click(object sender, RoutedEventArgs e)
        {
            UI_search.Text = "";
            Visi.VisiCol(true, UI_search_clear);
        }
        #endregion

        #region "游戏版本"
        //游戏版本选择
        private void UI_gameversion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RegeditRW.RegWrite("GameVersion", UI_gameversion.SelectedIndex);
        }
        #endregion

        #region "设置菜单"
        //设置
        private void UI_btn_setting_Click(object sender, RoutedEventArgs e)
        {
            UI_pop_setting.IsOpen = true;
        }
        //检查更新
        private void Se_button_Update_Click(object sender, RoutedEventArgs e)
        {
            updatePan.UpdateNow();
            UI_pop_setting.IsOpen = false;
            MWVisivility = false;
        }
        //窗口置顶
        private void Se_button_Topmost_Click(Object sender, RoutedEventArgs e)
        {
            if (Topmost == false)
            {
                Topmost = true;
                Se_image_Topmost.Source = ResourceShortName.PictureShortName(ResourceShortName.ShortName("Setting_Top_T"));
                Se_textblock_Topmost.Text = "永远置顶";
                RegeditRW.RegWrite("Topmost", 1);
            }
            else
            {
                Topmost = false;
                Se_image_Topmost.Source = ResourceShortName.PictureShortName(ResourceShortName.ShortName("Setting_Top_F"));
                Se_textblock_Topmost.Text = "永不置顶";
                RegeditRW.RegWrite("Topmost", 0);
            }
        }
        #endregion

        #region "皮肤菜单"
        //皮肤菜单
        private void UI_btn_bg_Click(object sender, RoutedEventArgs e)
        {
            UI_pop_bg.IsOpen = true;
        }
        //设置背景
        private void Se_button_Background_Click(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }
        //清除背景
        private void Se_button_Background_Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearBackground();
        }

        #region "设置/清除背景"
        //设置背景
        public void SetBackground()
        {
            var OFD = new Microsoft.Win32.OpenFileDialog();
            OFD.FileName = ""; //默认文件名
            OFD.DefaultExt = ".png"; // 默认文件扩展名
            OFD.Filter = "图像文件 (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"; //文件扩展名过滤器

            bool? result = OFD.ShowDialog(); //显示打开文件对话框

            Visi.VisiCol(false, UI_BackGroundBorder);
            try
            {
                string PictruePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKeCSharp\Background\"; //设置文件夹位置
                if ((Directory.Exists(PictruePath)) == false) //若文件夹不存在
                {
                    Directory.CreateDirectory(PictruePath);
                }
                var filename = Path.GetFileName(OFD.FileName); //设置文件名
                try
                {
                    File.Copy(OFD.FileName, PictruePath + filename, true);
                }
                catch (Exception) { }
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(PictruePath + filename));
                UI_BackGroundBorder.Background = brush;
                Se_BG_Alpha_Text.Foreground = Brushes.Black;
                Se_BG_Alpha.IsEnabled = true;
                RegeditRW.RegWrite("Background", PictruePath + filename);
            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("没有选择正确的图片");
            }
        }
        //清除背景
        private void ClearBackground()
        {
            Visi.VisiCol(true, UI_BackGroundBorder);
            Se_BG_Alpha_Text.Foreground = Brushes.Silver;
            Se_BG_Alpha.IsEnabled = false;
            RegeditRW.RegWrite("Background", "");
        }
        #endregion

        //设置背景透明度
        private void Se_BG_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UI_BackGroundBorder.Opacity = Se_BG_Alpha.Value / 100;
            Se_BG_Alpha_Text.Text = "背景不透明度：" + (int)Se_BG_Alpha.Value + "%";
            RegeditRW.RegWrite("BGAlpha", Se_BG_Alpha.Value + 1);
        }
        //设置面板透明度
        private void Se_Panel_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RightGrid.Background.Opacity = Se_Panel_Alpha.Value / 100;
            Se_Panel_Alpha_Text.Text = "面板不透明度：" + (int)Se_Panel_Alpha.Value + "%";
            RegeditRW.RegWrite("BGPanelAlpha", Se_Panel_Alpha.Value + 1);
        }
        //设置窗口透明度
        private void Se_Window_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Opacity = Se_Window_Alpha.Value / 100;
            Se_Window_Alpha_Text.Text = "窗口不透明度：" + (int)Se_Window_Alpha.Value + "%";
            RegeditRW.RegWrite("WindowAlpha", Se_Window_Alpha.Value + 1);
        }
        #endregion

        #region "最小化/最大化/关闭按钮"
        //最小化按钮
        private void UI_btn_minimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        Rect rcnormal;//窗口位置
        //最大化按钮
        private void UI_btn_maximized_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(true, UI_btn_maximized);
            Visi.VisiCol(false, UI_btn_normal);
            rcnormal = new Rect(Left, Top, Width, Height);//保存下当前位置与大小
            Left = 0;
            Top = 0;
            Rect rc = SystemParameters.WorkArea;
            Width = rc.Width;
            Height = rc.Height;
            //WindowState = WindowState.Maximized;
        }
        //还原按钮
        private void UI_btn_normal_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(false, UI_btn_maximized);
            Visi.VisiCol(true, UI_btn_normal);
            Left = rcnormal.Left;
            Top = rcnormal.Top;
            Width = rcnormal.Width;
            Height = rcnormal.Height;
            //WindowState = WindowState.Normal;
        }
        //关闭按钮
        private void UI_btn_close_Click(object sender, RoutedEventArgs e)
        {

            Environment.Exit(0);
        }
        #endregion
        #endregion

        #region "模拟SplitView按钮"
        //左侧菜单状态，0为关闭，1为打开
        public static byte LeftMenuState = 0;
        //左侧菜单开关
        private void Sidebar_Menu_Click(object sender, RoutedEventArgs e)
        {
            var MainWindowWidth = mainWindow.ActualWidth;
            double MainGridWidth = MainGrid.ActualWidth;
            if (LeftMenuState == 0)
            {
                Visi.VisiCol(false, UI_Version);
                Animation.Anim(LCWidth, 50, 150, WidthProperty);
                Animation.Anim(LeftCanvas, 50, 150, WidthProperty);
                Animation.Anim(LeftWrapPanel, 50, 150, WidthProperty);
                LCWidth.Width = new GridLength(150);
                LeftCanvas.Width = 150;
                LeftWrapPanel.Width = 150;
                LeftMenuState = 1;
            }
            else
            {
                Visi.VisiCol(true, UI_Version);
                Animation.Anim(LCWidth, 150, 50, WidthProperty);
                Animation.Anim(LeftCanvas, 150, 50, WidthProperty);
                Animation.Anim(LeftWrapPanel, 150, 50, WidthProperty);
                LCWidth.Width = new GridLength(50);
                LeftCanvas.Width = 50;
                LeftWrapPanel.Width = 50;
                LeftMenuState = 0;
            }

        }
        //左侧菜单按钮
        private void Sidebar_Welcome_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Welcome");
        }

        private void Sidebar_Character_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Character");
        }

        private void Sidebar_Food_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Food");
        }
        private void Sidebar_Science_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Science");
        }

        private void Sidebar_Cooking_Simulator_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Cooking_Simulator");
        }

        private void Sidebar_Animal_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Animal");
        }

        private void Sidebar_Natural_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Natural");
        }

        private void Sidebar_Goods_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Goods");
        }

        private void Sidebar_Setting_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Setting");
        }
        #endregion

        #region "右侧面板Visibility属性设置"
        //右侧面板初始化
        private void RightPanelVisibilityInitialize()
        {
            foreach (UIElement vControl in RightGrid.Children)
            {
                Visi.VisiCol(true, vControl);
            }
        }

        // 右侧面板可视化设置
        // obj可选值：
        // 主页：Welcome
        // 人物：Character
        // 食物：Food
        // 科技：Science
        // 模拟：Cooking_Simulator
        // 生物：Animal
        // 自然：Natural
        // 物品：Goods
        // 设置：Setting
        private void RightPanelVisibility(string obj)
        {
            RightPanelVisibilityInitialize();
            switch (obj)
            {
                //欢迎界面
                case "Welcome":
                    Visi.VisiCol(false, RightGrid_Welcome);
                    Visi.VisiCol(true, RightGrid_Setting, RightGrid);
                    break;
                //设置界面
                case "Setting":
                    Visi.VisiCol(false, RightGrid_Setting);
                    Visi.VisiCol(true, RightGrid_Welcome, RightGrid);
                    break;
                //内容界面
                default:
                    //隐藏欢迎/设置界面
                    Visi.VisiCol(true, RightGrid_Welcome);
                    Visi.VisiCol(true, RightGrid_Setting);
                    //显示右侧内容Grid容器/分割器
                    Visi.VisiCol(false, RightGrid);
                    Visi.VisiCol(false, UI_Splitter); 
                    switch (obj)
                    {
                        case "Character":
                            Visi.VisiCol(false, ScrollViewer_Left_Character, ScrollViewer_Right_Character);
                            break;
                        case "Food":
                            Visi.VisiCol(false, ScrollViewer_Left_Food, ScrollViewer_Right_Food);
                            break;
                        case "Science":
                            Visi.VisiCol(false, ScrollViewer_Left_Science, ScrollViewer_Right_Science);
                            break;
                        case "Cooking_Simulator":
                            Visi.VisiCol(false, ScrollViewer_Left_Cooking_Simulator, ScrollViewer_Right_Cooking_Simulator);
                            break;
                        case "Animal":
                            Visi.VisiCol(false, ScrollViewer_Left_Animal, ScrollViewer_Right_Animal);
                            break;
                        case "Natural":
                            Visi.VisiCol(false, ScrollViewer_Left_Natural, ScrollViewer_Right_Natural);
                            break;
                        case "Goods":
                            Visi.VisiCol(false, ScrollViewer_Left_Goods, ScrollViewer_Right_Goods);
                            break;
                    }
                    break;
            }
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