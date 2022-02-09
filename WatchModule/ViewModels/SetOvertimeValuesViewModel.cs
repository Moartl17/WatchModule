using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatchModule.ViewModels
{
    public class SetOvertimeValuesViewModel:BaseViewModel
    {
        private TimeSpan _LengthOfOnePeriod;
        public TimeSpan LengthOfOnePeriod
        {
            get { return _LengthOfOnePeriod; }
            set
            {
                if (_LengthOfOnePeriod != value)
                {
                    _LengthOfOnePeriod = value;
                    OnPropertyChanged("LengthOfOnePeriod");
                }
            }
        }

        private int _NumberOfOvertimePeriods;
        public int NumberOfOvertimePeriods
        {
            get { return _NumberOfOvertimePeriods; }
            set
            {
                if (_NumberOfOvertimePeriods != value)
                {
                    _NumberOfOvertimePeriods = value;
                    OnPropertyChanged("NumberOfOvertimePeriods");
                }
            }
        }

    }
}
