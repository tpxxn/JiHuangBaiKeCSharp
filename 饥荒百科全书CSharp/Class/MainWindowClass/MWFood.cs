using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowFood
    /// </summary>
    public partial class MainWindow : Window
    {

        //WrapPanel_Right_Food内Expander.Width设置为WrapPanel_Right_Food.ActualWidth
        private void WrapPanel_Right_Food_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (ExpanderStackpanel expanderStackpanel in WrapPanel_Right_Food.Children)
            {
                expanderStackpanel.Width = (int)WrapPanel_Right_Food.ActualWidth;
            }
        }
    }
}
