using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WatchModule.ViewModels;

namespace WatchModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var focusedControl = Keyboard.FocusedElement;
            TextBox txtBox = focusedControl as TextBox;
            if(txtBox == null || !txtBox.Name.Contains("TeamTextBox"))
                ((MainViewModel)this.DataContext).KeyDown(e);
        }
    }
}
