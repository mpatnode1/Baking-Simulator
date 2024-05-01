using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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
        TradeViewModel tradeViewModel;

        public Trade()
        {

            InitializeComponent();

            tradeViewModel = new TradeViewModel();
            updateTradeList();
        }

        private void updateTradeList()
        {
            tradeViewModel.TradeList.Clear();

            foreach (Item item in mainWindow.game.Vendor.Inventory)
            {
                tradeViewModel.TradeList.Add(item);

                //string ItemDescription = item.GetItemDescription();

            }
            this.DataContext = tradeViewModel;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonVisibility();
            TraderConversationUpdate();

            //TradeInventory.Text  = mainWindow.game.Vendor.GetInventoryItemList();
        }

        private void TraderConversationUpdate()
        {
            TraderConversation.Text = mainWindow.game.VendorIntroduction();
               
        }

        private void SetButtonVisibility()
        {
            mainWindow.Trade.Visibility = Visibility.Collapsed;
            mainWindow.Sell.Visibility = Visibility.Visible;
            mainWindow.Craft.Visibility = Visibility.Visible;
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            //this needs a check before it even starts to decide if player has enough
            //otherwise first items checked will be added to player inventory


            //for each item item in traders inventory
            //take item and for loop of buying count
            //check if player has enough currency in account
            //if not fail

            //if player does have enough currency
            //take item in for loop and add it to inventory 
            //reset buying count  
        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {

            var itemSelected = ((Button)sender).Tag;
            Debug.WriteLine(itemSelected.ToString());

            
            Item? itemToIncrement = mainWindow.game.Vendor.Inventory.Find(x => x.ItemName == itemSelected);
            itemToIncrement.BuyingCount++;
            updateTradeList();

        }
        private void Decrement_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class TradeViewModel 
    {
        public ObservableCollection<Item> TradeList { get; set; } = new ObservableCollection<Item>();


    }
}
