using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace 饥荒百科全书CSharp
{
    public class AnimationClass
    {
        public void Animation(UIElement obj, double from, double to, DependencyProperty property, double time=0.1)
        {
            var widthAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
            };
            obj.BeginAnimation(property, widthAnimation);
        }

        public void Animation(ColumnDefinition obj, double from, double to, DependencyProperty property, double time = 0.1)
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
