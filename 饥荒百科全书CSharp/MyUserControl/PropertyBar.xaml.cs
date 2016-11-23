using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace 饥荒百科全书CSharp.MyUserControl
{
    /// <summary>
    /// PropertyBar 属性条
    /// </summary>
    public partial class PropertyBar : UserControl
    {
        public PropertyBar(bool isNegativeNum = false)
        {
            InitializeComponent();
            if (isNegativeNum == true)
            {
                UCProgressBarAngle.Angle = 180;
                UCTextBlockValue.Foreground = Brushes.Red;
            }
        }
    }
}
