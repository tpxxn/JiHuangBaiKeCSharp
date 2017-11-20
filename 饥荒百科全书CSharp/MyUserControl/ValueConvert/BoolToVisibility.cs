using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace 饥荒百科全书CSharp.MyUserControl.ValueConvert
{
    internal class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            //return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }
            if ((bool)value)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
            //return value != null && (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            //return (Visibility)value == Visibility.Visible;
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return Visibility.Visible;
            //return value != null && (Visibility)value == Visibility.Visible;
        }
    }
}
