using System.Windows;
using System.Windows.Controls;
using 饥荒百科全书CSharp.Class;

namespace 饥荒百科全书CSharp.MyUserControl
{
    /// <summary>
    /// ExpanderStackpanel 带Stackpanel的Expander
    /// </summary>
    public partial class ExpanderStackpanel : UserControl
    {
        public ExpanderStackpanel(string textblockText = "", string imageSource = "")
        {
            InitializeComponent();
            UcTextBlock.Text = textblockText;
            if (imageSource != "")
            {
                var T = new Thickness { Left = 32 };
                UcTextBlock.Margin = T;
                UcImage.Visibility = Visibility.Visible;
                UcImage.Source = RSN.PictureShortName(imageSource);
            }
        }
    }
}
