using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WatchModule
{
   public class RelayCommand : ICommand, INotifyPropertyChanged
   {
      #region Members
      readonly Action<object> _execute;
      readonly Predicate<object> _canExecute;
      #endregion // Fields


      #region Construction

      /// <summary>
      /// Creates a new command that can always execute.
      /// </summary>
      /// <param name="execute">The execution logic.</param>
      public RelayCommand( Action<object> execute )
         : this( execute, null )
      {
      }

      /// <summary>
      /// Creates a new command.
      /// </summary>
      /// <param name="execute">The execution logic.</param>
      /// <param name="canExecute">The execution status logic.</param>
      public RelayCommand( Action<object> execute, Predicate<object> canExecute )
      {
         if( execute == null )
            throw new ArgumentNullException( "execute" );

         _execute = execute;
         _canExecute = canExecute;
      }

      #endregion


      #region ICommand Members

      private bool _CanExecuteProperty;
      public bool CanExecuteProperty
      {
          get { return _CanExecuteProperty; }
          set
          {
              if (_CanExecuteProperty != value)
              {
                  _CanExecuteProperty = value;
                  OnPropertyChanged("CanExecuteProperty");
              }
          }
      }


      /// <summary>
      /// Defines the method that determines whether the command can execute in its current state.
      /// </summary>
      /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
      /// <returns>
      /// true if this command can be executed; otherwise, false.
      /// </returns>
      [DebuggerStepThrough]
      public bool CanExecute( object parameter )
      {
         CanExecuteProperty = _canExecute == null ? true : _canExecute( parameter );
         return CanExecuteProperty;
      }

      /// <summary>
      /// Occurs when changes occur that affect whether or not the command should execute.
      /// </summary>
      public event EventHandler CanExecuteChanged
      {
         add
         {
            CommandManager.RequerySuggested += value;
         }
         remove
         {
            CommandManager.RequerySuggested -= value;
         }
      }

      /// <summary>
      /// Defines the method to be called when the command is invoked.
      /// </summary>
      /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
      public void Execute( object parameter )
      {
         _execute( parameter );
      }

      #endregion   
   
      public event PropertyChangedEventHandler PropertyChanged;
      protected void OnPropertyChanged(string propertyName)
      {
          if (this.PropertyChanged != null)
              PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
