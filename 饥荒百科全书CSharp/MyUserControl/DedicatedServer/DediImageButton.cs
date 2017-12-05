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
    /// 【2016.12.5】 
    /// 
    /// ImageButton4继承Button，额外添加了3个属性
    /// 
    /// 1.鼠标进入图片 2鼠标离开图片 3.按钮上的文本
    /// 
    /// 配合Controls_Themes.xaml样式文件使用
    /// 
    /// 不足的是样式文件中，对图片和文本更多细节的设置没有弄，只是简单地居中
    /// </summary>
    public class DediImageButton : Button
    {
        // private static bool isFirst = true;
        // 依赖属性 参考网址：http://www.cnblogs.com/luluping/archive/2011/05/06/2039489.html
        // 没怎么看懂.... http://blog.csdn.net/luxiaoyu_sdc/article/details/6173758
        // http://blog.csdn.net/rabbitsoft_1987/article/details/18677067

        #region 属性：ImageMouseEnter
            
        public ImageSource ImageMouseEnter
        {
            get => GetValue(ImageMouseEnterProperty) as ImageSource;
            set => SetValue(ImageMouseEnterProperty, value);
        }

        public static readonly DependencyProperty ImageMouseEnterProperty =
            DependencyProperty.Register("ImageMouseEnter", typeof(ImageSource), typeof(DediImageButton),
                new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(PropertyChange)));

        #endregion

        #region 属性：ImageMouseLeave
        
        public ImageSource ImageMouseLeave
        {
            get => GetValue(ImageMouseLeaveProperty) as ImageSource;
            set => SetValue(ImageMouseLeaveProperty, value);
        }
        
        public static readonly DependencyProperty ImageMouseLeaveProperty =
            DependencyProperty.Register("ImageMouseLeave", typeof(ImageSource), typeof(DediImageButton),
                new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(PropertyChange)));

        #endregion

        #region 属性：Text

        public string Text
        {
            get => GetValue(TextProperty) as string;
            set => SetValue(TextProperty, value);
        }
        
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DediImageButton),
                new PropertyMetadata("", new PropertyChangedCallback(PropertyChange)));

        #endregion
        
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

        static DediImageButton()
        {
            // 给当前值附初始值
            // ImageButton4.CurrentImageProperty = ImageButton4.ImageMouseLeaveProperty;
            // DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageSource), new FrameworkPropertyMetadata(typeof(ImageSource)));
        }
    }
}
