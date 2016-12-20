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

        private void UCCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if(UCCheckBox.IsChecked == false)
            {
                UCGrid.Background = new ImageBrush(RSN.PictureShortName(@"/饥荒百科全书CSharp;component/Resources/DedicatedServer/D_mp_mod_bg_unchecked.png"));
            }
            else
            {
                UCGrid.Background = new ImageBrush(RSN.PictureShortName(@"/饥荒百科全书CSharp;component/Resources/DedicatedServer/D_mp_mod_bg.png"));
            }
        }
    }
}
