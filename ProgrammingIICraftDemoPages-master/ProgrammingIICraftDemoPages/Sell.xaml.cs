using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Sell.xaml
    /// </summary>
    public partial class Sell : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public Sell()
        {
            SetButtonVisibility();
            InitializeComponent();
        }

        private void updateSellList()
        {
            SellInventoryView sellInventoryView = new SellInventoryView();
            foreach (Item item in mainWindow.game.Player.Inventory)
            {
                sellInventoryView.InventorySellList.Add(item);

                //string ItemDescription = item.GetItemDescription();

            }
            this.DataContext = sellInventoryView;
        }

        private void SetButtonVisibility()
        {
            mainWindow.Sell.Visibility = Visibility.Collapsed;
            mainWindow.Craft.Visibility = Visibility.Visible;
            mainWindow.Trade.Visibility = Visibility.Visible;
            //mainWindow.Inventory.Visibility = Visibility.Visible;
        }

        private void SellConfirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class SellInventoryView
    {
        public ObservableCollection<Item> InventorySellList { get; set; } = new ObservableCollection<Item>();

    }
}
