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

            if (mainWindow.game.VendorRestockCounter >= 3)
            {
                mainWindow.game.Vendor.RestockVendor(mainWindow.game);
                mainWindow.game.VendorRestockCounter = 0;
            }
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
            TraderConversation.Text = mainWindow.game.Vendor.VendorIntroduction(mainWindow.game);
               
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

            int buySuccess = mainWindow.game.CheckAbilityToBuyItems();


            if (buySuccess == 1)
            {
                //bought
                TraderConversation.Text = "Here you go. I've added it to your inventory.";

                //update inventory
                mainWindow.inventory.UpdateInventoryWindow();
                updateTradeList();
                mainWindow.UpdateCurrency();
                mainWindow.game.VendorRestockCounter++;
                


            }
            else if(buySuccess == 2)
            {
                TraderConversation.Text = "I'm sorry, it looks like you don't have enough.";
            }
            // else if player doesnt have item selected.
            else
            {
                TraderConversation.Text = "Please select an item first.";
            }
        }
    }

    public class TradeViewModel 
    {
        public ObservableCollection<Item> TradeList { get; set; } = new ObservableCollection<Item>();


    }
}
