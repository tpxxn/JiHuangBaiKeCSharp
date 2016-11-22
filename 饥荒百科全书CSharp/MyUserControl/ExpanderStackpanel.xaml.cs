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
            UCTextBlock.Text = textblockText;
            if (imageSource != "")
            {
                Thickness T = new Thickness();
                T.Left = 32;
                UCTextBlock.Margin = T;
                UCImage.Visibility = Visibility.Visible;
                UCImage.Source = RSN.PictureShortName(RSN.ShortName(imageSource));
            }
        }
    }
}
