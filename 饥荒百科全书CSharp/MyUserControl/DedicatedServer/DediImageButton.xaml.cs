using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 饥荒百科全书CSharp.MyUserControl.DedicatedServer
{
    /// <summary>
    /// DediImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class DediImageButton : UserControl
    {// 运行顺序：静态-》普通
        public DediImageButton()
        {
            InitializeComponent();

            // 给当前值附初始值
            // CurrentImage = ImageMouseLeave;
            //t = this.Margin;
        }

        #region 【依赖属性】

        // 1. 创建依赖属性---静态只读
        public static readonly DependencyProperty ImageMouseEnterProperty;
        public static readonly DependencyProperty ImageMouseLeaveProperty;
        public static readonly DependencyProperty ImageCurrentProperty;
        public static readonly DependencyProperty TextProperty;

        // 2.注册依赖属性 .  运行顺序：静态-》普通
        static DediImageButton()
        {
            ImageMouseEnterProperty = DependencyProperty.Register("ImageMouseEnter", typeof(ImageSource), typeof(DediImageButton), new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(PropertyChange)));

            ImageMouseLeaveProperty = DependencyProperty.Register("ImageMouseLeave", typeof(ImageSource), typeof(DediImageButton), new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(PropertyChange)));

            ImageCurrentProperty = DependencyProperty.Register("ImageCurrent", typeof(ImageSource), typeof(DediImageButton), new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(PropertyChange)));

            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DediImageButton), new PropertyMetadata("", new PropertyChangedCallback(PropertyChange))); 

            // 给当前值附初始值
            // ImageButton3.CurrentImageProperty = ImageButton3.ImageMouseLeaveProperty;
            // DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageSource), new FrameworkPropertyMetadata(typeof(ImageSource)));
        }

        // 回调函数 
        private static void PropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //if (e.Property.Name == "ImageMouseEnter")
            //{
            //    // MessageBox.Show("ImageMouseEnter");


            //}
            //if (e.Property.Name == "ImageMouseLeave" /*&&　isFirst*/)
            //{
            //    // MessageBox.Show("ImageMouseLeave");
            //    //CurrentImageProperty = ImageMouseLeaveProperty;
            //    //isFirst = false;

            //}
            //if (e.Property.Name == "CurrentImage")
            //{
            //    // MessageBox.Show("ImageMouseLeave");
            //}
        }

        // 3.包装属性
        /// <summary>
        /// 鼠标进入时的图片
        /// </summary>
        public ImageSource ImageMouseEnter
        {
            get
            {
                return GetValue(DediImageButton.ImageMouseEnterProperty) as ImageSource;
            }
            set
            {
                SetValue(DediImageButton.ImageMouseEnterProperty, value);
            }
        }

        /// <summary>
        /// 鼠标离开时的图片
        /// </summary>
        public ImageSource ImageMouseLeave
        {
            get
            {
                return GetValue(DediImageButton.ImageMouseLeaveProperty) as ImageSource;
            }
            set
            {
                SetValue(DediImageButton.ImageMouseLeaveProperty, value);
            }
        }

        public ImageSource ImageCurrent
        {
            get
            {
                return GetValue(DediImageButton.ImageCurrentProperty) as ImageSource;
            }
            set
            {
                SetValue(DediImageButton.ImageCurrentProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return GetValue(DediImageButton.TextProperty) as string;
            }
            set
            {
                SetValue(DediImageButton.TextProperty, value);
            }
        }
        #endregion

        Thickness 按钮初始位置;
        private void ImageButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsEnabled == true)
            {
                ImageCurrent = ImageMouseEnter;
            }
        }
        private void ImageButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsEnabled == true)
            {
                ImageCurrent = ImageMouseLeave;
                Margin = 按钮初始位置;
            }
        }
        private void ImageButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsEnabled == true)
            {
                Margin = new Thickness(按钮初始位置.Left, 按钮初始位置.Top + 2, 按钮初始位置.Right, 按钮初始位置.Bottom);

            }
        }
        private void ImageButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsEnabled == true)
            {
                Margin = 按钮初始位置;
            }
        }

        private void ImageButton_Loaded(object sender, RoutedEventArgs e)
        {
            按钮初始位置 = Margin;
        }
    }
}
