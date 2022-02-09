using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WatchModule.Converter
{
    public class DurationToDisplayableDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                TimeSpan timeSpan = (TimeSpan)value;
                if (timeSpan != null)
                {
                    return string.Format("{0}:{1}", timeSpan.ToString("mm"), timeSpan.ToString("ss"));
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string[] values = value.ToString().Split(':');
                return new TimeSpan(0, System.Convert.ToInt32(values[0]), System.Convert.ToInt32(values[1]));
            }

            return null;
        }
    }
}
