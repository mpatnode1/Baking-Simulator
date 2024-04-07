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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public Main()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonVisibility();
            Output.Text = mainWindow.game.Player.GetInventoryItemList();

        }

        private void SetButtonVisibility()
        {
            //mainWindow.Inventory.Visibility = Visibility.Collapsed;
            mainWindow.Trade.Visibility = Visibility.Visible;
            mainWindow.Craft.Visibility = Visibility.Visible;
        }
    }
}
