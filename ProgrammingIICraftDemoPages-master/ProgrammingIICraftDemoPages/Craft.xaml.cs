using Microsoft.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace ProgrammingIICraftDemoPages
{
    /// <summary>
    /// Interaction logic for Craft.xaml
    /// </summary>
    public partial class Craft : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public Craft()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonVisibility();
            //Output.Text = mainWindow.game.GetRecipeList();

            MenuItem milkshake = new MenuItem() {ItemContent = "Milkshake" };
            milkshake.Items.Add(new MenuItem() { ItemContent = "Milk" });
            milkshake.Items.Add(new MenuItem() { ItemContent = "Chocolate" });
            milkshake.ItemImage = (ImageSource)Resources["poop_sharp_solidDrawingImage"];
            Trace.WriteLine(Resources.Keys);

            RecipeTreeView.Items.Add(milkshake);

            MenuItem suspiciousMilkshake = new MenuItem() { ItemContent = "Suspicious Milkshake" };
            suspiciousMilkshake.Items.Add(new MenuItem() { ItemContent = "'Milk'" });
            suspiciousMilkshake.Items.Add(new MenuItem() { ItemContent = "Chocolate" });

            RecipeTreeView.Items.Add(suspiciousMilkshake);
        }

        private void SetButtonVisibility()
        {
            mainWindow.Craft.Visibility = Visibility.Collapsed;
            mainWindow.Trade.Visibility = Visibility.Visible;
            //mainWindow.Inventory.Visibility = Visibility.Visible;
        }

        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

        }

     
    }

    public class MenuItem
    {
        public ObservableCollection<MenuItem> Items{ get; set; }

        public string ItemContent{ get; set; }

        public ImageSource ItemImage { get; set; }
        
        
        public MenuItem()
        {
            Items = new ObservableCollection<MenuItem>();
        }
    }
}
