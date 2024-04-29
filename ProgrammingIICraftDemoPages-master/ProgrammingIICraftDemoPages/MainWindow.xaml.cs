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
 /*
 * Programming II Craft Project Demo (Pages)
 * Meghan Patnode
 * April 17th 2024
 * Uses demo code from PROG 201 Programming II course
 * https://github.com/janell-baxter/ProgrammingIICraftDemoPages
 */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Game game = new Game();

        public Inventory inventory;
        public MainWindow()
        {
            InitializeComponent();
            inventory = new Inventory();
        }

        private void SetUp()
        {
            ContentFrame.Navigate(new Main());
            PlayerName.Text = game.Player.PersonName;
            Currency.Text = game.Player.PersonCurrency.ToString("C");

            InventoryFrame.Navigate(inventory);
        }

        #region EventHandlers
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SetUp();
        }
        private void Craft_Click(object sender, RoutedEventArgs e)
        {
         
            ContentFrame.Navigate(new Uri("Craft.xaml", UriKind.Relative));
        }

        private void Trade_Click(object sender, RoutedEventArgs e)
        {
       
            ContentFrame.Navigate(new Uri("Trade.xaml", UriKind.Relative));
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {

            ContentFrame.Navigate(new Uri("Sell.xaml", UriKind.Relative));
        }
        /*private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Uri("Main.xaml", UriKind.Relative));
        }*/
        #endregion
    }
}
