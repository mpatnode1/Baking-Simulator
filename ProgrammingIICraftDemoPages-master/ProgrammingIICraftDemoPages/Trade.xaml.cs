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

namespace ProgrammingIICraftDemoPages
{
    /// <summary>
    /// Interaction logic for Trade.xaml
    /// </summary>
    public partial class Trade : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public Trade()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonVisibility();
            
        }

        private void SetButtonVisibility()
        {
            mainWindow.Trade.Visibility = Visibility.Collapsed;
            //mainWindow.Inventory.Visibility = Visibility.Visible;
            mainWindow.Craft.Visibility = Visibility.Visible;
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Sell_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
