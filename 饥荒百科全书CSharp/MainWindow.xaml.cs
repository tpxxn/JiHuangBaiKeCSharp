using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;
using 饥荒百科全书CSharp.Properties;
//控件计数
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
        /// <summary>
        /// 初始化
        /// </summary>
        public MainWindow()
        {
            string bg = RegeditRW.RegReadString("Background");
            double bgAlpha = RegeditRW.RegRead("BGAlpha");
            double bgPanelAlpha = RegeditRW.RegRead("BGPanelAlpha");
            double MWH = RegeditRW.RegRead("MainWindowHeight");
            double MWW = RegeditRW.RegRead("MainWindowWidth");

            InitializeComponent();

            //右侧面板Visibility属性初始化
            RightPanelVisibility("Welcome");
            //初始化版本
            UI_Version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
            //设置高度和宽度
            if (MWH == 0)
            {
                MWH = 660;
            }
            if (MWW == 0)
            {
                MWW = 1000;
            }
            Width = MWW;
            Height = MWH;
            //设置搜索框的最大字符数
            UI_search.MaxLength = 10;
        }

        /// <summary>
        /// 拖动窗口
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// 窗口尺寸改变
        /// </summary>
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

        /// <summary>
        /// 窗口加载
        /// </summary>
        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //测试
            //test.TextP = "23242342343434";
            //test.ImageP = "F_honeycomb";
            //test.TextWidthP = true;
        }

        #region "右上角按钮"
        #region "搜索框清除按钮显示/隐藏"
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

        private void UI_search_clear_Click(object sender, RoutedEventArgs e)
        {
            UI_search.Text = "";
            Visi.VisiCol(true, UI_search_clear);
        }
        #endregion
        /// <summary>
        /// 设置
        /// </summary>
        private void UI_btn_setting_Click(object sender, RoutedEventArgs e)
        {
            UI_pop_setting.IsOpen = true;
        }
        #region "皮肤菜单"
        /// <summary>
        /// 皮肤菜单
        /// </summary>
        private void UI_btn_bg_Click(object sender, RoutedEventArgs e)
        {
            UI_pop_bg.IsOpen = true;
        }
        /// <summary>
        /// 设置背景
        /// </summary>
        private void Se_button_Background_Click(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }
        /// <summary>
        /// 清除背景
        /// </summary>
        private void Se_button_Background_Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearBackground();
        }
        /// <summary>
        /// 设置面板透明度
        /// </summary>
        private void Se_Panel_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RightGrid.Background.Opacity = Se_Panel_Alpha.Value / 100;
            Se_Panel_Alpha_Text.Text = "面板不透明度：" + (int)Se_Panel_Alpha.Value + "%";
            RegeditRW.RegWrite("BGPanelAlpha", Se_Panel_Alpha.Value + 1);
        }
        /// <summary>
        /// 设置背景透明度
        /// </summary>
        private void Se_BG_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UI_BackGroundBorder.Opacity = Se_BG_Alpha.Value / 100;
            Se_BG_Alpha_Text.Text = "背景不透明度：" + (int)Se_BG_Alpha.Value + "%";
            RegeditRW.RegWrite("BGAlpha", Se_BG_Alpha.Value + 1);
        }
        #endregion
        /// <summary>
        /// 最小化按钮
        /// </summary>
        private void UI_btn_minimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        Rect rcnormal;//窗口位置
        /// <summary>
        /// 最大化按钮
        /// </summary>
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
        /// <summary>
        /// 还原按钮
        /// </summary>
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
        /// <summary>
        /// 关闭按钮
        /// </summary>
        private void UI_btn_close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region "主页面链接"
        /// <summary>
        /// 官网
        /// </summary>
        private void W_button_official_website_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jihuangbaike.com");
        }
        /// <summary>
        /// Mod
        /// </summary>
        private void W_button_Mods_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=635215011");
        }
        /// <summary>
        /// DS新闻
        /// </summary>
        private void W_button_DSNews_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=219740");
        }
        /// <summary>
        /// DST新闻
        /// </summary>
        private void W_button_DSTNewS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=322330");
        }
        /// <summary>
        /// 群二维码
        /// </summary>
        private void W_button_QRCode_Qun_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("tencent://groupwpa/?subcmd=all&param=7B2267726F757055696E223A3538303333323236382C2274696D655374616D70223A313437303132323238337D0A");
        }
        #endregion

        #region "设置/清除背景"
        /// <summary>
        /// 设置背景
        /// </summary>
        public void SetBackground()
        {
            var OFD = new OpenFileDialog();
            OFD.FileName = ""; //默认文件名
            OFD.DefaultExt = ".png"; // 默认文件扩展名
            OFD.Filter = "图像文件 (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"; //文件扩展名过滤器

            bool? result = OFD.ShowDialog(); //显示打开文件对话框

            Visi.VisiCol(false, UI_BackGroundBorder);
            try
            {
                string PictruePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKe\"; //设置文件夹位置
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

                MessageBox.Show("没有选择正确的图片");
            }
        }
        /// <summary>
        /// 清除背景
        /// </summary>
        private void ClearBackground()
        {
            Visi.VisiCol(true, UI_BackGroundBorder);
            Se_BG_Alpha_Text.Foreground = Brushes.Silver;
            Se_BG_Alpha.IsEnabled = false;
            RegeditRW.RegWrite("Background", "");
        }
        #endregion

        #region "模拟SplitView按钮"
        public static byte LeftMenuState = 0;//左侧菜单状态，0为关闭，1为打开
        /// <summary>
        /// 左侧菜单按钮
        /// </summary>
        private void Sidebar_Menu_Click(object sender, RoutedEventArgs e)
        {
            var MainWindowWidth = mainWindow.ActualWidth;
            double MainGridWidth = MainGrid.ActualWidth;
            if (LeftMenuState == 0)
            {
                Visi.VisiCol(false, UI_Version);
                AnimationClass.Animation(LCWidth, 50, 150, WidthProperty);
                AnimationClass.Animation(LeftCanvas, 50, 150, WidthProperty);
                AnimationClass.Animation(LeftWrapPanel, 50, 150, WidthProperty);
                LCWidth.Width = new GridLength(150);
                LeftCanvas.Width = 150;
                LeftWrapPanel.Width = 150;
                LeftMenuState = 1;
            }
            else
            {
                Visi.VisiCol(true, UI_Version);
                AnimationClass.Animation(LCWidth, 150, 50, WidthProperty);
                AnimationClass.Animation(LeftCanvas, 150, 50, WidthProperty);
                AnimationClass.Animation(LeftWrapPanel, 150, 50, WidthProperty);
                LCWidth.Width = new GridLength(50);
                LeftCanvas.Width = 50;
                LeftWrapPanel.Width = 50;
                LeftMenuState = 0;
            }

        }
        #endregion

        #region "右侧面板Visibility属性设置"
        /// <summary>
        /// 右侧面板初始化
        /// </summary>
        private void RightPanelVisibilityInitialize()
        {
            foreach (UIElement vControl in RightGrid.Children)
            {
                Visi.VisiCol(true, vControl);
            }
        }
        /// <summary>
        /// 右侧面板可视化设置
        /// </summary>
        /// <param name="obj">右侧面板</param>
        private void RightPanelVisibility(string obj)
        {
            RightPanelVisibilityInitialize();
            switch (obj)
            {
                case "Welcome":
                    Visi.VisiCol(false, RightGrid_Welcome);
                    Visi.VisiCol(true, RightGrid);
                    break;
                case "Setting":
                    Visi.VisiCol(false, RightGrid_Setting);
                    Visi.VisiCol(true, RightGrid);
                    break;
                default:
                    Visi.VisiCol(false, UI_Splitter);
                    Visi.VisiCol(false, RightGrid);
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

    }
}