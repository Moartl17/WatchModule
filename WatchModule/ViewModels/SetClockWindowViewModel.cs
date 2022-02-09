using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WatchModule.ViewModels
{
    public class SetClockWindowViewModel : BaseViewModel
    {
        bool _runDownwards = false;
        TimeSpan? _totalTime = null;


        public SetClockWindowViewModel(TimeSpan currentTime, bool runDownwards = false, TimeSpan? totalTime = null)
        {
            _runDownwards = runDownwards;
            _totalTime = totalTime;

            //  ME 31.10.2018: Auch wenn runDownwards aktiv ist zeige hier immer nur die aufsteigende Zeit pro Drittel an!
            //if (!runDownwards || totalTime == null)
            //{
            Minutes = currentTime.Minutes;
            Seconds = currentTime.Seconds;
            //}

            //else
            //{
            //    TimeSpan displayTime = totalTime.Value.Subtract(currentTime);
            //    Minutes = displayTime.Minutes;
            //    Seconds = displayTime.Seconds;
            //}
        }

        private int _Minutes;
        public int Minutes
        {
            get { return _Minutes; }
            set
            {
                if (_Minutes != value)
                {
                    _Minutes = value;
                    OnPropertyChanged("Minutes");
                }
            }
        }

        private int _Seconds;
        public int Seconds
        {
            get { return _Seconds; }
            set
            {
                if (_Seconds != value)
                {
                    _Seconds = value;
                    OnPropertyChanged("Seconds");
                }
            }
        }

        public int MinutesCalculated
        {
            get
            {
                if (!_runDownwards || _totalTime == null)
                    return Minutes;
                else
                {
                    return _totalTime.Value.Minutes - Minutes;
                }
            }

        }

        public int SecondsCalculated
        {
            get
            {
                if (!_runDownwards || _totalTime == null)
                    return Seconds;
                else
                {
                    return _totalTime.Value.Seconds - Seconds;
                }
            }

        }

    }
}
