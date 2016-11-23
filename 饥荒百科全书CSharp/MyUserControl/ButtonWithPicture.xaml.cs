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

namespace 饥荒百科全书CSharp.MyUserControl
{
    /// <summary>
    /// ButtonWithPicture.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonWithPicture : UserControl
    {
        public ButtonWithPicture(string imageSource, string imageSourceSecond="")
        {
            InitializeComponent();
            if (imageSourceSecond == "")
                UCImage.Source = RSN.PictureShortName(RSN.ShortName(imageSource));
            else
                UCImage.Source = RSN.PictureShortName(RSN.ShortName(imageSource, imageSourceSecond));
        }
    }
}
