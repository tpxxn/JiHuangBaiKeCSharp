using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace 饥荒百科全书CSharp.Class
{
    static class Visi
    {
        /// <summary>
        /// 控件可视性设置
        /// </summary>
        /// <param name="Visi">是否隐藏</param>
        /// <param name="obj">控件Name</param>
        public static void VisiCol(bool Visi, params UIElement[] obj)
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