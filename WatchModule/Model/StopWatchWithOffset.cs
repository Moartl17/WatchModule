using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchModule.ViewModels;

namespace WatchModule.Model
{
    public class StopWatchWithOffset : BaseViewModel
    {
        private Stopwatch _stopwatch = null;
        TimeSpan _offsetTimeSpan;

        public StopWatchWithOffset()
        {
            _offsetTimeSpan = new TimeSpan(0, 0, 0);
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public TimeSpan ElapsedTimeSpan
        {
            get
            {
                return _stopwatch.Elapsed + _offsetTimeSpan;
            }
            set
            {
                _stopwatch.Reset();
                _offsetTimeSpan = value;
                OnPropertyChanged("ElapsedTimeSpan");
            }
        }

        public void UpdateElapsedTimeSpan()
        {
            OnPropertyChanged("ElapsedTimeSpan");
        }

        public bool IsRunning { get { return _stopwatch.IsRunning; } }
    }
}
