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
using 饥荒百科全书CSharp.Class;


namespace 饥荒百科全书CSharp.MyUserControl.DedicatedServer
{
    /// <summary>
    /// DediModBox.xaml 的交互逻辑
    /// </summary>
    public partial class DediModBox : UserControl
    {
        public DediModBox()
        {
            InitializeComponent();
        }

        //private void UCCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    if(UCCheckBox.IsChecked == false)
        //    {
        //        UCEnableLabel.Foreground = Brushes.Black;
        //        UCEnableLabel.Content = "停用";
        //    }
        //    else
        //    {
        //        UCEnableLabel.Foreground = Brushes.Green;
        //        UCEnableLabel.Content = "启用";
        //    }
        //}

        private void Border_GotFocus(object sender, RoutedEventArgs e)
        {
            // 有bug,在vs里测试没有问题的,但是放到外面,当勾选某个mod时,会出问题.我不会弄..
            //UC.Background = new ImageBrush(RSN.PictureShortName(@"../../Resources/DedicatedServer/D_mp_mod_bg.png"));
        }

        private void Border_LostFocus(object sender, RoutedEventArgs e)
        {
            // 有bug,在vs里测试没有问题的,但是放到外面,当勾选某个mod时,会出问题.
            //UC.Background = new ImageBrush(RSN.PictureShortName(@"../../Resources/DedicatedServer/D_mp_mod_bg_unchecked.png"));
        }
    }
}
