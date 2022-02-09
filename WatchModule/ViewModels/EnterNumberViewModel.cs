using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WatchModule.ViewModels
{
    public class EnterNumberViewModel : BaseViewModel
    {

        public EnterNumberViewModel(int numberOfMinutes)
        {
            CustomMinutes = numberOfMinutes;
            CustomSeconds = 0;
            if (numberOfMinutes == 0)
                CustomTimeEntryVisibility = Visibility.Visible;
            else
                CustomTimeEntryVisibility = Visibility.Hidden;
        }

        private int _NumberOfPlayer;
        public int NumberOfPlayer
        {
            get { return _NumberOfPlayer; }
            set
            {
                if (_NumberOfPlayer != value)
                {
                    _NumberOfPlayer = value;
                    OnPropertyChanged("NumberOfPlayer");
                }
            }
        }

        private int _CustomMinutes;
        public int CustomMinutes
        {
            get { return _CustomMinutes; }
            set
            {
                if (_CustomMinutes != value)
                {
                    _CustomMinutes = value;
                    OnPropertyChanged("CustomMinutes");
                }
            }
        }

        private int _CustomSeconds;
        public int CustomSeconds
        {
            get { return _CustomSeconds; }
            set
            {
                if (_CustomSeconds != value)
                {
                    _CustomSeconds = value;
                    OnPropertyChanged("CustomSeconds");
                }
            }
        }


        private Visibility _CustomTimeEntryVisibility;
        public Visibility CustomTimeEntryVisibility
        {
            get { return _CustomTimeEntryVisibility; }
            set
            {
                if (_CustomTimeEntryVisibility != value)
                {
                    _CustomTimeEntryVisibility = value;
                    OnPropertyChanged("CustomTimeEntryVisibility");
                }
            }
        }


    }
}
