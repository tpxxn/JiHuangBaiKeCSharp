using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace 饥荒百科全书CSharp.Class
{
    static class Animation
    {

        public static void Anim(UIElement obj, double from, double to, DependencyProperty property, double time=0.1)
        {
            var widthAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
            };
            obj.BeginAnimation(property, widthAnimation);
        }

        public static void Anim(ColumnDefinition obj, double from, double to, DependencyProperty property, double time = 0.1)
        {
            var widthAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
            };
            obj.BeginAnimation(property, widthAnimation);
        }
    }
}
