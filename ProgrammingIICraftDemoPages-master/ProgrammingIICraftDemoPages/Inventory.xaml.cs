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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public Inventory()
        {
           
            InitializeComponent();
            //InventoryText.Text = mainWindow.game.Player.GetInventoryItemList();

            InventoryView inventoryview = new InventoryView();
            foreach (Item item in mainWindow.game.Player.Inventory) 
            {
                inventoryview.InventoryList.Add(item);
            }

            this.DataContext = inventoryview;
        }
    }


    public class InventoryView
    {
        public ObservableCollection<Item> InventoryList { get; set; } = new ObservableCollection<Item>();
    }
}
