using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WatchModule.Converter
{
    public class CurrentPeriodConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int currentPeriod = Int32.Parse(values[0].ToString());
            bool isInOvertime = System.Convert.ToBoolean(values[1].ToString());
            int currentOverTimePeriod = Int32.Parse(values[2].ToString());

            return isInOvertime ? (currentPeriod + currentOverTimePeriod).ToString() : currentPeriod.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
