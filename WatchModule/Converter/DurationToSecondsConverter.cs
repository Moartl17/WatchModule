using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WatchModule.Converter
{
    public class DurationToSecondsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool runDownwards = (values[1] == null) ? false : System.Convert.ToBoolean(values[1].ToString());
            bool isCurrentlyPause = (values[2] == null) ? false : System.Convert.ToBoolean(values[2].ToString());
            bool isInOvertime = false;
            TimeSpan overtimePeriodDuration = new TimeSpan();

            if (values.Length == 7)
            {
                overtimePeriodDuration = (TimeSpan)values[6];
                isInOvertime = System.Convert.ToBoolean(values[5].ToString());
            }

            if (values[0] != null)
            {
                TimeSpan timeSpan = (TimeSpan)values[0];
                if (timeSpan != null)
                {
                    if (!runDownwards)
                        return timeSpan.ToString("ss");
                    else
                    {
                        TimeSpan totalEndDuration = (TimeSpan)values[3];
                        if (isCurrentlyPause)
                            totalEndDuration = (TimeSpan)values[4];
                        else if (isInOvertime)
                            totalEndDuration = overtimePeriodDuration;

                        return totalEndDuration.Subtract(new TimeSpan(0, timeSpan.Minutes, timeSpan.Seconds)).ToString("ss");
                    }
                }
            }

            return "00";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
