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
        #region 【依赖属性】
        // 依赖属性 参考网址：http://www.cnblogs.com/luluping/archive/2011/05/06/2039489.html
        // 没怎么看懂.... http://blog.csdn.net/luxiaoyu_sdc/article/details/6173758
        // http://blog.csdn.net/rabbitsoft_1987/article/details/18677067

        // 1. 创建依赖属性---静态只读
        /// <summary>
        /// 鼠标进入的图片
        /// </summary>
        public static readonly DependencyProperty ImageMouseEnterProperty;

        /// <summary>
        /// 鼠标离开的图片
        /// </summary>
        public static readonly DependencyProperty ImageMouseLeaveProperty;
        /// <summary>
        /// 显示的文本
        /// </summary>
        public static readonly DependencyProperty TextProperty;

        //private ImageSource imageMouseEnter;
        //private ImageSource imageMouseLeave;

        // 2.注册依赖属性 .  运行顺序：静态-》普通
        static DediImageButton()
        {
            DediImageButton.ImageMouseEnterProperty =
            DependencyProperty.Register("ImageMouseEnter", typeof(ImageSource), typeof(DediImageButton),
                   new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(PropertyChange)));
            //注意：一个字母写错，搞了2小时。。
            DediImageButton.ImageMouseLeaveProperty =
            DependencyProperty.Register("ImageMouseLeave", typeof(ImageSource), typeof(DediImageButton),
                new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(PropertyChange)));

            DediImageButton.TextProperty = /*ImageButton4.ImageMouseLeaveProperty;*/
            DependencyProperty.Register("Text", typeof(string), typeof(DediImageButton),
                    new PropertyMetadata("", new PropertyChangedCallback(PropertyChange)));



            // 给当前值附初始值
            // ImageButton4.CurrentImageProperty = ImageButton4.ImageMouseLeaveProperty;
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
                return base.GetValue(DediImageButton.ImageMouseEnterProperty) as ImageSource;
            }
            set
            {
                base.SetValue(DediImageButton.ImageMouseEnterProperty, value);
            }
        }

        /// <summary>
        /// 鼠标离开时的图片
        /// </summary>
        public ImageSource ImageMouseLeave
        {
            get
            {
                return base.GetValue(DediImageButton.ImageMouseLeaveProperty) as ImageSource;
            }
            set
            {
                base.SetValue(DediImageButton.ImageMouseLeaveProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return base.GetValue(DediImageButton.TextProperty) as string;
            }
            set
            {
                base.SetValue(DediImageButton.TextProperty, value);
            }
        }
        #endregion
    }
}
