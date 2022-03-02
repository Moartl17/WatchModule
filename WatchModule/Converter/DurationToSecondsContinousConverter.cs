using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WatchModule.Converter
{
   public class DurationToSecondsContinousConverter : IMultiValueConverter
    {
       public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
           bool isCurrentlyPause = (values[1] == null) ? false : System.Convert.ToBoolean(values[1].ToString());
           TimeSpan periodDuration = (TimeSpan)values[2];
           int currentPeriod = Int32.Parse(values[3].ToString());
           bool isInOvertime = System.Convert.ToBoolean(values[4].ToString());
           TimeSpan overtimePeriodDuration = (TimeSpan)values[5];
           int currentOverTimePeriod = Int32.Parse(values[6].ToString());

           if (values[0] != null)
           {
               TimeSpan timeSpan = (TimeSpan)values[0];
               if (timeSpan != null)
               {
                   var expiredTime = new TimeSpan(0, 0, 0);

                   for (int i = 1; i < currentPeriod; i++)
                   {
                       expiredTime = expiredTime.Add(periodDuration);
                   }

                   #region Mit Overtime

                   if (isInOvertime)
                   {
                       //   Einmal noch das letzte Drittel hinzu zählen
                       expiredTime = expiredTime.Add(periodDuration);

                       for (int i = 1; i < currentOverTimePeriod; i++)
                       {
                           expiredTime = expiredTime.Add(overtimePeriodDuration);
                       }
                   }

                   #endregion

                   //Debug.WriteLine("From Seconds Converter: EllapseTime = " + timeSpan.Add(expiredTime).ToString());
                   return (timeSpan.Add(expiredTime)).ToString("ss");
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
