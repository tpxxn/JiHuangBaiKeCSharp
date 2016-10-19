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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;
using 饥荒百科全书CSharp.Properties;

namespace 饥荒百科全书CSharp
{
    public partial class MainWindow : Window
    {
        #region "拖动窗口" 
        private static bool IsDrag = false;
        private double enterX;
        private double enterY;
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsDrag = true;
            enterX = e.GetPosition(this).X;
            enterY = e.GetPosition(this).Y;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDrag = false;
            enterX = 0;
            enterY = 0;
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrag)
            {
                Left += e.GetPosition(this).X - enterX;
                Top += e.GetPosition(this).Y - enterY;
                UI_btn_normal.Visibility = Visibility.Collapsed;
                UI_btn_maximized.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region "右上角按钮"
        private void UI_btn_minimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        Rect rcnormal;//窗口位置

        private void UI_btn_maximized_Click(object sender, RoutedEventArgs e)
        {
            UI_btn_maximized.Visibility = Visibility.Collapsed;
            UI_btn_normal.Visibility = Visibility.Visible;
            rcnormal = new Rect(Left, Top, Width, Height);//保存下当前位置与大小
            Left = 0;
            Top = 0;
            Rect rc = SystemParameters.WorkArea;
            Width = rc.Width;
            Height = rc.Height;
            //WindowState = WindowState.Maximized;
        }

        private void UI_btn_normal_Click(object sender, RoutedEventArgs e)
        {
            UI_btn_normal.Visibility = Visibility.Collapsed;
            UI_btn_maximized.Visibility = Visibility.Visible;
            Left = rcnormal.Left;
            Top = rcnormal.Top;
            Width = rcnormal.Width;
            Height = rcnormal.Height;
            //WindowState = WindowState.Normal;
        }

        private void UI_btn_close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }

        #endregion

        #region "主页面链接"
        private void W_button_official_website_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jihuangbaike.com");
        }

        private void W_button_Mods_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=635215011");
        }

        private void W_button_DSNews_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=219740");
        }

        private void W_button_DSTNewS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=322330");
        }

        private void W_button_QRCode_Qun_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("tencent://groupwpa/?subcmd=all&param=7B2267726F757055696E223A3538303333323236382C2274696D655374616D70223A313437303132323238337D0A");
        }
        #endregion

        #region "背景"
        public void SetBackground()
        {
            var OFD = new OpenFileDialog();
            OFD.FileName = ""; //默认文件名
            OFD.DefaultExt = ".png"; // 默认文件扩展名
            OFD.Filter = "图像文件 (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"; //文件扩展名过滤器

            bool? result = OFD.ShowDialog(); //显示打开文件对话框

            UI_BackGroundBorder.Visibility = Visibility.Visible;
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
                Settings.Default.SettingBackground = PictruePath + filename;
                Settings.Default.Save();
            }
            catch (Exception)
            {

                MessageBox.Show("没有选择正确的图片");
            }
        }

        private void ClearBackground()
        {
            UI_BackGroundBorder.Visibility = Visibility.Collapsed;
            Settings.Default.SettingBackground = "";
            Settings.Default.Save();
        }
        #endregion

        #region "右侧面板Visibility属性设置"
        private void RightPanelVisibilityInitialize()
        {
            var cv = new ControlVisibility();
            foreach (UIElement vControl in RightGrid.Children)
            {
                cv.ControlVisibilityCollapsed(true, vControl);
            }
        }

        private void RightPanelVisibility(string obj)
        {
            var cv = new ControlVisibility();
            RightPanelVisibilityInitialize();
            switch (obj)
            {
                case "Welcome":
                    cv.ControlVisibilityCollapsed(false, RightGrid_Welcome);
                    break;
                case "Setting":
                    cv.ControlVisibilityCollapsed(false, RightGrid_Setting);
                    break;
                default:
                    cv.ControlVisibilityCollapsed(false, UI_Splitter);
                    switch (obj)
                    {
                        case "Character":
                            cv.ControlVisibilityCollapsed(false, ScrollViewer_Left_Character, ScrollViewer_Right_Character);
                            break;
                        case "Food":
                            cv.ControlVisibilityCollapsed(false, ScrollViewer_Left_Food, ScrollViewer_Right_Food);
                            break;
                        case "Science":
                            cv.ControlVisibilityCollapsed(false, ScrollViewer_Left_Science, ScrollViewer_Right_Science);
                            break;
                        case "Cooking_Simulator":
                            cv.ControlVisibilityCollapsed(false, ScrollViewer_Left_Cooking_Simulator, ScrollViewer_Right_Cooking_Simulator);
                            break;
                        case "Animal":
                            cv.ControlVisibilityCollapsed(false, ScrollViewer_Left_Animal, ScrollViewer_Right_Animal);
                            break;
                        case "Natural":
                            cv.ControlVisibilityCollapsed(false, ScrollViewer_Left_Natural, ScrollViewer_Right_Natural);
                            break;
                        case "Goods":
                            cv.ControlVisibilityCollapsed(false, ScrollViewer_Left_Goods, ScrollViewer_Right_Goods);
                            break;
                    }
                    break;
            }
        }
        #endregion

        #region "模拟SplitView按钮"
        public static byte LeftMenuState = 0;

        private void Sidebar_Menu_Click(object sender, RoutedEventArgs e)
        {
            var animation = new AnimationClass();
            var MainWindowWidth = mainWindow.ActualWidth;//获取窗口宽度
            double MainGridWidth = MainGrid.ActualWidth;
            if (LeftMenuState == 0)
            {
                UI_Version.Visibility = Visibility.Visible;
                animation.Animation(LCWidth, 50, 150, WidthProperty);
                animation.Animation(LeftCanvas, 50, 150, WidthProperty);
                animation.Animation(LeftWrapPanel, 50, 150, WidthProperty);
                LCWidth.Width = new GridLength(150);
                LeftCanvas.Width = 150;
                LeftWrapPanel.Width = 150;
                LeftMenuState = 1;
            }
            else
            {
                UI_Version.Visibility = Visibility.Collapsed;
                animation.Animation(LCWidth, 150, 50, WidthProperty);
                animation.Animation(LeftCanvas, 150, 50, WidthProperty);
                animation.Animation(LeftWrapPanel, 150, 50, WidthProperty);
                LCWidth.Width = new GridLength(50);
                LeftCanvas.Width = 50;
                LeftWrapPanel.Width = 50;
                LeftMenuState = 0;
            }

        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UI_Version.Margin = new Thickness(10, mainWindow.ActualHeight - 35, 0, 0);
            //UpdateLayout();
            //var animation = new AnimationClass();
            LeftCanvas.Height = mainWindow.ActualHeight - 2;
            LeftWrapPanel.Height = mainWindow.ActualHeight - 2;

            //MainGrid.Height = mainWindow.ActualHeight - 2;
            //UI_BackGroundBorder.Width = mainWindow.ActualWidth - 51;
            //UI_BackGroundBorder.Height = ActualHeight;
            UI_Splitter.Height = ActualHeight - 52;
            //WrapPanel_Right.Width = MainGrid.ActualWidth - 200;
            //if (LeftMenuState == 0)
            //{
            //    animation.Animation(MainGrid, mainWindow.ActualWidth - 51, mainWindow.ActualWidth - 51, WidthProperty, 0.001);
            //    animation.Animation(MainGrid, mainWindow.ActualWidth - 51, mainWindow.ActualWidth - 51, WidthProperty, 0.001);
            //}
            //else
            //{
            //    animation.Animation(UI_BackGroundBorder, mainWindow.ActualWidth - 151, mainWindow.ActualWidth - 151, WidthProperty, 0.001);
            //    animation.Animation(UI_BackGroundBorder, mainWindow.ActualWidth - 151, mainWindow.ActualWidth - 151, WidthProperty, 0.001);
            //}
        }
        #endregion

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Welcome");///右侧面板Visibility属性初始化
            ///测试
            //test.TextP = "23242342343434";
            //test.ImageP = "F_honeycomb";
            //test.TextWidthP = true;
            ///初始化版本
            UI_Version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ///设置背景
            string bg = Settings.Default.SettingBackground.ToString();
            if (bg != "")
            {
                try
                {
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(bg));
                    UI_BackGroundBorder.Background = brush;
                }
                catch
                {
                    UI_BackGroundBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {

            }
            //控件计数
            //int sum = 0;
            //foreach (Control vControl in WrapPanel_Right.Children)
            //{
            //    if (vControl is ButtonWithText)
            //    {
            //        sum += 1;
            //    }
            //}
        }

    }
}