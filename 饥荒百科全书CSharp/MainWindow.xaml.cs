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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 饥荒百科全书CSharp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
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
                this.Left += e.GetPosition(this).X - enterX;
                this.Top += e.GetPosition(this).Y - enterY;
            }
        }

    }
}