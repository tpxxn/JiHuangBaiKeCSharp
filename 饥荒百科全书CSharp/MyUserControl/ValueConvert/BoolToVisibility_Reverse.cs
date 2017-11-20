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
    internal class BoolToVisibilityReverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            //return !(bool)value ? Visibility.Visible : Visibility.Collapsed;
            return value != null && !(bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            //return (Visibility)value == Visibility.Collapsed;
            return value != null && (Visibility)value == Visibility.Collapsed;
        }
    }
}
