using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        string BuyingItemCount;

        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        TradeViewModel tradeViewModel;

        public Trade()
        {

            InitializeComponent();

            tradeViewModel = new TradeViewModel();
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

        }
        private void Sell_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {

            //get index of increment click button in tradelist that was clicked
            //find item at that index in tradelist
            //take that item and increment the buying count by 1
            
            //int itemToBuyIndex = tradeViewModel.TradeList.IndexOf(x => x.Button == sender);

            //Item ItemToBuy = (Item)TradeInventory;      
            //ItemToBuy.BuyingCount++;
            //BuyingItemCount = $" {ItemToBuy.BuyingCount}";
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
