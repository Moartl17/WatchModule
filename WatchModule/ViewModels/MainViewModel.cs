using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WatchModule.Model;
using System.Windows.Forms;
using WatchModule.Views;
using System.Drawing;

namespace WatchModule.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        DispatcherTimer dt = new DispatcherTimer();

        #region Properties

        private int _HomeGoals;
        public int HomeGoals
        {
            get { return _HomeGoals; }
            set
            {
                if (_HomeGoals != value)
                {
                    _HomeGoals = value;
                    OnPropertyChanged("HomeGoals");
                }
            }
        }

        private int _AwayGoals;
        public int AwayGoals
        {
            get { return _AwayGoals; }
            set
            {
                if (_AwayGoals != value)
                {
                    _AwayGoals = value;
                    OnPropertyChanged("AwayGoals");
                }
            }
        }


        private Dictionary<int, PenaltyInfo> allPenalties;
        public Dictionary<int, PenaltyInfo> AllPenalties
        {
            get { return allPenalties; }
            set
            {
                allPenalties = value;
                OnPropertyChanged("HomePenalties");
                OnPropertyChanged("AwayPenalties");
            }
        }

        public ObservableCollection<PenaltyInfo> HomePenalties
        {
            get
            {
                return new ObservableCollection<PenaltyInfo>(AllPenalties.Where(item => item.Value.IsHomeTeam).OrderBy(item => item.Key)
                .Select(item => item.Value));
            }
        }

        public ObservableCollection<PenaltyInfo> AwayPenalties
        {
            get
            {
                return new ObservableCollection<PenaltyInfo>(AllPenalties.Where(item => !item.Value.IsHomeTeam).OrderBy(item => item.Key)
                    .Select(item => item.Value));
            }
        }

        private PenaltyInfo _SelectedHomePenalty;
        public PenaltyInfo SelectedHomePenalty
        {
            get { return _SelectedHomePenalty; }
            set
            {
                if (_SelectedHomePenalty != value)
                {
                    _SelectedHomePenalty = value;
                    OnPropertyChanged("SelectedHomePenalty");
                }
            }
        }

        private PenaltyInfo _SelectedAwayPenalty;
        public PenaltyInfo SelectedAwayPenalty
        {
            get { return _SelectedAwayPenalty; }
            set
            {
                if (_SelectedAwayPenalty != value)
                {
                    _SelectedAwayPenalty = value;
                    OnPropertyChanged("SelectedAwayPenalty");
                }
            }
        }

        private bool _runDownwardsStoredValue;

        private bool _IsCurrentlyPause;
        public bool IsCurrentlyPause
        {
            get { return _IsCurrentlyPause; }
            set
            {
                if (_IsCurrentlyPause != value)
                {
                    if (value)
                    {
                        _runDownwardsStoredValue = RunDownwards;
                        RunDownwards = true;
                    }
                    else
                    {
                        RunDownwards = _runDownwardsStoredValue;
                    }
                    _IsCurrentlyPause = value;
                    OnPropertyChanged("IsCurrentlyPause");
                }
            }
        }

        private bool _soundAlreadyPlayed;

        private bool _RunDownwards;
        public bool RunDownwards
        {
            get { return _RunDownwards; }
            set
            {
                if (_RunDownwards != value)
                {
                    _RunDownwards = value;
                    OnPropertyChanged("RunDownwards");
                    OnPropertyChanged("RunDownwardsInverse");
                    OnPropertyChanged("Minutes");
                    OnPropertyChanged("Seconds");
                }
            }
        }

        public bool RunDownwardsInverse
        {
            get { return !RunDownwards; }
        }


        private TimeSpan _PeriodDuration;
        public TimeSpan PeriodDuration
        {
            get
            {
                if (_PeriodDuration < new TimeSpan(0, 0, 1))
                    _PeriodDuration = new TimeSpan(0, 20, 0);
                //if (_PeriodDuration < new TimeSpan(0, 0, 1))
                //    _PeriodDuration = new TimeSpan(0, 0, 3);
                return _PeriodDuration;
            }
            set
            {
                if (_PeriodDuration != value)
                {
                    _PeriodDuration = value;
                    OnPropertyChanged("PeriodDuration");
                }
            }
        }

        private TimeSpan _PauseDuration;
        public TimeSpan PauseDuration
        {
            get
            {
                if (_PauseDuration < new TimeSpan(0, 0, 1))
                    _PauseDuration = new TimeSpan(0, 10, 0);
                //if (_PauseDuration < new TimeSpan(0, 0, 1))
                //    _PauseDuration = new TimeSpan(0, 0, 2);
                return _PauseDuration;
            }
            set
            {
                if (_PauseDuration != value)
                {
                    _PauseDuration = value;
                    OnPropertyChanged("PauseDuration");
                }
            }
        }


        private int _CurrentPeriod;
        public int CurrentPeriod
        {
            get
            {
                return _CurrentPeriod;
            }
            set
            {
                if (_CurrentPeriod != value)
                {
                    _CurrentPeriod = value;
                    OnPropertyChanged("CurrentPeriod");
                    OnPropertyChanged("IsOvertimeOptionEnabled");
                }
            }
        }

        private int _NumberOfPeriods;
        public int NumberOfPeriods
        {
            get
            {
                if (_NumberOfPeriods < 1)
                    _NumberOfPeriods = 3;
                return _NumberOfPeriods;
            }
            set
            {
                if (_NumberOfPeriods != value)
                {
                    _NumberOfPeriods = value;
                    OnPropertyChanged("NumberOfPeriods");
                    OnPropertyChanged("IsOvertimeOptionEnabled");
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

        private int _CurrentOvertimePeriod;
        public int CurrentOvertimePeriod
        {
            get { return _CurrentOvertimePeriod; }
            set
            {
                if (_CurrentOvertimePeriod != value)
                {
                    _CurrentOvertimePeriod = value;
                    OnPropertyChanged("CurrentOvertimePeriod");
                }
            }
        }


        private TimeSpan _OvertimeDuration;
        public TimeSpan OvertimeDuration
        {
            get { return _OvertimeDuration; }
            set
            {
                if (_OvertimeDuration != value)
                {
                    _OvertimeDuration = value;
                    OnPropertyChanged("OvertimeDuration");
                }
            }
        }

        public bool IsOvertimeOptionEnabled
        {
            get { return CurrentPeriod >= NumberOfPeriods; }

        }



        private bool _IsCurrentlyInOvertime;
        public bool IsCurrentlyInOvertime
        {
            get { return _IsCurrentlyInOvertime; }
            set
            {
                if (_IsCurrentlyInOvertime != value)
                {
                    _IsCurrentlyInOvertime = value;

                    if (value)
                    {
                        //CurrentPeriod = NumberOfPeriods;
                        CurrentOvertimePeriod = 1;
                        SetClock(new TimeSpan(0, 0, 0));
                    }
                    else
                    {
                        CurrentOvertimePeriod = 0;
                    }
                    OnPropertyChanged("IsCurrentlyInOvertime");
                }
            }
        }


        private string _HomeTeam;
        public string HomeTeam
        {
            get { return _HomeTeam; }
            set
            {
                if (_HomeTeam != value)
                {
                    _HomeTeam = value;
                    OnPropertyChanged("HomeTeam");
                }
            }
        }

        private string _AwayTeam;
        public string AwayTeam
        {
            get { return _AwayTeam; }
            set
            {
                if (_AwayTeam != value)
                {
                    _AwayTeam = value;
                    OnPropertyChanged("AwayTeam");
                }
            }
        }

        private Visibility _TimeoutVisibility;
        public Visibility TimeoutVisibility
        {
            get { return _TimeoutVisibility; }
            set
            {
                if (_TimeoutVisibility != value)
                {
                    _TimeoutVisibility = value;
                    IsTimeoutVisible = value == Visibility.Visible;
                    OnPropertyChanged("TimeoutVisibility");
                }
            }
        }

        private bool _IsTimeoutVisible;
        public bool IsTimeoutVisible
        {
            get { return _IsTimeoutVisible; }
            set
            {
                if (_IsTimeoutVisible != value)
                {
                    _IsTimeoutVisible = value;
                    OnPropertyChanged("IsTimeoutVisible");
                }
            }
        }


        private string _TimeoutSeconds;
        public string TimeoutSeconds
        {
            get { return _TimeoutSeconds; }
            set
            {
                if (_TimeoutSeconds != value)
                {
                    _TimeoutSeconds = value;
                    OnPropertyChanged("TimeoutSeconds");
                }
            }
        }

        private bool _IsInBambiniMode;
        public bool IsInBambiniMode
        {
            get { return _IsInBambiniMode; }
            set
            {
                if (_IsInBambiniMode != value)
                {
                    _IsInBambiniMode = value;
                    OnPropertyChanged("IsInBambiniMode");
                }
            }
        }

        private TimeSpan _BambiniShiftDuration;
        public TimeSpan BambiniShiftDuration
        {
            get
            {
                if (_BambiniShiftDuration < new TimeSpan(0, 0, 1))
                    _BambiniShiftDuration = new TimeSpan(0, 1, 30);
                return _BambiniShiftDuration;
            }
            set
            {
                if (_BambiniShiftDuration != value)
                {
                    _BambiniShiftDuration = value;
                    OnPropertyChanged("BambiniShiftDuration");
                }
            }
        }

        private bool _divideAllPenaltiesByTwo;
        public bool DivideAllPenaltiesByTwo
        {
            get { return _divideAllPenaltiesByTwo; }
            set
            {
                if (_divideAllPenaltiesByTwo != value)
                {
                    _divideAllPenaltiesByTwo = value;
                    OnPropertyChanged("DivideAllPenaltiesByTwo");
                }
            }
        }

        private bool _isClockRunning;
        public bool IsClockRunning
        {
            get
            {
                return Clock.IsRunning;
            }
            set
            {
                if (_isClockRunning != value)
                {
                    _isClockRunning = value;
                    OnPropertyChanged("IsClockRunning");
                }
            }
        }



        public StopWatchWithOffset Clock { get; set; }
        public StopWatchWithOffset TimeoutClock { get; set; }

        public bool FalseProperty { get { return false; } }

        #endregion

        public MainViewModel()
        {
            AllPenalties = new Dictionary<int, PenaltyInfo>();
            Clock = new StopWatchWithOffset();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = TimeSpan.FromMilliseconds(100);
            CurrentPeriod = 1;
            IsCurrentlyPause = false;
            HomeTeam = "Pflanz";
            AwayTeam = "Gegner";
            TimeoutVisibility = Visibility.Hidden;
            RunDownwards = true;
            OvertimeDuration = new TimeSpan(0, 10, 0);
            NumberOfOvertimePeriods = 2;
        }

        private int currentNumberOfShiftInBambiniMode = 0;

        private void dt_Tick(object sender, EventArgs e)
        {
            #region In der Auszeit

            if (TimeoutClock != null && TimeoutClock.IsRunning)
            {

                if (TimeoutClock.ElapsedTimeSpan > new TimeSpan(0, 0, 30))
                {
                    TimeoutClock.Stop();
                    PlaySound();
                    TimeoutVisibility = Visibility.Hidden;
                    CommandManager.InvalidateRequerySuggested();
                }

                else
                {
                    var residualTime = new TimeSpan(0, 0, 31).Subtract(TimeoutClock.ElapsedTimeSpan);
                    TimeoutSeconds = residualTime.ToString("ss");
                    TimeoutClock.UpdateElapsedTimeSpan();
                }
            }

            #endregion

            if (Clock.IsRunning)
            {
                #region  Normales Spiel läuft

                if (!IsCurrentlyPause)
                {
                    //  Update Penalties
                    var runningHomePens = AllPenalties.OrderBy(kvp => kvp.Key).Where(kvp => kvp.Value.IsHomeTeam).Take(2);
                    foreach (var homePen in runningHomePens)
                    {
                        homePen.Value.RemainingDuration = homePen.Value.Duration.Subtract(new TimeSpan(0, homePen.Value.Stopwatch.ElapsedTimeSpan.Minutes, homePen.Value.Stopwatch.ElapsedTimeSpan.Seconds));
                    }

                    var runningAwayPens = AllPenalties.OrderBy(kvp => kvp.Key).Where(kvp => !kvp.Value.IsHomeTeam).Take(2);
                    foreach (var awayPen in runningAwayPens)
                    {
                        awayPen.Value.RemainingDuration = awayPen.Value.Duration.Subtract(new TimeSpan(0, awayPen.Value.Stopwatch.ElapsedTimeSpan.Minutes, awayPen.Value.Stopwatch.ElapsedTimeSpan.Seconds));
                    }

                    var finishedPenalties = AllPenalties.Where(item => item.Value.Stopwatch.ElapsedTimeSpan >= item.Value.Duration).ToList();
                    bool isPenaltyFinished = false;
                    foreach (var item in finishedPenalties)
                    {
                        AllPenalties.Remove(item.Key);
                        isPenaltyFinished = true;
                    }
                    if (isPenaltyFinished)
                    {
                        AllPenalties.OrderBy(kvp => kvp.Key).Where(kvp => kvp.Value.IsHomeTeam).Take(2).ToList().ForEach(item =>
                            {
                                if (!item.Value.Stopwatch.IsRunning)
                                    item.Value.Stopwatch.Start();
                            });
                        AllPenalties.OrderBy(kvp => kvp.Key).Where(kvp => !kvp.Value.IsHomeTeam).Take(2).ToList().ForEach(item =>
                        {
                            if (!item.Value.Stopwatch.IsRunning)
                                item.Value.Stopwatch.Start();
                        });

                    }

                    OnPropertyChanged("HomePenalties");
                    OnPropertyChanged("AwayPenalties");

                    if (!IsCurrentlyInOvertime) // && !IsInBambiniMode)
                    {
                        //Debug.WriteLine("Ellapsed: " + Clock.ElapsedTimeSpan.ToString());

                        //  Check if period end time is reached
                        if (Clock.ElapsedTimeSpan >= PeriodDuration)
                        {
                            StopClock();
                            PlaySound();
                            if (CurrentPeriod < NumberOfPeriods)
                            {
                                IsCurrentlyPause = true;
                                SetClock(new TimeSpan(0, 0, 0));
                                StartClock(false);
                                _soundAlreadyPlayed = false;
                                if (IsInBambiniMode)
                                    currentNumberOfShiftInBambiniMode++;
                            }

                            StartAppSwitcherCommand.Execute(new object());

                            CommandManager.InvalidateRequerySuggested();
                        }

                        //  Check if in Bambini mode and shift is over
                        else if (IsInBambiniMode)
                        {
                            TimeSpan ellapsedTimeSpan = Clock.ElapsedTimeSpan;
                            //  Add check if more than one period
                            if (CurrentPeriod > 1)
                            {
                                for (int i = 1; i < CurrentPeriod; i++)
                                {
                                    ellapsedTimeSpan = ellapsedTimeSpan.Add(PeriodDuration);
                                }
                            }

                            if (ellapsedTimeSpan >= BambiniShiftDuration.Add(TimeSpan.FromTicks(currentNumberOfShiftInBambiniMode * BambiniShiftDuration.Ticks)))
                            {
                                StopClock();
                                PlaySound();
                                currentNumberOfShiftInBambiniMode++;
                                CommandManager.InvalidateRequerySuggested();
                            }
                        }
                    }
                    else if (IsCurrentlyInOvertime)
                    {

                        Debug.WriteLine("Ellapsed: " + Clock.ElapsedTimeSpan.ToString());
                        //  Check if overtime period end time is reached
                        if (Clock.ElapsedTimeSpan >= OvertimeDuration)
                        {
                            StopClock();
                            PlaySound();
                            if (CurrentPeriod + CurrentOvertimePeriod < NumberOfPeriods + NumberOfOvertimePeriods)
                            {
                                IsCurrentlyPause = true;
                                SetClock(new TimeSpan(0, 0, 0));
                                StartClock(false);
                                _soundAlreadyPlayed = false;
                                if (IsInBambiniMode)
                                    currentNumberOfShiftInBambiniMode++;
                            }

                            CommandManager.InvalidateRequerySuggested();
                        }
                    }

                    Clock.UpdateElapsedTimeSpan();
                }
                #endregion

                #region In der Pause

                else
                {
                    if (PauseDuration >= new TimeSpan(0, 5, 0) && !_soundAlreadyPlayed && Clock.ElapsedTimeSpan >= PauseDuration.Subtract(new TimeSpan(0, 1, 30)))
                    {
                        PlaySound();
                        _soundAlreadyPlayed = true;
                    }

                    if (Clock.ElapsedTimeSpan >= PauseDuration)
                    {
                        StopClock();
                        PlaySound();
                        IsCurrentlyPause = false;
                        if (!IsCurrentlyInOvertime)
                            CurrentPeriod++;
                        else
                            CurrentOvertimePeriod++;

                        //  Set Clock on Main Window to continous time over whole match
                        SetClock(new TimeSpan(0, 0, 0));

                        StopAppSwitcherCommand.Execute(new object());

                        CommandManager.InvalidateRequerySuggested();
                    }

                    Clock.UpdateElapsedTimeSpan();
                }
                #endregion
            }
        }



        #region Commands

        private ICommand _NewGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (_NewGameCommand == null)
                    _NewGameCommand = new RelayCommand(param => CreateNewGame(), param => CanCreateNewGame());
                return _NewGameCommand;
            }
        }

        private bool CanCreateNewGame()
        {
            return !Clock.IsRunning;
        }

        private void CreateNewGame()
        {
            var dialogResult = System.Windows.MessageBox.Show("Neues Spiel starten?", "Neues Spiel", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
            if (dialogResult == MessageBoxResult.OK)
            {
                AllPenalties = new Dictionary<int, PenaltyInfo>();
                SetClock(new TimeSpan(0, 0, 0));
                CurrentPeriod = 1;
                HomeTeam = "Pflanz";
                AwayTeam = "Gegner";
                HomeGoals = 0;
                AwayGoals = 0;
                IsCurrentlyPause = false;
                currentNumberOfShiftInBambiniMode = 0;
                IsCurrentlyInOvertime = false;
                NumberOfOvertimePeriods = 0;
                OvertimeDuration = new TimeSpan(0, 0, 0);
            }
        }


        private ICommand _StartStopCommand;
        public ICommand StartStopCommand
        {
            get
            {
                if (_StartStopCommand == null)
                    _StartStopCommand = new RelayCommand(param => StartStop(true), param => CanStartStop());
                return _StartStopCommand;
            }
        }

        private bool CanStartStop()
        {
            return TimeoutVisibility != Visibility.Visible;
        }

        private void StartStop(bool includePenalties)
        {
            if (Clock.IsRunning)
                StopClock();
            else
            {
                if (includePenalties && IsCurrentlyPause)
                    includePenalties = false;
                dt.Start();
                StartClock(includePenalties);
            }

        }

        private ICommand _AddHomeGoalCommand;
        public ICommand AddHomeGoalCommand
        {
            get
            {
                if (_AddHomeGoalCommand == null)
                    _AddHomeGoalCommand = new RelayCommand(param => ModifyGoals(1));
                return _AddHomeGoalCommand;
            }
        }

        private ICommand _SubtractHomeGoalCommand;
        public ICommand SubtractHomeGoalCommand
        {
            get
            {
                if (_SubtractHomeGoalCommand == null)
                    _SubtractHomeGoalCommand = new RelayCommand(param => ModifyGoals(-1));
                return _SubtractHomeGoalCommand;
            }
        }

        private void ModifyGoals(int number, bool isHomeTeam = true)
        {
            if (isHomeTeam)
                HomeGoals = HomeGoals + number;
            else
                AwayGoals = AwayGoals + number;
        }

        private ICommand _AddAwayGoalCommand;
        public ICommand AddAwayGoalCommand
        {
            get
            {
                if (_AddAwayGoalCommand == null)
                    _AddAwayGoalCommand = new RelayCommand(param => ModifyGoals(1, false));
                return _AddAwayGoalCommand;
            }
        }

        private ICommand _SubtractAwayGoalCommand;
        public ICommand SubtractAwayGoalCommand
        {
            get
            {
                if (_SubtractAwayGoalCommand == null)
                    _SubtractAwayGoalCommand = new RelayCommand(param => ModifyGoals(-1, false));
                return _SubtractAwayGoalCommand;
            }
        }

        private ICommand _AddHomeTwoMinPenaltyCommand;
        public ICommand AddHomeTwoMinPenaltyCommand
        {
            get
            {
                if (_AddHomeTwoMinPenaltyCommand == null)
                    _AddHomeTwoMinPenaltyCommand = new RelayCommand(param => AddPenalty(2));
                return _AddHomeTwoMinPenaltyCommand;
            }
        }

        private ICommand _AddHomeFiveMinPenaltyCommand;
        public ICommand AddHomeFiveMinPenaltyCommand
        {
            get
            {
                if (_AddHomeFiveMinPenaltyCommand == null)
                    _AddHomeFiveMinPenaltyCommand = new RelayCommand(param => AddPenalty(5));
                return _AddHomeFiveMinPenaltyCommand;
            }
        }

        private ICommand _AddHomeCustomPenaltyCommand;
        public ICommand AddHomeCustomPenaltyCommand
        {
            get
            {
                if (_AddHomeCustomPenaltyCommand == null)
                    _AddHomeCustomPenaltyCommand = new RelayCommand(param => AddPenalty(0, true));
                return _AddHomeCustomPenaltyCommand;
            }
        }

        private ICommand _AddAwayTwoMinPenaltyCommand;
        public ICommand AddAwayTwoMinPenaltyCommand
        {
            get
            {
                if (_AddAwayTwoMinPenaltyCommand == null)
                    _AddAwayTwoMinPenaltyCommand = new RelayCommand(param => AddPenalty(2, false));
                return _AddAwayTwoMinPenaltyCommand;
            }
        }

        private ICommand _AddAwayFiveMinPenaltyCommand;
        public ICommand AddAwayFiveMinPenaltyCommand
        {
            get
            {
                if (_AddAwayFiveMinPenaltyCommand == null)
                    _AddAwayFiveMinPenaltyCommand = new RelayCommand(param => AddPenalty(5, false));
                return _AddAwayFiveMinPenaltyCommand;
            }
        }

        private ICommand _AddAwayCustomPenaltyCommand;
        public ICommand AddAwayCustomPenaltyCommand
        {
            get
            {
                if (_AddAwayCustomPenaltyCommand == null)
                    _AddAwayCustomPenaltyCommand = new RelayCommand(param => AddPenalty(0, false));
                return _AddAwayCustomPenaltyCommand;
            }
        }



        private void AddPenalty(int minutes, bool isHomeTeam = true)
        {
            var selectPlayerView = new EnterNumberWindow();
            var viewModel = new EnterNumberViewModel(minutes);
            selectPlayerView.DataContext = viewModel;

            Screen s = Screen.PrimaryScreen;
            selectPlayerView.Top = s.WorkingArea.Top;

            var dialogResult = selectPlayerView.ShowDialog();

            if (dialogResult != null && dialogResult.Value)
            {
                var penaltyDuration = new TimeSpan(0, viewModel.CustomMinutes, viewModel.CustomSeconds);
                if (DivideAllPenaltiesByTwo && minutes != 0)
                {
                    if (minutes == 5)
                        penaltyDuration = new TimeSpan(0, 2, 30);
                    else
                        penaltyDuration = new TimeSpan(0, minutes / 2, 0);
                }

                var penaltyInfo = new PenaltyInfo
                {
                    Duration = penaltyDuration,
                    RemainingDuration = penaltyDuration,
                    Stopwatch = new StopWatchWithOffset(),
                    IsHomeTeam = isHomeTeam,
                    PlayerNumber = (selectPlayerView.DataContext as EnterNumberViewModel).NumberOfPlayer,
                    Key = GetKeyForNewPenalty(),
                    PenaltyStartTime = Clock.ElapsedTimeSpan
                };

                AddNewPenalty(penaltyInfo);
                OnPropertyChanged("HomePenalties");
                OnPropertyChanged("AwayPenalties");
            }
        }

        private ICommand _RemoveSelectedHomePenaltyCommand;
        public ICommand RemoveSelectedHomePenaltyCommand
        {
            get
            {
                if (_RemoveSelectedHomePenaltyCommand == null)
                    _RemoveSelectedHomePenaltyCommand = new RelayCommand(param => RemovePenalty(), param => CanRemovePenalty());
                return _RemoveSelectedHomePenaltyCommand;
            }
        }

        private ICommand _RemoveSelectedAwayPenaltyCommand;
        public ICommand RemoveSelectedAwayPenaltyCommand
        {
            get
            {
                if (_RemoveSelectedAwayPenaltyCommand == null)
                    _RemoveSelectedAwayPenaltyCommand = new RelayCommand(param => RemovePenalty(false), param => CanRemovePenalty(false));
                return _RemoveSelectedAwayPenaltyCommand;
            }
        }

        private bool CanRemovePenalty(bool isHomePenalty = true)
        {
            if (isHomePenalty)
                return SelectedHomePenalty != null;
            else
                return SelectedAwayPenalty != null;
        }

        private void RemovePenalty(bool isHomePenalty = true)
        {
            if (isHomePenalty)
            {
                AllPenalties.Remove(SelectedHomePenalty.Key);
                OnPropertyChanged("HomePenalties");
            }
            else
            {
                AllPenalties.Remove(SelectedAwayPenalty.Key);
                OnPropertyChanged("AwayPenalties");
            }
        }

        private ICommand _EditSelectedHomePenaltyCommand;
        public ICommand EditSelectedHomePenaltyCommand
        {
            get
            {
                if (_EditSelectedHomePenaltyCommand == null)
                    _EditSelectedHomePenaltyCommand = new RelayCommand(param => EditPenalty(SelectedHomePenalty), param => CanEditPenalty(SelectedHomePenalty));
                return _EditSelectedHomePenaltyCommand;
            }
        }

        private ICommand _EditSelectedAwayPenaltyCommand;
        public ICommand EditSelectedAwayPenaltyCommand
        {
            get
            {
                if (_EditSelectedAwayPenaltyCommand == null)
                    _EditSelectedAwayPenaltyCommand = new RelayCommand(param => EditPenalty(SelectedAwayPenalty), param => CanEditPenalty(SelectedAwayPenalty));
                return _EditSelectedAwayPenaltyCommand;
            }
        }

        private bool CanEditPenalty(PenaltyInfo selectedPenalty)
        {
            return !Clock.IsRunning && selectedPenalty != null;
        }

        private void EditPenalty(PenaltyInfo selectedPenalty)
        {
            TimeSpan tsToDisplay = selectedPenalty.Stopwatch.ElapsedTimeSpan;
            if (selectedPenalty.Stopwatch.ElapsedTimeSpan > new TimeSpan(0, 0, 0))
                tsToDisplay = tsToDisplay.Subtract(new TimeSpan(0, 0, 1));
            var vm = new SetClockWindowViewModel(tsToDisplay, true, selectedPenalty.Duration);
            SetClockWindow window = new SetClockWindow { DataContext = vm };
            var dialogResult = window.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                selectedPenalty.Stopwatch.ElapsedTimeSpan = new TimeSpan(0, vm.MinutesCalculated, vm.SecondsCalculated);
                selectedPenalty.RemainingDuration = selectedPenalty.Duration.Subtract(selectedPenalty.Stopwatch.ElapsedTimeSpan.Subtract(new TimeSpan(0,0,0,0,999)));
            }

            OnPropertyChanged("HomePenalties");
            OnPropertyChanged("AwayPenalties");
        }


        private ICommand _OpenDisplayWindowCommand;
        public ICommand OpenDisplayWindowCommand
        {
            get
            {
                if (_OpenDisplayWindowCommand == null)
                    _OpenDisplayWindowCommand = new RelayCommand(param => OpenDisplayWindow());
                return _OpenDisplayWindowCommand;
            }
        }

        DisplayWindow displayWindow;

        private void OpenDisplayWindow()
        {
            if (displayWindow == null)
            {
                displayWindow = new DisplayWindow();
                displayWindow.WindowWasClosed += displayWindow_WindowWasClosed;
                displayWindow.DataContext = this;

                displayWindow.WindowStartupLocation = WindowStartupLocation.Manual;

                Debug.Assert(SystemInformation.MonitorCount > 1);

                if (SystemInformation.MonitorCount > 1)
                {
                    Rectangle workingArea = Screen.AllScreens[1].WorkingArea;
                    displayWindow.Left = workingArea.Left;
                    displayWindow.Top = workingArea.Top;
                    displayWindow.Width = workingArea.Width;
                    displayWindow.Height = workingArea.Height;
                }

                //displayWindow.WindowState = WindowState.Maximized;
                displayWindow.WindowStyle = WindowStyle.None;
                //displayWindow.Topmost = true;
                displayWindow.Show();
            }

            else
                displayWindow.Activate();
        }

        void displayWindow_WindowWasClosed(object sender, EventArgs e)
        {
            displayWindow = null;
        }

        private ICommand _SetClockCommand;
        public ICommand SetClockCommand
        {
            get
            {
                if (_SetClockCommand == null)
                    _SetClockCommand = new RelayCommand(param => SetClock(), param => CanSet());
                return _SetClockCommand;
            }
        }

        private bool CanSet()
        {
            return !Clock.IsRunning && TimeoutVisibility != Visibility.Visible;
        }

        private void SetClock()
        {
            var timeToPassToSetClockWindow = Clock.ElapsedTimeSpan;
            if (Clock.ElapsedTimeSpan > new TimeSpan(0, 0, 0))
                timeToPassToSetClockWindow = Clock.ElapsedTimeSpan.Subtract(new TimeSpan(0, 0, 1));
            var vm = new SetClockWindowViewModel(timeToPassToSetClockWindow, RunDownwards, PeriodDuration);
            SetClockWindow window = new SetClockWindow { DataContext = vm };
            var dialogResult = window.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                //SetClock(new TimeSpan(0, vm.MinutesCalculated, vm.SecondsCalculated));
                SetClock(new TimeSpan(0, vm.Minutes, vm.Seconds));
            }
        }

        private ICommand _PlaySoundCommand;
        public ICommand PlaySoundCommand
        {
            get
            {
                if (_PlaySoundCommand == null)
                    _PlaySoundCommand = new RelayCommand(param => PlaySound());
                return _PlaySoundCommand;
            }
        }

        private void PlaySound()
        {
            SoundPlayer player = new SoundPlayer("Sounds/sirene.wav");
            player.Play();
        }

        private ICommand _PlayGoalHornCommand;
        public ICommand PlayGoalHornCommand
        {
            get
            {
                if (_PlayGoalHornCommand == null)
                    _PlayGoalHornCommand = new RelayCommand(param => PlayGoalSound());
                return _PlayGoalHornCommand;
            }
        }

        bool isSoundPlaying = false;
        SoundPlayer player;
        private void PlayGoalSound()
        {
            if (!isSoundPlaying)
            {
                player = new SoundPlayer("Sounds/goal.wav");
                player.Play();
                isSoundPlaying = true;
            }

            else
            {
                player.Stop();
                isSoundPlaying = false;
            }
        }


        private ICommand _TimeoutCommand;
        public ICommand TimeoutCommand
        {
            get
            {
                if (_TimeoutCommand == null)
                    _TimeoutCommand = new RelayCommand(param => TimeOut(), param => CanTimeOut());
                return _TimeoutCommand;
            }
        }

        private bool CanTimeOut()
        {
            return !Clock.IsRunning;
        }

        private void TimeOut()
        {
            if (TimeoutClock != null && TimeoutClock.IsRunning)
            {
                TimeoutClock.Stop();
                TimeoutVisibility = Visibility.Hidden;
            }

            else
            {
                TimeoutClock = new StopWatchWithOffset();
                TimeoutVisibility = Visibility.Visible;
                TimeoutClock.Start();
            }
        }

        private ICommand _StartAppSwitcherCommand;
        public ICommand StartAppSwitcherCommand
        {
            get
            {
                if (_StartAppSwitcherCommand == null)
                    _StartAppSwitcherCommand = new RelayCommand(param => StartAppSwitcher(), param => CanStartStopAppSwitcher());
                return _StartAppSwitcherCommand;
            }
        }

        private bool CanStartStopAppSwitcher()
        {
            return true;
        }

        private void StartAppSwitcher()
        {
            try
            {
                var processes = Process.GetProcessesByName("wscript");
                if (processes.Length == 0)
                {
                    string pathToSwitcherVBS = @"Files\\AppSwitcher.vbs";
                    Process.Start(pathToSwitcherVBS);
                }
            }
            catch (Exception)
            {
                //  Mach nix
            }
        }

        private ICommand _StopAppSwitcherCommand;
        public ICommand StopAppSwitcherCommand
        {
            get
            {
                if (_StopAppSwitcherCommand == null)
                    _StopAppSwitcherCommand = new RelayCommand(param => StopAppSwitcher(param), param => CanStartStopAppSwitcher());
                return _StopAppSwitcherCommand;
            }
        }

        private void StopAppSwitcher(object param)
        {
            try
            {
                var processes = Process.GetProcessesByName("wscript");
                if (processes.Length > 0)
                {
                    foreach (var proc in processes)
                    {
                        proc.Kill();
                    }
                }
                else
                    //  Meldung nur anzeigen, wenn manuell geklickt!
                    if (param == null)
                    System.Windows.Forms.MessageBox.Show("Keine Werbung mehr aktiv.", "Ok", MessageBoxButtons.OK);
            }
            catch (Exception)
            {

            }
        }

        private ICommand _CancelPauseCommand;
        public ICommand CancelPauseCommand
        {
            get
            {
                if (_CancelPauseCommand == null)
                    _CancelPauseCommand = new RelayCommand(param => CancelPause(), param => CanCancelPause());
                return _CancelPauseCommand;
            }
        }

        private bool CanCancelPause()
        {
            return IsCurrentlyPause;
        }

        private void CancelPause()
        {
            if (IsCurrentlyPause)
            {
                var timeToSet = PauseDuration.Subtract(new TimeSpan(0, 0, 0, 1));
                SetClock(timeToSet);
                StartClock(false);
            }
        }


        #endregion

        public void StartClock(bool includePenalties)
        {
            Clock.Start();
            if (includePenalties)
            {
                AllPenalties.OrderBy(kvp => kvp.Key).Where(kvp => kvp.Value.IsHomeTeam).Take(2).ToList().ForEach(pen => pen.Value.Stopwatch.Start());
                AllPenalties.OrderBy(kvp => kvp.Key).Where(kvp => !kvp.Value.IsHomeTeam).Take(2).ToList().ForEach(pen => pen.Value.Stopwatch.Start());
            }
            OnPropertyChanged("IsClockRunning");
        }

        public void StopClock()
        {
            Clock.Stop();
            AllPenalties.ToList().ForEach(item => item.Value.Stopwatch.Stop());
            OnPropertyChanged("IsClockRunning");
            CommandManager.InvalidateRequerySuggested();
        }

        public void ResetClock()
        {
            Clock.Reset();
        }

        public void SetClock(TimeSpan duration)
        {
            Clock.ElapsedTimeSpan = duration;
            Clock.UpdateElapsedTimeSpan();
        }

        public int GetKeyForNewPenalty()
        {
            int newKey = AllPenalties.Count == 0 ? 1 : AllPenalties.Keys.OrderByDescending(key => key).First() + 1;
            return newKey;
        }

        public bool AddNewPenalty(PenaltyInfo info)
        {
            try
            {
                AllPenalties.Add(info.Key, info);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void RemovePenalty(PenaltyInfo penalty)
        {
            AllPenalties.Remove(penalty.Key);
        }

        internal void KeyDown(System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    AddHomeTwoMinPenaltyCommand.Execute(null);
                    break;
                case Key.F2:
                    AddHomeFiveMinPenaltyCommand.Execute(null);
                    break;
                case Key.F3:
                    AddHomeCustomPenaltyCommand.Execute(null);
                    break;
                case Key.F5:
                    AddAwayTwoMinPenaltyCommand.Execute(null);
                    break;
                case Key.F6:
                    AddAwayFiveMinPenaltyCommand.Execute(null);
                    break;
                case Key.F7:
                    AddAwayCustomPenaltyCommand.Execute(null);
                    break;
                case Key.F12:
                    RunDownwards = !RunDownwards;
                    break;
            }
        }
    }
}
