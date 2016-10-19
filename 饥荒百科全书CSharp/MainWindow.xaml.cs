using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        #region "最小化、最大化、关闭按钮"
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
        #endregion

        #region "模拟SplitView按钮"
        public static byte LeftMenuState = 0;

        private void LeftButton_Menu_Click(object sender, RoutedEventArgs e)
        {
            var animation = new AnimationClass();
            var MainWindowWidth = mainWindow.ActualWidth;//获取窗口宽度
            double MainGridWidth = MainGrid.ActualWidth;
            if (LeftMenuState == 0)
            {
                UI_Version.Visibility = Visibility.Visible;
                animation.Animation(LCWidth, 50, 150, WidthProperty);
                LCWidth.Width = new GridLength(150);
                animation.Animation(LeftCanvas, 50, 150, WidthProperty);
                LeftCanvas.Width = 150;
                animation.Animation(LeftWrapPanel, 50, 150, WidthProperty);
                LeftWrapPanel.Width = 150;
                LeftMenuState = 1;
            }
            else
            {
                UI_Version.Visibility = Visibility.Collapsed;
                animation.Animation(LCWidth, 150, 50, WidthProperty);
                LCWidth.Width = new GridLength(50);
                animation.Animation(LeftCanvas, 150, 50, WidthProperty);
                LeftCanvas.Width = 50;
                animation.Animation(LeftWrapPanel, 150, 50, WidthProperty);
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
            test.TextP = "23242342343434";
            test.ImageP = "F_honeycomb";
            test.TextWidthP = true;

            UI_Version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
            /*
              If RegReadBG <> "" Then
                  Dim brush As New ImageBrush
                  brush.ImageSource = New BitmapImage(New Uri(RegReadBG))
                  BackGroundBorder.Background = brush
              Else
                  Se_TextBlock_Alpha.Foreground = Brushes.Silver
                  Setting_slider_Alpha.IsEnabled = False
              End If
             */
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }
    }
}