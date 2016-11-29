using System.Windows;
using System.Windows.Controls;
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
            //MessageBox.Show(UCImage.Source.ToString());
        }
    }
}
