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

        string instructionsText = "Welcome to Baking Simulator. Your goal is to become the best baker in town and bake every recipe!\n\n" +
            "Your inventory will be on the right side and what you're working on will be on the left.\n \n" +
            "Use the Craft window to view and craft recipes. Clicking the drop down on recipes will let you see what ingredients you need.\n \n" +
            "In the Buy window, buy extra ingredients needed for recipes. The vendor has displayed how many each item they have in stock, and will restock periodically. \n\n" +
            "Use the Sell window to sell anything you bake and any extra ingredients you don't want. \n\n" +
            "If you get stuck, use the Tips window to collect extra change. It will increase your funds by 0.01 per click.";

        string submitNameInstructions = "Type your name below and hit submit to get started.";

        public Main()
        {
           
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Output.Text = instructionsText;
            NameSubmissionInfo.Text = submitNameInstructions;
            SetButtonsInvisible();
            
            //Output.Text = mainWindow.game.Player.GetInventoryItemList();

        }

        private void SubmitName_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.game.Player.PersonName = NameInput.Text;
            mainWindow.PlayerName.Text = mainWindow.game.Player.PersonName;
            SetButtonVisibility();
        }
        private void SetButtonsInvisible()
        {
            mainWindow.Trade.Visibility = Visibility.Collapsed;
            mainWindow.Craft.Visibility = Visibility.Collapsed;
            mainWindow.Sell.Visibility = Visibility.Collapsed;
            mainWindow.CollectTips.Visibility = Visibility.Collapsed;
        }
        private void SetButtonVisibility()
        {
            //mainWindow.Inventory.Visibility = Visibility.Visible;
            mainWindow.Trade.Visibility = Visibility.Visible;
            mainWindow.Craft.Visibility = Visibility.Visible;
            mainWindow.Sell.Visibility = Visibility.Visible;
            mainWindow.CollectTips.Visibility = Visibility.Visible;
        }
    }
}
