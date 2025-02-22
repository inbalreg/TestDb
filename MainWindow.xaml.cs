using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
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

namespace TestDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HamburgerMenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("abc" + sender);
        }

        private void HamburgerMenuItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("abcd" + sender);
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("Selection Binding");
        }

        private void HamburgerMenuItem_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("LEV");
        }

        private void HamburgerMenuItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }
    }
    
}

