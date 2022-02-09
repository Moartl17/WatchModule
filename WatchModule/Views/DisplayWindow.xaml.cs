using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WatchModule.Views
{
    /// <summary>
    /// Interaktionslogik für DisplayWindow.xaml
    /// </summary>
    public partial class DisplayWindow : Window
    {
        public event EventHandler WindowWasClosed;

        public DisplayWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (WindowWasClosed != null)
                WindowWasClosed(this, new EventArgs());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

            //if (secondaryScreen != null)
            //{
            //    if (!this.IsLoaded)
            //        this.WindowStartupLocation = WindowStartupLocation.Manual;

            //    var workingArea = secondaryScreen.WorkingArea;
            //    this.Left = workingArea.Left;
            //    this.Top = workingArea.Top;
            //    this.Width = workingArea.Width;
            //    this.Height = workingArea.Height;
            //    // If window isn't loaded then maxmizing will result in the window displaying on the primary monitor
            //    if (this.IsLoaded)
                    this.WindowState = WindowState.Maximized;
            //}
        }
    }
}
