using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
            //UI_btn_maximized.Cursor = ((TextBlock)Resources["Cursor_link"]).Cursor;
        }
    }
}