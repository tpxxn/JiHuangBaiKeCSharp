using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace 饥荒百科全书CSharp.Class
{
    class ControlVisibility
    {
        public void ControlVisibilityCollapsed(bool Visi, params UIElement[] obj)
        {

            foreach (UIElement i in obj)
            {
                if (Visi == false)
                {
                    i.Visibility = Visibility.Visible;
                }
                else
                {
                    i.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}