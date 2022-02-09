using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WatchModule.Converter
{
        public class IsClockRunningToColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                bool valueInScope = (bool)value;
                return valueInScope ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }

