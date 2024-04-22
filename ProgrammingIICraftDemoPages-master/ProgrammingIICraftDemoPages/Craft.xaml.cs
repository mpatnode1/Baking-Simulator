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

            //puts recipes from game into tree view 

            for (int i = 0; i < mainWindow.game.Recipes.Count; i++)
            {
                MenuItem recipeListItem = new MenuItem() { ItemContent = mainWindow.game.Recipes[i].RecipeName };
                //recipeListItem.ItemDescriptions.Add(new MenuItem() { ItemContent = mainWindow.game.Recipes[i].GetRecipeDescription()});

                //when player expands recipe, recipe requirements are shown
                foreach (Item item in mainWindow.game.Recipes[i].RecipeRequirements)
                {
                    string printRequirement = item.ItemAmount + " " + item.ItemAmountType + " " + item.ItemName;
                    recipeListItem.Items.Add(new MenuItem() { ItemContent = printRequirement });
                }
                
                RecipeTreeView.Items.Add(recipeListItem);

            }

        }

        private void SetButtonVisibility()
        {
            mainWindow.Craft.Visibility = Visibility.Collapsed;
            mainWindow.Trade.Visibility = Visibility.Visible;
            //mainWindow.Inventory.Visibility = Visibility.Visible;
        }

        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //TreeViewItem selectedRecipe = (TreeViewItem)RecipeTreeView.SelectedItem;
            MenuItem selectedRecipe = (MenuItem)RecipeTreeView.SelectedItem;
    

        }

     
    }

    public class MenuItem
    {
        
        public ObservableCollection<MenuItem> Items{ get; set; }

        //public ObservableCollection<MenuItem> ItemDescriptions{get; set;}
        public string ItemContent{ get; set; }

        public ImageSource ItemImage { get; set; }
        
        
        public MenuItem()
        {
            Items = new ObservableCollection<MenuItem>();
        }
    }
}
