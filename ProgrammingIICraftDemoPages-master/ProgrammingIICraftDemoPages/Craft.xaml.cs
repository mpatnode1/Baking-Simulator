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

        IDictionary<MenuItem, Recipe> MenuItemToRecipe = new Dictionary<MenuItem, Recipe>();
        public Craft()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonVisibility();
            //Output.Text = mainWindow.game.GetRecipeList();

            setUpRecipes();

        }

        private void setUpRecipes()
        {
            //puts recipes from game into tree view 
            for (int i = 0; i < mainWindow.game.Recipes.Count; i++)
            {
                MenuItem recipeListItem = new MenuItem() { ItemContent = mainWindow.game.Recipes[i].RecipeName };
                //recipeListItem.ItemDescriptions.Add(new MenuItem() { ItemContent = mainWindow.game.Recipes[i].GetRecipeDescription()});

                MenuItemToRecipe.Add(recipeListItem, mainWindow.game.Recipes[i]);

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
            mainWindow.Sell.Visibility = Visibility.Visible;
        }

        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            FeedbackText.Text = string.Empty;

           //takes selected item in from menu and tries to craft it 
            MenuItem selectedRecipe = (MenuItem)RecipeTreeView.SelectedItem;
     
            Recipe result;

            if (MenuItemToRecipe.TryGetValue(selectedRecipe, out result));
            bool craftSuccess = mainWindow.game.CheckAbilityToCraft(result);

            if(craftSuccess)
            {
                FeedbackText.Text = "Recipe made! Item has been added to your inventory.";
                mainWindow.inventory.UpdateInventoryWindow();
            }
            else
            {
                FeedbackText.Text = "You do not have the correct ingredients.";
            }
           
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
