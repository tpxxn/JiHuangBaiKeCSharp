using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace 饥荒百科全书CSharp
{
    public partial class AnimationClass : Window
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
    }
}
