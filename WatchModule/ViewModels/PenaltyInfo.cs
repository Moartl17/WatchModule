using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatchModule.Model;

namespace WatchModule.ViewModels
{
    public class PenaltyInfo : BaseViewModel
    {
        private TimeSpan _Duration;
        public TimeSpan Duration
        {
            get { return _Duration; }
            set
            {
                if (_Duration != value)
                {
                    _Duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private TimeSpan _RemainingDuration;
        public TimeSpan RemainingDuration
        {
            get { return _RemainingDuration; }
            set
            {
                if (_RemainingDuration != value)
                {
                    _RemainingDuration = value;
                    OnPropertyChanged("RemainingDuration");
                }
            }
        }


        public StopWatchWithOffset Stopwatch { get; set; }

        private int _PlayerNumber;
        public int PlayerNumber
        {
            get { return _PlayerNumber; }
            set
            {
                if (_PlayerNumber != value)
                {
                    _PlayerNumber = value;
                    OnPropertyChanged("PlayerNumber");
                }
            }
        }

        public TimeSpan PenaltyStartTime { get; set; }

        public bool IsHomeTeam { get; set; }
        public int Key { get; set; }
    }
}
