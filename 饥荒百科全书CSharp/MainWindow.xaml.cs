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
            var MainWindowWidth = ActualWidth;//获取窗口宽度
            if (LeftMenuState == 0)
            {
                animation.Animation(LeftCanvas, 50, 150, WidthProperty);
                animation.Animation(LeftWrapPanel, 50, 150, WidthProperty);
                animation.Animation(MainGrid, ActualWidth - 50, ActualWidth - 150, WidthProperty);
                LeftMenuState = 1;
            }
            else
            {
                animation.Animation(LeftCanvas, 150, 50, WidthProperty);
                animation.Animation(LeftWrapPanel, 150, 50, WidthProperty);
                animation.Animation(MainGrid, ActualWidth - 150, ActualWidth - 50, WidthProperty);
                LeftMenuState = 0;
            }

        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateLayout();
            var animation = new AnimationClass();
            MainGrid.Height = ActualHeight - 2;
            LeftCanvas.Height = ActualHeight - 2;
            LeftWrapPanel.Height = ActualHeight - 2;
            BackGroundBorder.Width = ActualWidth;
            SPLITTER.Height = ActualHeight - 52;
            if (LeftMenuState == 0)
            {
                animation.Animation(MainGrid, ActualWidth - 50, ActualWidth - 50, WidthProperty, 0.001);
            }
            else
            {
                animation.Animation(MainGrid, ActualWidth - 150, ActualWidth - 150, WidthProperty, 0.001);
            }
        }
        #endregion

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            test.TextP = "23242342343434";
            test.ImageP = "F_honeycomb";
            test.TextWidthP = true;
            string bg = Settings.Default.SettingBackground.ToString();
            if (bg != "")
            {
                try
                {
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(bg));
                    BackGroundBorder.Background = brush;
                }
                catch
                {
                    BackGroundBorder.Visibility = Visibility.Collapsed;
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

            BackGroundBorder.Visibility = Visibility.Visible;
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
                BackGroundBorder.Background = brush;
                Settings.Default.SettingBackground = PictruePath + filename;
                Settings.Default.Save();//保存设置
            }
            catch (Exception)
            {

                MessageBox.Show("没有选择正确的图片");
            }
        }

        private void ClearBackground()
        {
            BackGroundBorder.Visibility = Visibility.Collapsed;
            Settings.Default.SettingBackground = "";
            Settings.Default.Save();//保存设置
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }
    }
}